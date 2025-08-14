using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace New_MVC.Models
{
    public class MainEmployee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string PhoneNumber { get; set; }

        public Emp_Detail EmpDetail { get; set; }
        public List<EmpDetail> Detail { get; set; }
    }
}