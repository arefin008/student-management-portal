using DAL.Models;
namespace BLL.Services
{
    public interface IClassService
    {
        Task<IEnumerable<Class>> GetAllAsync();
        Task<int> CreateAsync(string className);
        Task<bool> UpdateAsync(int id, string className);
        Task<bool> DeleteAsync(int id);
    }
}