using DAL.Models;
using Microsoft.Data.SqlClient;
namespace DAL.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly string _cs;
        public SubjectRepository(string cs) => _cs = cs;
        private SqlConnection Con() => new SqlConnection(_cs);

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            var list = new List<Subject>();
            await using var con = Con();
            await using var cmd = new SqlCommand("SELECT SubjectId, SubjectName FROM Subject", con);
            await con.OpenAsync();
            await using var r = await cmd.ExecuteReaderAsync();
            while (await r.ReadAsync())
                list.Add(new Subject { SubjectId = (int)r["SubjectId"], SubjectName = r["SubjectName"].ToString()! });
            return list;
        }

        public async Task<int> CreateAsync(string name)
        {
            await using var con = Con();
            await using var cmd = new SqlCommand("INSERT INTO Subject(SubjectName) VALUES(@N); SELECT SCOPE_IDENTITY();", con);
            cmd.Parameters.AddWithValue("@N", name);
            await con.OpenAsync();
            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }

        public async Task<bool> UpdateAsync(int id, string name)
        {
            await using var con = Con();
            await using var cmd = new SqlCommand("UPDATE Subject SET SubjectName=@N WHERE SubjectId=@Id", con);
            cmd.Parameters.AddWithValue("@N", name); cmd.Parameters.AddWithValue("@Id", id);
            await con.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using var con = Con();
            await using var cmd = new SqlCommand("DELETE FROM Subject WHERE SubjectId=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            await con.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}