using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wyklad4.DTOs
{
    public class EnrollStudentRequest
    {
        public string IndexNumber { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
        public string Studies { get; set; }
    }
}
