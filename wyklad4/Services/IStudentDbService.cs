using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wyklad4.DTOs;
using wyklad4.Models;

namespace wyklad4.Services
{
    interface IStudentDbService
    {
        // void EnrollStudent(EnrollStudentRequest request);
        //  void PromoteStudents(int semester, string studies);

        Enrollment EnrollStudent(EnrollStudentRequest student);
        Enrollment PromoteStudents(int semester, string studies);
    }
}
