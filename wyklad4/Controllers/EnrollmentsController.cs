using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using wyklad4.DTOs;
using wyklad4.DTOs.Request;
using wyklad4.DTOs.Response;
using wyklad4.Models;
using wyklad4.Services;

namespace wyklad4.Controllers
{


    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        readonly IStudentDBService iDB;
        public EnrollmentsController(IStudentDBService idb)
        {
            iDB = idb;
        }
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest req)
        {
            EnrollStudentResponse erep = new EnrollStudentResponse();

            erep = iDB.EnrollStudent(req);
            return Created("Added", erep);
        }
        [HttpPost("promotoion")]

        public IActionResult PromoteStudent(PromoteStudentRequest preq)
        {
            PromoteStudentResponse pres = new PromoteStudentResponse();

            pres = iDB.PromoteStudent(preq);
            return Created("promote", pres);
        }

    } 
}



            /*
            var response = new EnrollStudentResponse();
            response.LastName = st.LastName;
           
            if (request.FirstName == null || request.LastName == null || request.IndexNumber == null
               || request.BirthDay == null || request.Studies == null)
            {
                return BadRequest("Nie zostały podane wszystkie wartości");
            }
              
            
   
            return Ok();
            
        }

    }
}*/