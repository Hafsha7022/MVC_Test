using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Mvc_Test.Models
{
    public class CustomerDetails_Model
    {
        [Display(Name = "Customer Code")]
        public Nullable<int> CustomerId { get; set; }


        [Display(Name = "Address Line-1")]
        [Required( ErrorMessage ="Please Enter AaddressLine-1")]
        public string AddressLine1 { get; set; }


        [Display(Name = "Address Line-2")]
        public string AddressLine2 { get; set; }


        [Required( ErrorMessage = "Please Enter City")]
        public string City { get; set; }


        [Required(ErrorMessage = "Please Enter State")]
        public string State { get; set; }

        [RegularExpression(@"^[ A-Za-z]{3}\s{1}[0-9]{2}[a-zA-Z0-9]{1}$")]
        [Required( ErrorMessage = "Please Enter PinCode")]
        public string PinCode { get; set; }


        [Required(ErrorMessage = "Please Enter PhoneNumber")]
        public int PhoneNumber { get; set; }

        
    }
}