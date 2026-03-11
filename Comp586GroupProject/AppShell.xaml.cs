namespace Comp586GroupProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.NewPatientPage), typeof(Views.NewPatientPage));
            Routing.RegisterRoute(nameof(Views.PatientDetailsPage), typeof(Views.PatientDetailsPage));
        }
    }
}
