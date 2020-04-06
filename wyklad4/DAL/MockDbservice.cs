﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wyklad4.DAL
{
    public class MockDbservice : IDbService
    {
        private static IEnumerable<Student> _students;
        static MockDbservice()
        {

            _students = new List<Student> {

                new Student{IdStudent=1, FirstName="Jan", LastName="Kowalski" },
                new Student{IdStudent=2, FirstName="Anna", LastName="Malwjeska" },
                new Student{IdStudent=3, FirstName="Andrzej", LastName="Andrzejewicz" }

            };

        }
        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }


    }
}
