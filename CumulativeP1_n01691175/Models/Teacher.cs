using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CumulativeP1_n01691175.Models
{
    public class Teacher
    {

        //The following fields define a Teacher
        public int TeacherId { get; set; }
        public string TeacherFname { get; set; }
        public string TeacherLname { get; set; }
        public string EmployeeNumber { get; set; }
        public string HireDate { get; set; }
        public double Salary { get; set; }
        public Teacher() { }
    }
}