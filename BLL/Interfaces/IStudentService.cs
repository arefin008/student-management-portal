using DAL.DTOs;
using DAL.Models;
namespace BLL.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<int> CreateAsync(StudentCreateDto dto);
        Task<bool> UpdateAsync(StudentUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}