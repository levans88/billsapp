using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using billsapp.Enum;
using System.Web.Mvc;

namespace billsapp.Models {
    public class WizardViewModel {

        public string ShareTargetEmail { get; set; }
        public List<payers_permissions> Permissions { get; set; }
        public Permission Permission { get; set; }
        public PermissionLevel PermissionLevel { get; set; }
        
        public string Note { get; set; }
        
        public List<SelectListItem> BillTypes { get; set; }

        public decimal StartingBalance { get; set; }

        public List<SelectListItem> PaymentMethodSelectList { get; set; }
        public List<payment_method> PaymentMethods { get; set; }
        public payment_method PaymentMethod { get; set; }
        public int? PaymentMethodID { get; set; }

        public int PayerID { get; set; }

        public bool Auto { get; set; }
        public bool Task { get; set; }

        // [Required] but will default to monthly
        public BillTypeFrequency Frequency { get; set; }
        //[EnumDataType(typeof(Enum.BillTypeFrequency), ErrorMessage = "Invalid frequency selection.")]
        //public int frequency_id { get; set; }

        //public Status Status { get; set; }

        public bool DuplicatePaymentMethodName { get; set; }
        public bool DuplicatePaymentMethodAbbreviation { get; set; }
        public bool PaymentMethodUsedByTemplate { get; set; }
        public bool PaymentMethodUsedByPayment { get; set; }

        public int StatusID { get; set; }

        [Required]
        [Display(Name = "Status")]
        [EnumDataType(typeof(Enum.Status), ErrorMessage = "Invalid status selection.")]
        public Status Status { get; set; }

        [Required]
        [Display(Name = "Payment Method Name")]
        [StringLength(45, ErrorMessage = "Payment method name must be between {2} and {1} characters long.", MinimumLength = 1)]
        public string PaymentMethodName { get; set; }

        [Required]
        [Display(Name = "Payment Method Abbreviation")]
        [StringLength(4, ErrorMessage = "Payment method abbreviation must be fewer than {1} characters long.")]
        public string PaymentMethodAbbreviation { get; set; }

        ////[Required]
        [Display(Name = "Template Name")]
        [StringLength(45, ErrorMessage = "Template name must be between {2} and {1} characters long.", MinimumLength = 1)]
        public string TypeName { get; set; }

        // not [Required]
        [Display(Name = "URL")]
        [DataType(DataType.Url)]
        public string Url { get; set; }

        // [Required] but we will just insert $0 if it's missing
        [Display(Name = "Amount Due")]
        [DataType(DataType.Currency)]
        public string AmountDue { get; set; }

        ////[Required]
        [DataType(DataType.Date)]
        public DateTime DateDue { get; set; }

        // [Required] but will default to the 1st of the month if not selected
        //[Display(Name = "Day Due")]
        //public int DayDue { get; set; }

        // [Required] only if yearly
        //[Display(Name = "Month Due")]
        //public int MonthDue { get; set; }

        // [Required] with frequency less than a year
        //[Display(Name = "Year Due")]
        //public int YearDue { get; set; }
    }
}