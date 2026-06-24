using DAL.DTOs;
using DAL.Models;
using DAL.Repositories;
namespace BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        public StudentService(IStudentRepository repo) => _repo = repo;

        public Task<IEnumerable<Student>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Student?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<int> CreateAsync(StudentCreateDto dto) => _repo.CreateAsync(dto);
        public Task<bool> UpdateAsync(StudentUpdateDto dto) => _repo.UpdateAsync(dto);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}