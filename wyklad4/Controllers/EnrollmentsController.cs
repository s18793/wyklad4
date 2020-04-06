using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using wyklad4.DTOs;
using wyklad4.Models;

namespace wyklad4.Controllers
{
    [Route("api/enrollments")]
    [ApiController] //-> implict model validation
    public class EnrollmentsController : ControllerBase
    {

        private const string ConString = "Data Source=db-mssql;Initial Catalog=s18793;Integrated Security=True";
        [HttpPost]

        public IActionResult EnrollStudent(EnrollStudentRequest request) {
            //Dtos - przerzucenie jakis danych miedzy 2 pkt


            var st = new Student();

            st.FirstName = request.FirstName;
            st.LastName = request.LastName;
            st.IndexNumber = request.IndexNumber;
            st.Birthday = request.BirthDay;
            st.Studies = request.Studies;


            int idEnrollment;
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();
                try {
                    //czy studia isntijea
                    com.CommandText = "select IdStudies from studies where name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);
                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        return BadRequest("Nie ma takich stud");
                    }
                    int idStudies = (int)dr["Studies"];

                    dr.Close();

                    com.CommandText = "Select  enr.IdEnrollment, enr.Semester,enr.IdStudy From Enrollment enr inner join Studies s on enr.IdStudy=s.IdStudy where enr.semester=1 and s.Name=@name";
                    
                    com.CommandText = "select Max(IdEnrollment) As maxEnroll from Enrollment";
                    
                    dr = com.ExecuteReader();

                    if (!dr.Read())
                    {
                        idEnrollment = 1;
                        dr.Close();
                    }
                    else { 
                        com.CommandText = "insert into enrollment (IdEnrollment,semester,Idstudy,startdate)" + "values(IdEnrollment,1,@idstudy,@date)";
                        com.Parameters.AddWithValue("IdStudies", request.Studies);
                        com.Parameters.AddWithValue("date", DateTime.Now.ToString());
                        idEnrollment = (int)dr["IdEnrollment"];
                        dr = com.ExecuteReader();

                        dr.Close();
                    }
                    
                  

                    com.CommandText = "Insert into Student(IndexNumber, Firstname, lastname, birthday, studies,semester,IdEnrollment) values(@Index,@fname,@lname,@bday,@stud,@IdEnrollment)";
                    com.Parameters.AddWithValue("index", request.IndexNumber);
                    com.Parameters.AddWithValue("fname", request.FirstName);
                    com.Parameters.AddWithValue("lname", request.LastName);
                    com.Parameters.AddWithValue("bday", request.BirthDay);
                    com.Parameters.AddWithValue("stud", request.Studies);
                    com.Parameters.AddWithValue("IdEnrollment", idEnrollment);
                    com.ExecuteNonQuery();
                }catch(SqlException exc)
                {
                    tran.Rollback();
                }

                }


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
}