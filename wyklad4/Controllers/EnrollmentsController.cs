using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wyklad4.DTOs;

namespace wyklad4.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request) {
            //Dtos - przerzucenie jakis danych miedzy 2 pkt


            if (request.FirstName == null || request.LastName == null || request.IndexNumber==null
               || request.Birthday==null || request.Studies==null)
            {
                return BadRequest("Nie zostały podane wszystkie wartości");
            }

            var st=new Student();
            st.FirstName = request.FirstName;
            st.LastName = request.LastName;
            st.IndexNumber = request.IndexNumber;
            st.Birthday = request.Birthday;
            st.Studies = request.Studies;


            var response = new EnrollStudentResponse();
            response.LastName = st.LastName;




            return Ok();
        }

    }
}