using DAL.Models;
using DAL.Repositories;
namespace BLL.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _repo;
        public SubjectService(ISubjectRepository repo) => _repo = repo;

        public Task<IEnumerable<Subject>> GetAllAsync() => _repo.GetAllAsync();
        public Task<int> CreateAsync(string subjectName) => _repo.CreateAsync(subjectName);
        public Task<bool> UpdateAsync(int id, string subjectName) => _repo.UpdateAsync(id, subjectName);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}