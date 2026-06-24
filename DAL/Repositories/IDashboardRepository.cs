using DAL.DTOs;
namespace DAL.Repositories
{
    public interface IDashboardRepository
    {
        Task<DashboardStatsDto> GetStatsAsync();
    }
}