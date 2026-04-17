using Comp586GroupProject.Models;

namespace Comp586GroupProject.Interfaces
{
    public interface IAuthService
    {
        Staff? CurrentStaff { get; }
        bool IsAuthenticated { get; }

        event EventHandler? AuthenticationStateChanged;

        Task<AuthResult> LoginAsync(string email, string password, CancellationToken cancellationToken = default);

        void Logout();
    }
}
