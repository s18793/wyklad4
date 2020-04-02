using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wyklad4.DTOs
{
    public class EnrollStudentResponse
    {
        public string LastName { get; set; }

        public int Semester
        {
            get; set;
        }

        public DateTime BeginDate { get; set; }
    }
}
