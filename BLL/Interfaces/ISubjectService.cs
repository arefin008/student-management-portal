using DAL.Models;
namespace BLL.Services
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetAllAsync();
        Task<int> CreateAsync(string subjectName);
        Task<bool> UpdateAsync(int id, string subjectName);
        Task<bool> DeleteAsync(int id);
    }
}