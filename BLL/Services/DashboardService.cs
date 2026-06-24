using DAL.DTOs;
using DAL.Repositories;
namespace BLL.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repo;
        public DashboardService(IDashboardRepository repo) => _repo = repo;
        public Task<DashboardStatsDto> GetStatsAsync() => _repo.GetStatsAsync();
    }
}