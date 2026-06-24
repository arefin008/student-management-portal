using System.Data;
using DAL.DTOs;
using DAL.Models;
using Microsoft.Data.SqlClient;

namespace DAL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection GetConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var list = new List<Student>();
            const string sql = @"
                SELECT s.StudentId, s.StudentName, s.Email, s.Phone, s.ClassId, c.ClassName
                FROM Student s
                INNER JOIN Class c ON s.ClassId = c.ClassId";

            await using var con = GetConnection();
            await using var cmd = new SqlCommand(sql, con);
            await con.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new Student
                {
                    StudentId = (int)reader["StudentId"],
                    StudentName = reader["StudentName"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Phone = reader["Phone"] as string,
                    ClassId = (int)reader["ClassId"],
                    ClassName = reader["ClassName"].ToString()
                });
            }
            return list;
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            const string sql = @"
                SELECT s.StudentId, s.StudentName, s.Email, s.Phone, s.ClassId, c.ClassName
                FROM Student s
                INNER JOIN Class c ON s.ClassId = c.ClassId
                WHERE s.StudentId = @Id";

            await using var con = GetConnection();
            await using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", id);
            await con.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Student
                {
                    StudentId = (int)reader["StudentId"],
                    StudentName = reader["StudentName"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Phone = reader["Phone"] as string,
                    ClassId = (int)reader["ClassId"],
                    ClassName = reader["ClassName"].ToString()
                };
            }
            return null;
        }

        public async Task<int> CreateAsync(StudentCreateDto dto)
        {
            const string sql = @"
                INSERT INTO Student (StudentName, Email, Phone, ClassId)
                VALUES (@Name, @Email, @Phone, @ClassId);
                SELECT SCOPE_IDENTITY();";

            await using var con = GetConnection();
            await using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Name", dto.StudentName);
            cmd.Parameters.AddWithValue("@Email", dto.Email);
            cmd.Parameters.AddWithValue("@Phone", (object?)dto.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ClassId", dto.ClassId);
            await con.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<bool> UpdateAsync(StudentUpdateDto dto)
        {
            const string sql = @"
                UPDATE Student
                SET StudentName = @Name, Email = @Email, Phone = @Phone, ClassId = @ClassId
                WHERE StudentId = @Id";

            await using var con = GetConnection();
            await using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Name", dto.StudentName);
            cmd.Parameters.AddWithValue("@Email", dto.Email);
            cmd.Parameters.AddWithValue("@Phone", (object?)dto.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ClassId", dto.ClassId);
            cmd.Parameters.AddWithValue("@Id", dto.StudentId);
            await con.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM Student WHERE StudentId = @Id";
            await using var con = GetConnection();
            await using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", id);
            await con.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}