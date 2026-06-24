using DAL.DTOs;
using DAL.Models;
using Microsoft.Data.SqlClient;
namespace DAL.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly string _cs;
        public ResultRepository(string cs) => _cs = cs;
        private SqlConnection Con() => new SqlConnection(_cs);

        public async Task<IEnumerable<Result>> GetAllAsync()
        {
            var list = new List<Result>();
            const string sql = @"
                SELECT r.ResultId, r.StudentId, r.SubjectId, r.Marks,
                       s.StudentName, sub.SubjectName
                FROM Result r
                JOIN Student s ON r.StudentId = s.StudentId
                JOIN Subject sub ON r.SubjectId = sub.SubjectId";
            await using var con = Con();
            await using var cmd = new SqlCommand(sql, con);
            await con.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                list.Add(new Result
                {
                    ResultId = (int)reader["ResultId"],
                    StudentId = (int)reader["StudentId"],
                    SubjectId = (int)reader["SubjectId"],
                    Marks = (decimal)reader["Marks"],
                    StudentName = reader["StudentName"].ToString(),
                    SubjectName = reader["SubjectName"].ToString()
                });
            return list;
        }

        public async Task<StudentResultDto?> GetByStudentIdAsync(int studentId)
        {
            const string sql = @"
                SELECT s.StudentName, c.ClassName, sub.SubjectName, r.Marks
                FROM Result r
                JOIN Student s  ON r.StudentId  = s.StudentId
                JOIN Class c    ON s.ClassId     = c.ClassId
                JOIN Subject sub ON r.SubjectId  = sub.SubjectId
                WHERE r.StudentId = @Id";

            await using var con = Con();
            await using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", studentId);
            await con.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();

            StudentResultDto? dto = null;
            while (await reader.ReadAsync())
            {
                dto ??= new StudentResultDto
                {
                    StudentName = reader["StudentName"].ToString()!,
                    ClassName = reader["ClassName"].ToString()!
                };
                dto.Subjects.Add(new SubjectMarkDto
                {
                    SubjectName = reader["SubjectName"].ToString()!,
                    Marks = (decimal)reader["Marks"]
                });
            }
            if (dto != null && dto.Subjects.Count > 0)
            {
                dto.Average = Math.Round(dto.Subjects.Average(x => x.Marks), 2);
                dto.Grade = dto.Average >= 90 ? "A+" :
                              dto.Average >= 80 ? "A" :
                              dto.Average >= 70 ? "B" :
                              dto.Average >= 60 ? "C" : "F";
            }
            return dto;
        }

        public async Task<int> CreateAsync(ResultCreateDto dto)
        {
            const string sql = @"
                INSERT INTO Result (StudentId, SubjectId, Marks)
                VALUES (@StudentId, @SubjectId, @Marks);
                SELECT SCOPE_IDENTITY();";
            await using var con = Con();
            await using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@StudentId", dto.StudentId);
            cmd.Parameters.AddWithValue("@SubjectId", dto.SubjectId);
            cmd.Parameters.AddWithValue("@Marks", dto.Marks);
            await con.OpenAsync();
            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }
    }
}