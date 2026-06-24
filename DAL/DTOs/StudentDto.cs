using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTOs
{
    public class StudentCreateDto
    {
        public string StudentName { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
        public string? Phone { get; set; }
        public int ClassId { get; set; }
    }
    public class StudentUpdateDto: StudentCreateDto
    {
        public int StudentId { get; set; }
    }
}
