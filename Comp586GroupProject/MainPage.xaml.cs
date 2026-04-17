using Comp586GroupProject.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Comp586GroupProject
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private readonly IAuthService _auth;
        private readonly IServiceProvider _services;

        public MainPage(IAuthService auth, IServiceProvider services)
        {
            InitializeComponent();
            _auth = auth;
            _services = services;
        }

        private void OnSignOutClicked(object? sender, EventArgs e)
        {
            _auth.Logout();
            if (Application.Current?.Windows.Count is not > 0)
                return;
            Application.Current.Windows[0].Page = _services.GetRequiredService<LoginPage>();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
