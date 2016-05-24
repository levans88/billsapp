using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace billsapp.Models {
    public class SessionPayersViewModel {
        
        // List of records in a specific session
        public List<sessions_payers> session { get; set; }

        // List of sessions for a specific payer
        public List<sessions_payers> sessions { get; set; }

        public List<SelectListItem> multiSelectPayers { get; set; }

        public List<AspNetUsers> users { get; set; }
        public List<payer> payers { get; set; }

        public string userID { get; set; }

        public List<payment_method> paymentMethods { get; set; }
        //public List<SelectListItem> paymentMethods { get; set; }

        public bool paid { get; set; }
        public decimal paidAmount { get; set; }

        [Required]
        [Display(Name = "Payment Method")]
        public int paymentMethod { get; set; }

        private int _payerCount;
        public int payerCount {
            get {
                _payerCount = session.Count();
                return _payerCount;
            }
            set {
                _payerCount = value;
            }
        }
    }
}