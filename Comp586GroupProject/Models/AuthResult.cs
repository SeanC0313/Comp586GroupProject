namespace Comp586GroupProject.Models
{
    public sealed record AuthResult(bool Success, string? ErrorMessage = null)
    {
        public static AuthResult Ok() => new(true, null);

        public static AuthResult Failed(string message) => new(false, message);
    }
}
