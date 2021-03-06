﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace wyklad4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

        private const string ConString = "Data Source=db-mssql;Initial Catalog=s18793;Integrated Security=True";
        [HttpGet]
        public IActionResult GetStudents()
        {
            /*
            var conBuilder = new SqlConnectionStringBuilder();
            conBuilder.InitialCatalog = "s18793";

            string conStr = conBuilder.ConnectionString;
            */



            var list = new List<Student>();

            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select FirstName,LastName,Birthdate, Studies.name, Enrollment.semester from Enrollment,student, studies  where Enrollment.IdEnrollment = student.IdEnrollment AND Enrollment.IdStudy = studies.IdStudy";

                

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.Birthdate = DateTime.Parse(dr["Birthdate"].ToString());
                    st.Studies = dr["name"].ToString();
                    st.Semester = (int)dr["Semester"];
                    list.Add(st);
                }
                con.Dispose();
            }






            return Ok(list);
        }




         [HttpGet("{NrIndeksu}")]
        public IActionResult GetStudent(string NrIndeksu)
        {
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from student where indexnumber=@index";
              

                com.Parameters.AddWithValue("index", NrIndeksu);

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();

                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    return Ok(st);
                }

            }

            return NotFound();                    //{"type":"https://tools.ietf.org/html/rfc7231#section-6.5.4","title":"Not Found","status":404,"traceId":"|73f283ec-438686a053a309b9."}
        }
        /*
        //
        [HttpGet("roll")]
        public IActionResult GetStudents2()
        {
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "insert into Student(FirstName) values (@firstName)";


                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    int affectedRows = com.ExecuteNonQuery();

                    com.CommandText = "update into Students whem student where NrIndeksu = @indeks";


                //re nrindeksu==0";
                    com.ExecuteNonQuery();

                   
                    transaction.Commit();
                }
                catch (Exception exc)
                {
                    transaction.Rollback();
                }

            }

            return Ok();
        }
        */
    }
}