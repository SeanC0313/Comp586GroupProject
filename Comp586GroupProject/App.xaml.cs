using Comp586GroupProject.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Comp586GroupProject
{
    public partial class App : Application
    {
        private readonly IAuthService _auth;
        private readonly IServiceProvider _services;

        public App(IAuthService auth, IServiceProvider services)
        {
            _auth = auth;
            _services = services;
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Page page = _auth.IsAuthenticated
                ? _services.GetRequiredService<AppShell>()
                : _services.GetRequiredService<LoginPage>();
            return new Window(page);
        }
    }
}
