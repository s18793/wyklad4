﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wyklad4.DTOs
{
    public class EnrollStudentResponse
    {
        public int IdEnrollment { get; set; }
        public int Semester { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }

       
    }
}
