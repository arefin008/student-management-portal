using DAL.DTOs;
using Microsoft.Data.SqlClient;
namespace DAL.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly string _cs;
        public DashboardRepository(string cs) => _cs = cs;

        public async Task<DashboardStatsDto> GetStatsAsync()
        {
            const string sql = @"
                SELECT
                    (SELECT COUNT(*) FROM Student) AS TotalStudents,
                    (SELECT COUNT(*) FROM Class)   AS TotalClasses,
                    (SELECT COUNT(*) FROM Subject)  AS TotalSubjects,
                    (SELECT COUNT(*) FROM Result)   AS TotalResults";

            await using var con = new SqlConnection(_cs);
            await using var cmd = new SqlCommand(sql, con);
            await con.OpenAsync();
            await using var r = await cmd.ExecuteReaderAsync();
            if (await r.ReadAsync())
                return new DashboardStatsDto
                {
                    TotalStudents = (int)r["TotalStudents"],
                    TotalClasses = (int)r["TotalClasses"],
                    TotalSubjects = (int)r["TotalSubjects"],
                    TotalResults = (int)r["TotalResults"]
                };
            return new DashboardStatsDto();
        }
    }
}