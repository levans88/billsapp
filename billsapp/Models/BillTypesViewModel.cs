using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace billsapp.Models {
    public class BillTypesViewModel {

        public int payer_id { get; set; }
        public int payment_method_id { get; set; }
        public int type_id { get; set; }

        public bool auto { get; set; }
        public bool task { get; set; }

        // [Required] but we will just default to monthly if missing
        [Display(Name = "Frequency")]
        [EnumDataType(typeof(Enum.BillTypeFrequency), ErrorMessage = "Invalid frequency selection.")]
        public int frequency_id { get; set; }

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
        [Display(Name = "Day Due")]
        public string day_due { get; set; }

        // [Required] only if yearly
        [Display(Name = "Month Due")]
        public string month_due { get; set; }

        // [Required] with frequency less than a year
        [Display(Name = "Year Due")]
        public string year_due { get; set; }
    }
}