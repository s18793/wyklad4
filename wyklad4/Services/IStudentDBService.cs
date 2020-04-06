using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wyklad4.DTOs;
using wyklad4.Models;

namespace wyklad4.Services
{
    public interface IStudentDBService
    {
        public IActionResult EnrollStudent(EnrollStudentRequest student);
        public IActionResult PromoteStudents(int semester, string studies);
    }
}
