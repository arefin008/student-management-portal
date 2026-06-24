using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalStudents { get; set; }
        public int TotalClasses { get; set; }
        public int TotalSubjects { get; set; }
        public int TotalResults { get; set; }
    }
}
