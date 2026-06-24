using DAL.Models;
using Microsoft.Data.SqlClient;

namespace DAL.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly string _connectionString;
        public ClassRepository(string connectionString) => _connectionString = connectionString;
        private SqlConnection GetConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Class>> GetAllAsync()
        {
            var list = new List<Class>();
            await using var con = GetConnection();
            await using var cmd = new SqlCommand("SELECT ClassId, ClassName FROM Class", con);
            await con.OpenAsync();
            await using var r = await cmd.ExecuteReaderAsync();
            while (await r.ReadAsync())
                list.Add(new Class { ClassId = (int)r["ClassId"], ClassName = r["ClassName"].ToString()! });
            return list;
        }

        public async Task<int> CreateAsync(string className)
        {
            const string sql = "INSERT INTO Class (ClassName) VALUES (@Name); SELECT SCOPE_IDENTITY();";
            await using var con = GetConnection();
            await using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Name", className);
            await con.OpenAsync();
            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }

        public async Task<bool> UpdateAsync(int id, string className)
        {
            const string sql = "UPDATE Class SET ClassName = @Name WHERE ClassId = @Id";
            await using var con = GetConnection();
            await using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Name", className);
            cmd.Parameters.AddWithValue("@Id", id);
            await con.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using var con = GetConnection();
            await using var cmd = new SqlCommand("DELETE FROM Class WHERE ClassId = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            await con.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}