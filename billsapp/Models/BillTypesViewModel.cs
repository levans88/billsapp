using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using billsapp.Enum;
using System.Web.Mvc;

namespace billsapp.Models {
    public class BillTypesViewModel {

        public int payer_id { get; set; }
        public int type_id { get; set; }
   
        public List<SelectListItem> paymentMethods { get; set; }

        // not [Required]
        [Display(Name = "Payment Method")]
        public int paymentMethod { get; set; }
        //public int payment_method_id { get; set; }        

        public bool auto { get; set; }
        public bool task { get; set; }

        // [Required] but will default to monthly
        public BillTypeFrequency frequency { get; set; }
        //[EnumDataType(typeof(Enum.BillTypeFrequency), ErrorMessage = "Invalid frequency selection.")]
        //public int frequency_id { get; set; }
        
        // [Required] but we will default to active
        [Display(Name = "Status")]
        [EnumDataType(typeof(Enum.Status), ErrorMessage = "Invalid status selection.")]
        public int status_id { get; set; }

        [Required]
        [Display(Name = "Payment Method Name")]
        [StringLength(45, ErrorMessage = "Payment method name must be between {2} and {1} characters long.", MinimumLength = 1)]
        public string payment_method_name { get; set; }

        [Required]
        [Display(Name = "Payment Method Abbreviation")]
        [StringLength(45, ErrorMessage = "Payment method abbreviation must be between {2} and {1} characters long.", MinimumLength = 4)]
        public string payment_method_abbreviation { get; set; }

        [Required]
        [Display(Name = "Template Name")]
        [StringLength(45, ErrorMessage = "Template name must be between {2} and {1} characters long.", MinimumLength = 1)]
        public string type_name { get; set; }

        // not [Required]
        [Display(Name = "URL")]
        [DataType(DataType.Url)]
        public string url { get; set; }

        // [Required] but we will just insert $0 if it's missing
        [Display(Name = "Amount Due")]
        [DataType(DataType.Currency)]
        public string amount_due { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime dateDue { get; set; }

        // [Required] but will default to the 1st of the month if not selected
        //[Display(Name = "Day Due")]
        //public int day_due { get; set; }

        // [Required] only if yearly
        //[Display(Name = "Month Due")]
        //public int month_due { get; set; }

        // [Required] with frequency less than a year
        //[Display(Name = "Year Due")]
        //public int year_due { get; set; }
    }
}