using DAL.DTOs;
namespace BLL.Services
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetStatsAsync();
    }
}