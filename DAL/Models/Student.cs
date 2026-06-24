using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public int ClassId { get; set; }
        public string? ClassName { get; set; }   // for JOIN queries
    }
}
