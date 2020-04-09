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


namespace wyklad4.Services
{
    public class SqlServerDbService : IStudentDBService
    {

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
           
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18793;Integrated Security=True"))
            using (var com = new SqlCommand())
            using (var tran = con.BeginTransaction())
            {
                EnrollStudentResponse enrollRepso = new EnrollStudentResponse();



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
                      //  return OkResult(400 + "Nie ma takich stud");
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

                        
                    }
                    dr.Close();

                    com.Parameters.AddWithValue("IdStudies", request.Studies);
                    com.Parameters.AddWithValue("date", DateTime.Now.ToString());

                    com.ExecuteNonQuery();

                    com.CommandText = "Insert into Student(IndexNumber, Firstname, lastname, birthday, studies,semester,IdEnrollment) values(@Index,@fname,@lname,@bday,@stud,@IdEnrollment)";
                    com.Parameters.AddWithValue("index", request.IndexNumber);
                    com.Parameters.AddWithValue("fname", request.FirstName);
                    com.Parameters.AddWithValue("lname", request.LastName);
                    com.Parameters.AddWithValue("bday", request.BirthDay);
                    com.Parameters.AddWithValue("stud", request.Studies);
                    com.Parameters.AddWithValue("IdEnrollment", idEnrollment);
                    com.ExecuteNonQuery();





                   enrollRepso.IdEnrollment = idEnrollment;
                     enrollRepso.Semester = 1;
                    enrollRepso.Name = dr["name"].ToString();
                    enrollRepso.StartDate = DateTime.Parse(dr["date"].ToString());


                  
                    tran.Commit();
                   

                    
                }
                catch (SqlException exc)
                {

                    tran.Rollback();
                    
                }

                return enrollRepso;

            }

        }
      
        public PromoteStudentResponse PromoteStudent(PromoteStudentRequest psrequest)
        {
            
            using (SqlConnection con = new SqlConnection("Data Source = db - mssql; Initial Catalog = s18793; Integrated Security = True"))
            using (SqlCommand com = new SqlCommand())
            using (var tran = con.BeginTransaction())
            {
                PromoteStudentResponse response = new PromoteStudentResponse();
                con.Open();
                com.Connection = con;
                com.Transaction = tran;
                try
                {


                    com.CommandText = "exec PromoteStudents @name, @semester";
                    com.Parameters.AddWithValue("semester", psrequest.Semester);
                    com.Parameters.AddWithValue("name", psrequest.Name);
                    com.ExecuteNonQuery();


                    com.CommandText = "SELECT * FROM Enrollment e INNER JOIN Studies stud ON stud.idstudy = e.idstudy WHERE e.semester = Semest AND stud.name = stName";
                    com.Parameters.AddWithValue("stName", psrequest.Name);
                    com.Parameters.AddWithValue("Semest", psrequest.Semester);
                    var dr = com.ExecuteReader();


                    if (!dr.Read())
                    {
                        dr.Close();
                        tran.Rollback();


                    }
                    else
                    {

                        response.IdEnrollment = (int)dr["IdEnrollment"];
                        response.IdStudy = dr["IdStudy"].ToString();
                        response.Semester = dr["Semester"].ToString();
                        response.StartDate = (DateTime)dr["StartDate"];


                    }






                }
                catch (SqlException e)
                {
                    tran.Rollback();

                }

                return response;
            }

            
        }

        public bool StudentExist(string nrineksu)
        {
            bool isExist = false;

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18793;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                
                com.Connection = con;
                com.CommandText = "SELECT indexNumber FROM Student WHERE indexNumber = @indexNumber; ";
                com.Parameters.AddWithValue("indexNumber", nrineksu);

                con.Open();
                var dr = com.ExecuteReader();
                
                if (dr.Read())
                {
                    isExist = true;
                }


            }

            return isExist;
        }

    }

}
