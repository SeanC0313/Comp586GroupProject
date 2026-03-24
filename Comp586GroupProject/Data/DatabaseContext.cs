using Comp586GroupProject.Models;
using Microsoft.EntityFrameworkCore;

namespace Comp586GroupProject.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        // Core DbSets for the main modules
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Billing> Billings { get; set; }

        // Optional: Fluent API configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Patient entity configuration
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(p => p.PatientId);
                entity.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.LastName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.CreatedAt)
                    .ValueGeneratedOnAdd();
            });

            // Appointment entity configuration
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.AppointmentID);
                entity.Property(a => a.AppointmentDate).IsRequired();
                entity.HasOne(a => a.Patient)
                      .WithMany(p => p.Appointments)
                      .HasForeignKey(a => a.PatientID);
                entity.HasOne(a => a.Staff)
                      .WithMany(s => s.Appointments)
                      .HasForeignKey(a => a.StaffID);
            });

            // Prescription entity configuration
            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(pr => pr.PrescriptionID);
                entity.Property(pr => pr.Medication).IsRequired().HasMaxLength(200);
                entity.HasOne(pr => pr.Patient)
                      .WithMany(p => p.Prescriptions)
                      .HasForeignKey(pr => pr.PatientID);
                entity.HasOne(pr => pr.Staff)
                      .WithMany(s => s.Prescriptions)
                      .HasForeignKey(pr => pr.StaffID);
            });

            // Medication entity configuration
            modelBuilder.Entity<Medication>(entity =>
            {
                entity.HasKey(m => m.MedicationId);
                entity.Property(m => m.Name).HasMaxLength(100).IsRequired();
                entity.Property(m => m.Description).IsRequired(false);
                entity.Property(m => m.Stock).IsRequired();
            });

            // Insurance entity configuration
            modelBuilder.Entity<Insurance>(entity =>
            {
                entity.HasKey(i => i.InsuranceId);
                entity.Property(i => i.ProviderName).IsRequired().HasMaxLength(100);
                entity.Property(i => i.Phone).HasMaxLength(20);
                entity.Property(i => i.CoverageDetails).IsRequired(false);
            });

            // Role entity configuration
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.RoleID);
                entity.Property(r => r.RoleName)
                      .HasMaxLength(50);

                entity.HasIndex(r => r.RoleName)
                      .IsUnique();
            });

            // Staff entity configuration
            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(s => s.StaffID);

                entity.Property(s => s.FirstName).HasMaxLength(50);
                entity.Property(s => s.LastName).HasMaxLength(50);
                entity.Property(s => s.Specialty).HasMaxLength(100);
                entity.Property(s => s.Email).HasMaxLength(100);
                entity.Property(s => s.PassWordHash).HasMaxLength(255);

                entity.HasIndex(s => s.Email)
                      .IsUnique();

                entity.HasOne(s => s.Role)
                      .WithMany(r => r.StaffMembers)
                      .HasForeignKey(s => s.RoleID);
            });

            modelBuilder.Entity<Billing>(entity =>
            {
                entity.HasKey(b => b.BillingId);

                entity.Property(b => b.InsuranceCovered)
                      .HasMaxLength(50);

                entity.Property(b => b.PaymentStatus)
                      .HasMaxLength(50);

                entity.HasOne(b => b.Patient)
                      .WithMany()
                      .HasForeignKey(b => b.PatientId);

                entity.HasOne(b => b.Appointment)
                      .WithMany()
                      .HasForeignKey(b => b.AppointmentId);
            });

        }
    }
}