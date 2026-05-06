namespace Comp586GroupProject;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(Views.PatientsPage), typeof(Views.PatientsPage));
        Routing.RegisterRoute(nameof(Views.NewPatientPage), typeof(Views.NewPatientPage));
        Routing.RegisterRoute(nameof(Views.PatientDetailsPage), typeof(Views.PatientDetailsPage));
        Routing.RegisterRoute(nameof(Views.EditPatientPage), typeof(Views.EditPatientPage));

        Routing.RegisterRoute(nameof(Views.AppointmentsPage), typeof(Views.AppointmentsPage));
        Routing.RegisterRoute(nameof(Views.NewAppointmentPage), typeof(Views.NewAppointmentPage));
        Routing.RegisterRoute(nameof(Views.EditAppointmentPage), typeof(Views.EditAppointmentPage));

        Routing.RegisterRoute(nameof(Views.StaffPage), typeof(Views.StaffPage));
        Routing.RegisterRoute(nameof(Views.NewStaffPage), typeof(Views.NewStaffPage));
        Routing.RegisterRoute(nameof(Views.EditStaffPage), typeof(Views.EditStaffPage));

        Routing.RegisterRoute(nameof(Views.ReportsPage), typeof(Views.ReportsPage));

        Routing.RegisterRoute(nameof(Views.MedicalRecordsPage), typeof(Views.MedicalRecordsPage));
        Routing.RegisterRoute(nameof(Views.PrescriptionsPage), typeof(Views.PrescriptionsPage));
        Routing.RegisterRoute(nameof(Views.LabsPage), typeof(Views.LabsPage));
        Routing.RegisterRoute(nameof(Views.RoleManagementPage), typeof(Views.RoleManagementPage));
        Routing.RegisterRoute(nameof(Views.BillingPage), typeof(Views.BillingPage));
        Routing.RegisterRoute(nameof(Views.InsurancePage), typeof(Views.InsurancePage));
        Routing.RegisterRoute(nameof(Views.SuppliersPage), typeof(Views.SuppliersPage));
        Routing.RegisterRoute(nameof(Views.FinancialReportsPage), typeof(Views.FinancialReportsPage));

        Routing.RegisterRoute(nameof(Views.NewBillingPage), typeof(Views.NewBillingPage));
        Routing.RegisterRoute(nameof(Views.NewPrescriptionPage), typeof(Views.NewPrescriptionPage));
        Routing.RegisterRoute(nameof(Views.NewMedicalRecordPage), typeof(Views.NewMedicalRecordPage));
        Routing.RegisterRoute(nameof(Views.NewInsurancePage), typeof(Views.NewInsurancePage));
        Routing.RegisterRoute(nameof(Views.NewSupplierPage), typeof(Views.NewSupplierPage));
        Routing.RegisterRoute(nameof(Views.NewLabTestPage), typeof(Views.NewLabTestPage));
        Routing.RegisterRoute(nameof(Views.NewLabOrderPage), typeof(Views.NewLabOrderPage));
    }
}