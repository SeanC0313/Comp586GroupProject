using Comp586GroupProject.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Comp586GroupProject
{
    public partial class LoginPage : ContentPage
    {
        private readonly IAuthService _auth;
        private readonly IServiceProvider _services;

        public LoginPage(IAuthService auth, IServiceProvider services)
        {
            InitializeComponent();
            _auth = auth;
            _services = services;
        }

        private async void OnLoginClicked(object? sender, EventArgs e)
        {
            var email = EmailEntry.Text?.Trim();
            var password = PasswordEntry.Text;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ShowError("Enter email and password.");
                return;
            }

            ErrorLabel.IsVisible = false;
            var result = await _auth.LoginAsync(email, password);
            if (!result.Success)
            {
                ShowError(result.ErrorMessage ?? "Login failed.");
                return;
            }

            if (Application.Current?.Windows.Count is not > 0)
                return;

            Application.Current.Windows[0].Page = _services.GetRequiredService<AppShell>();
        }

        private void ShowError(string message)
        {
            ErrorLabel.Text = message;
            ErrorLabel.IsVisible = true;
        }
    }
}
