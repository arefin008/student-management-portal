using DAL.DTOs;
using DAL.Models;
namespace BLL.Services
{
    public interface IResultService
    {
        Task<IEnumerable<Result>> GetAllAsync();
        Task<StudentResultDto?> GetByStudentIdAsync(int studentId);
        Task<int> CreateAsync(ResultCreateDto dto);
    }
}