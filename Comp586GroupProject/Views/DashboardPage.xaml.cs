namespace Comp586GroupProject.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterPatient(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(NewPatientPage));

    private async void OnViewPatients(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(PatientsPage));

    private async void OnAddPatientClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(PatientsPage));

    private async void OnViewAppointmentsClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(AppointmentsPage));

    private async void OnManageStaffClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(StaffPage));

    private async void OnGenerateReportsClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(ReportsPage));
    private async void OnMedicalRecordsClicked(object sender, EventArgs e) =>
   await Shell.Current.GoToAsync(nameof(MedicalRecordsPage));

    private async void OnPrescriptionsClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(PrescriptionsPage));

    private async void OnLabsClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(LabsPage));

    private async void OnRolesClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(RoleManagementPage));

    private async void OnBillingClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(BillingPage));

    private async void OnInsuranceClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(InsurancePage));

    private async void OnSuppliersClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(SuppliersPage));

    private async void OnFinancialReportsClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(FinancialReportsPage));
}