using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Result
    {
        public int ResultId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public decimal Marks { get; set; }
        public string? StudentName { get; set; }
        public string? SubjectName { get; set; }
    }
}
