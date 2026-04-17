using Comp586GroupProject.Models;
using Microsoft.AspNetCore.Identity;

namespace Comp586GroupProject.Services
{
    /// <summary>
    /// Use when inserting or updating <see cref="Staff.PassWordHash"/> (e.g. SQL seed, admin tool).
    /// The hash format must match <see cref="AuthService"/> verification.
    /// </summary>
    public static class StaffPasswordHasher
    {
        private static readonly PasswordHasher<Staff> Hasher = new();

        public static string Hash(Staff staff, string plainPassword) =>
            Hasher.HashPassword(staff, plainPassword);
    }
}
