using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTOs
{
    public class ResultCreateDto
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public decimal Marks { get; set; }
    }

    public class StudentResultDto
    {
        public string StudentName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public List<SubjectMarkDto> Subjects { get; set; } = new();
        public decimal Average { get; set; }
        public string Grade { get; set; } = string.Empty;
    }

    public class SubjectMarkDto
    {
        public string SubjectName { get; set; } = string.Empty;
        public decimal Marks { get; set; }
    }
}
