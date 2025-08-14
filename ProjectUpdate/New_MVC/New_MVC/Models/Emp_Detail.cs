using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace New_MVC.Models
{
    public class Emp_Detail
    {
        public int Id { get; set; }
        public bool Gender { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public string Hobbies { get; set; }

        public EmpCountry Emp_Country { get; set; }
        public EmpState Emp_State { get; set; }
        public EmpCity Emp_City { get; set; }
        public List<EmpHobby> Emp_Hobby { get; set; }
    }
}