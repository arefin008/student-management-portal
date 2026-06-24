using DAL.DTOs;
using DAL.Models;
using DAL.Repositories;
namespace BLL.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _repo;
        public ResultService(IResultRepository repo) => _repo = repo;

        public Task<IEnumerable<Result>> GetAllAsync() => _repo.GetAllAsync();
        public Task<StudentResultDto?> GetByStudentIdAsync(int sid) => _repo.GetByStudentIdAsync(sid);
        public Task<int> CreateAsync(ResultCreateDto dto) => _repo.CreateAsync(dto);
    }
}