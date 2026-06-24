
namespace studentManagement.Helpers
{
    public static class PasswordHelper
    {
        public static string Hash(string plainPassword)
            => BCrypt.Net.BCrypt.HashPassword(plainPassword, workFactor: 11);
    }
}