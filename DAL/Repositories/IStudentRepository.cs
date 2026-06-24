using DAL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<int> CreateAsync(StudentCreateDto dto);
        Task<bool> UpdateAsync(StudentUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
