using DAL.DTOs;
using DAL.Models;
namespace DAL.Repositories
{
    public interface IResultRepository
    {
        Task<IEnumerable<Result>> GetAllAsync();
        Task<StudentResultDto?> GetByStudentIdAsync(int studentId);
        Task<int> CreateAsync(ResultCreateDto dto);
    }
}