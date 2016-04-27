using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace billsapp.Models {
    public class BillTypesViewModel {

        //[Required] ???
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string payment_method_name { get; set; }

        public string payment_method_abbreviation { get; set; }

        public int status_id { get; set; }

        [Required]
        [Display(Name = "Template Name")]
        public string bill_type { get; set; }

        [Display(Name = "URL")]
        public string url { get; set; }

        // Required but we will insert $0 if it's missing
        []
        [Display(Name = "amount_due")]
        public string amount_due { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}