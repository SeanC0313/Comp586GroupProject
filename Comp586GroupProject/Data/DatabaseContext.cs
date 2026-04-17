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
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<TreatmentPlan> TreatmentPlans { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<LabOrder> LabOrders { get; set; }
        public DbSet<LabResult> LabResults { get; set; }

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
                entity.HasOne(pr => pr.Patient)
                      .WithMany(p => p.Prescriptions)
                      .HasForeignKey(pr => pr.PatientID);
                entity.HasOne(pr => pr.Staff)
                      .WithMany(s => s.Prescriptions)
                      .HasForeignKey(pr => pr.StaffID);
                entity.HasOne(pr => pr.Medication)
                      .WithMany()
                      .HasForeignKey(pr => pr.MedicationID);
            });

            // Supplier entity configuration
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(s => s.SupplierId);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.ContactName).HasMaxLength(100).IsRequired(false);
                entity.Property(s => s.Phone).HasMaxLength(20).IsRequired(false);
                entity.Property(s => s.Email).HasMaxLength(100).IsRequired(false);
                entity.Property(s => s.Address).IsRequired(false);
            });

            // Medication entity configuration
            modelBuilder.Entity<Medication>(entity =>
            {
                entity.HasKey(m => m.MedicationId);
                entity.Property(m => m.Name).HasMaxLength(100).IsRequired();
                entity.Property(m => m.Description).IsRequired(false);
                entity.Property(m => m.Stock).IsRequired();

                entity.HasOne(m => m.Supplier)
                      .WithMany(s => s.Medications)
                      .HasForeignKey(m => m.SupplierId)
                      .IsRequired(false);
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

            // LabTest entity configuration
            modelBuilder.Entity<LabTest>(entity =>
            {
                entity.HasKey(l => l.LabTestId);
                entity.Property(l => l.Name).IsRequired().HasMaxLength(100);
                entity.Property(l => l.Description).IsRequired(false);
                entity.Property(l => l.ReferenceRange).HasMaxLength(100).IsRequired(false);
            });

            // LabOrder entity configuration
            modelBuilder.Entity<LabOrder>(entity =>
            {
                entity.HasKey(o => o.LabOrderId);
                entity.Property(o => o.Status).IsRequired().HasMaxLength(20);
                entity.Property(o => o.Notes).IsRequired(false);
                entity.Property(o => o.OrderedAt).ValueGeneratedOnAdd();

                entity.HasOne(o => o.Patient)
                      .WithMany(p => p.LabOrders)
                      .HasForeignKey(o => o.PatientId);

                entity.HasOne(o => o.Staff)
                      .WithMany()
                      .HasForeignKey(o => o.StaffId)
                      .IsRequired(false);

                entity.HasOne(o => o.LabTest)
                      .WithMany()
                      .HasForeignKey(o => o.LabTestId);

                entity.HasOne(o => o.Result)
                      .WithOne(r => r.LabOrder)
                      .HasForeignKey<LabResult>(r => r.LabOrderId);
            });

            // LabResult entity configuration
            modelBuilder.Entity<LabResult>(entity =>
            {
                entity.HasKey(r => r.LabResultId);
                entity.Property(r => r.ResultValue).IsRequired().HasMaxLength(255);
                entity.Property(r => r.Unit).HasMaxLength(50).IsRequired(false);
                entity.Property(r => r.Notes).IsRequired(false);

                entity.HasOne(r => r.ReviewedByStaff)
                      .WithMany()
                      .HasForeignKey(r => r.ReviewedByStaffId)
                      .IsRequired(false);
            });

            // TreatmentPlan entity configuration
            modelBuilder.Entity<TreatmentPlan>(entity =>
            {
                entity.HasKey(t => t.TreatmentPlanId);
                entity.Property(t => t.Title).IsRequired().HasMaxLength(200);
                entity.Property(t => t.Status).IsRequired().HasMaxLength(20);
                entity.Property(t => t.Description).IsRequired(false);
                entity.Property(t => t.Notes).IsRequired(false);
                entity.Property(t => t.CreatedAt).ValueGeneratedOnAdd();

                entity.HasOne(t => t.Patient)
                      .WithMany(p => p.TreatmentPlans)
                      .HasForeignKey(t => t.PatientId);

                entity.HasOne(t => t.Staff)
                      .WithMany()
                      .HasForeignKey(t => t.StaffId)
                      .IsRequired(false);
            });

            // MedicalRecord entity configuration
            modelBuilder.Entity<MedicalRecord>(entity =>
            {
                entity.HasKey(m => m.MedicalRecordId);
                entity.Property(m => m.Diagnosis).IsRequired().HasMaxLength(255);
                entity.Property(m => m.Symptoms).IsRequired(false);
                entity.Property(m => m.Notes).IsRequired(false);
                entity.Property(m => m.RecordedAt).ValueGeneratedOnAdd();

                entity.HasOne(m => m.Patient)
                      .WithMany(p => p.MedicalRecords)
                      .HasForeignKey(m => m.PatientId);

                entity.HasOne(m => m.Staff)
                      .WithMany()
                      .HasForeignKey(m => m.StaffId)
                      .IsRequired(false);
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