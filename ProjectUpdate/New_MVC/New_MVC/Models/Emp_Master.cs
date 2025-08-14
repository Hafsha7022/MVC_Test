using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_Test.Models
{
    public class Emp_Master
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string PhoneNumber { get; set; }

        public Employee EmpDetail { get; set; }
    }
}