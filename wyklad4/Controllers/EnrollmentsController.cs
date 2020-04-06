using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using wyklad4.DTOs;
using wyklad4.Models;
using wyklad4.Services;

namespace wyklad4.Controllers
{


    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18793;Integrated Security=True"))
            using (var com = new SqlCommand())
            using (var tran = con.BeginTransaction())
            {
                con.Open();
                com.Connection = con;
                com.Transaction = tran;

                var dr = com.ExecuteReader();
                try
                {
                    //czy studia isntijea
                    com.CommandText = "select IdStudies from studies where name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);

                    if (!dr.Read())
                    {
                        dr.Close();
                        tran.Rollback();
                        return BadRequest("Nie ma takich stud");
                    }

                    dr.Close();
                    int idStudies = (int)dr["Studies"];

                    com.CommandText = "Select  enr.IdEnrollment, enr.Semester,enr.IdStudy From Enrollment enr inner join Studies s on enr.IdStudy=s.IdStudy where enr.semester=1 and s.Name=@name";
                    int idEnrollment;


                    dr = com.ExecuteReader();

                    if (!dr.Read())
                    {
                        idEnrollment = 1;
                        dr.Close();
                    }
                    else
                    {

                        com.CommandText = "select Max(IdEnrollment) As maxEnroll from Enrollment";
                        idEnrollment = (int)dr["maxEnroll"];
                        com.CommandText = "insert into enrollment (IdEnrollment,semester,Idstudy,startdate)" + "values(IdEnrollment,1,@idstudy,@date)";

                        dr.Close();
                    }

                    com.Parameters.AddWithValue("IdStudies", request.Studies);
                    com.Parameters.AddWithValue("date", DateTime.Now.ToString());

                    com.ExecuteNonQuery();


                    var enrollRepso = new EnrollStudentResponse
                    {
                        IdEnrollment = idEnrollment,
                        Semester = 1,
                        Name = dr["name"].ToString(),
                        StartDate = DateTime.Parse(dr["date"].ToString())


                    };
                    dr.Close();


                    com.CommandText = "Insert into Student(IndexNumber, Firstname, lastname, birthday, studies,semester,IdEnrollment) values(@Index,@fname,@lname,@bday,@stud,@IdEnrollment)";
                    com.Parameters.AddWithValue("index", request.IndexNumber);
                    com.Parameters.AddWithValue("fname", request.FirstName);
                    com.Parameters.AddWithValue("lname", request.LastName);
                    com.Parameters.AddWithValue("bday", request.BirthDay);
                    com.Parameters.AddWithValue("stud", request.Studies);
                    com.Parameters.AddWithValue("IdEnrollment", idEnrollment);
                    com.ExecuteNonQuery();
                    com.Transaction.Commit();
                    return Ok(enrollRepso);
                }
                catch (SqlException exc)
                {

                    tran.Rollback();
                    return BadRequest("!!");
                }

            }

        }

        public IActionResult PromoteStudent() { 
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