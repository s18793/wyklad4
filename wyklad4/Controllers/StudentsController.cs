﻿using System;
using System.Collections.Generic;
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
                com.CommandText = "select * from students";

                con.Open();
               SqlDataReader dr= com.ExecuteReader();
                while (dr.Read())
                {
                    var st
                }
            }

            



         
            return Ok(list);
        }
    }
}