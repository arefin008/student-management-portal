using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public interface IClassRepository
    {
        Task<IEnumerable<Class>> GetAllAsync();
        Task<int> CreateAsync(string className);
        Task<bool> UpdateAsync(int id, string className);
        Task<bool> DeleteAsync(int id);
    }
}
