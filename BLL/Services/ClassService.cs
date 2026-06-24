using DAL.Models;
using DAL.Repositories;
namespace BLL.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _repo;
        public ClassService(IClassRepository repo) => _repo = repo;

        public Task<IEnumerable<Class>> GetAllAsync() => _repo.GetAllAsync();
        public Task<int> CreateAsync(string className) => _repo.CreateAsync(className);
        public Task<bool> UpdateAsync(int id, string className) => _repo.UpdateAsync(id, className);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}