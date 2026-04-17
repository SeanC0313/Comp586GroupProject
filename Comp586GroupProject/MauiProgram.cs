using Microsoft.Extensions.Logging;
using Comp586GroupProject.Services;
using Comp586GroupProject.Interfaces;

namespace Comp586GroupProject
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddScoped<IPatientInterface, PatientService>();

            builder.Services.AddTransient<Views.DashboardPage>();
            builder.Services.AddTransient<Views.PatientDetailsPage>();
            builder.Services.AddTransient<Views.NewPatientPage>();
            builder.Services.AddTransient<Views.PatientDetailsPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
