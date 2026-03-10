using Comp586GroupProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CompGroup586GroupProject.Data
{
    public class DatabaseContext : DbContext
    {
        // Constructor accepting DbContextOptions for dependency injection
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        // Core DbSets for the main modules
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        //public DbSet<Billing> Billings { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<insurance> insurances { get; set; }

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
                entity.Property(p => p.DOB).HasColumnType("date");
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Appointment entity configuration
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.AppointmentID);
                entity.Property(a => a.AppointmentDate).IsRequired();
                entity.HasOne(a => a.Patient)
                      .WithMany(p => p.Appointments)
                      .HasForeignKey(a => a.PatientID);
            });

           /* // Prescription entity configuration
            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(pr => pr.PrescriptionId);
                entity.Property(pr => pr.Medication).IsRequired().HasMaxLength(200);
                entity.HasOne(pr => pr.Patient)
                      .WithMany(p => p.Prescriptions)
                      .HasForeignKey(pr => pr.PatientId);
                entity.HasOne(pr => pr.Staff)
                      .WithMany(s => s.Prescriptions)
                      .HasForeignKey(pr => pr.StaffId);
            });*/

            // Add other entities similarly...
        }
    }
}