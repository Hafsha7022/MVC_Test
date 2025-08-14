using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Mvc_Test.Models
{
    public class CustomerMaster_Model
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        public Nullable<int> CustomerCode { get; set; }

        [Required]
        [Display(Name = "TotalQuantity")]
        public int TotalQty { get; set; }

        [Required]
        [Display(Name = "GrossAmt")]
        public float GrossAmt { get; set; }

        [Required]
        [Display(Name = "Discount")]
        public float DiscPer { get; set; }

        [Required]
        [Display(Name = "DiscountAmount")]
        public float DiscAmt { get; set; }

        [Required]
        [Display(Name = "NetAmount")]
        public float NetAmt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MM, yy}")]
        public DateTime SatrtDate { get; set; }
        public int Days { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MM, yy}")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        
        public string NewSatrtDate { get; set; }
        public int NewDays { get; set; }


        public string NewEndDate { get; set; }

        public List<CustomerDetails_Model> CustomerDetails { get; set; }
        public List<CustomerItem_Model> CustomerItems { get; set; }
    }
}