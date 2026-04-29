using Comp586GroupProject.Data;
using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Comp586GroupProject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Comp586GroupProject
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            AddEmbeddedAppSettings(builder.Configuration);

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var connectionString = builder.Configuration.GetConnectionString("MedicalDb");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string 'MedicalDb' is missing. Edit appsettings.json (embedded resource) with your MySQL server, database name, user, and password.");
            }

            var versionString = builder.Configuration["Database:ServerVersion"] ?? "8.0.36-mysql";
            var serverVersion = ServerVersion.Parse(versionString);

            builder.Services.AddDbContextFactory<DatabaseContext>(options =>
                options.UseMySql(connectionString, serverVersion));

            builder.Services.AddTransient<IStaffService, StaffService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddSingleton<App>();

            builder.Services.AddTransient<IPatientInterface, PatientService>();
            builder.Services.AddTransient<IAppointmentInterface, AppointmentService>();
            builder.Services.AddTransient<IBillingService, BillingService>();
            builder.Services.AddTransient<IPrescriptionService, PrescriptionService>();
            builder.Services.AddTransient<IRoleService, RoleService>();
            builder.Services.AddTransient<IMedicationService, MedicationService>();
            builder.Services.AddTransient<IInsuranceService, InsuranceService>();
            builder.Services.AddTransient<IMedicalRecordService, MedicalRecordService>();
            builder.Services.AddTransient<ITreatmentPlanService, TreatmentPlanService>();
            builder.Services.AddTransient<ILabService, LabService>();
            builder.Services.AddTransient<ISupplierService, SupplierService>();
            builder.Services.AddTransient<IFinancialReportService, FinancialReportService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            SeedRolesAsync(app).GetAwaiter().GetResult();
            return app;
        }

        private static void AddEmbeddedAppSettings(IConfigurationBuilder configuration)
        {
            var assembly = typeof(MauiProgram).Assembly;
            var resourceName = assembly
                .GetManifestResourceNames()
                .FirstOrDefault(n => n.EndsWith("appsettings.json", StringComparison.OrdinalIgnoreCase));

            if (resourceName is null)
                return;

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream is not null)
                configuration.AddJsonStream(stream);
        }

        private static async Task SeedRolesAsync(MauiApp app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DatabaseContext>>();
            await using var db = await factory.CreateDbContextAsync();

            var existingRoleNames = await db.Roles
                .Select(r => r.RoleName)
                .ToListAsync();

            var requiredRoles = new[] { "Admin", "Doctor", "Nurse", "Billing" };
            var missingRoles = requiredRoles
                .Except(existingRoleNames, StringComparer.OrdinalIgnoreCase)
                .Select(roleName => new Role { RoleName = roleName })
                .ToList();

            if (missingRoles.Count == 0)
                return;

            db.Roles.AddRange(missingRoles);
            await db.SaveChangesAsync();
        }
    }
}
