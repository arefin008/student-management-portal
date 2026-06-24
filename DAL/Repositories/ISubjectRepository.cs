using DAL.Models;
namespace DAL.Repositories
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllAsync();
        Task<int> CreateAsync(string subjectName);
        Task<bool> UpdateAsync(int id, string subjectName);
        Task<bool> DeleteAsync(int id);
    }
}