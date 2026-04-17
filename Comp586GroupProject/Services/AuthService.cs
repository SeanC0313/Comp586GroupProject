using Comp586GroupProject.Interfaces;
using Comp586GroupProject.Models;
using Microsoft.AspNetCore.Identity;

namespace Comp586GroupProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly IStaffService _staffService;
        private readonly PasswordHasher<Staff> _passwordHasher = new();
        private Staff? _currentStaff;

        public AuthService(IStaffService staffService)
        {
            _staffService = staffService;
        }

        public Staff? CurrentStaff => _currentStaff;

        public bool IsAuthenticated => _currentStaff is not null;

        public event EventHandler? AuthenticationStateChanged;

        public async Task<AuthResult> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(password))
                return AuthResult.Failed("Email and password are required.");

            var normalizedEmail = email.Trim();
            var staff = await _staffService.GetByEmailAsync(normalizedEmail, cancellationToken);
            if (staff is null || string.IsNullOrEmpty(staff.PassWordHash))
                return AuthResult.Failed("Invalid email or password.");

            var verification = _passwordHasher.VerifyHashedPassword(staff, staff.PassWordHash, password);
            if (verification == PasswordVerificationResult.Failed)
                return AuthResult.Failed("Invalid email or password.");

            _currentStaff = staff;
            AuthenticationStateChanged?.Invoke(this, EventArgs.Empty);
            return AuthResult.Ok();
        }

        public void Logout()
        {
            _currentStaff = null;
            AuthenticationStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
