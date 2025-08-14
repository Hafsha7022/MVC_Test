using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mvc_Test.Models
{
    public class CustomerItem_Model
    {
        [Display(Name = "Item No.")]
        public Nullable<int> ItemId { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [Display(Name = "ItemName")]
        public string Item { get; set; }

        [Required(ErrorMessage = "Please Enter Quantity")]
        [Display(Name = "Quantity")]
        public int Qty { get; set; }


        [Required(ErrorMessage = "Please Enter Rate")]
        [Display(Name = "Rate")]
        public float Rate { get; set; }

        [Required(ErrorMessage = "Please Enter Amount")]
        [Display(Name = "Amount")]
        public float Amount { get; set; }
    }
}