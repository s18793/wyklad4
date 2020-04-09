using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wyklad4.DTOs;
using wyklad4.DTOs.Request;
using wyklad4.DTOs.Response;
using wyklad4.Models;

namespace wyklad4.Services
{
    public interface IStudentDBService
    {
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest esrequest);
        public PromoteStudentResponse PromoteStudent(PromoteStudentRequest psrequest);
        public bool StudentExist(string nrineksu);
    }
}
