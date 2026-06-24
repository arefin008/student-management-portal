using DAL.Models;
using Microsoft.Data.SqlClient;
namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _cs;
        public UserRepository(string cs) => _cs = cs;

        public async Task<User?> GetByUsernameAsync(string username)
        {
            const string sql = "SELECT UserId, Username, PasswordHash, Role FROM Users WHERE Username = @U";
            await using var con = new SqlConnection(_cs);
            await using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@U", username);
            await con.OpenAsync();
            await using var r = await cmd.ExecuteReaderAsync();
            if (await r.ReadAsync())
                return new User
                {
                    UserId = (int)r["UserId"],
                    Username = r["Username"].ToString()!,
                    PasswordHash = r["PasswordHash"].ToString()!,
                    Role = r["Role"].ToString()!
                };
            return null;
        }
    }
}