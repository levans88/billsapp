using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace billsapp.Models {
    public class SessionPayersViewModel {
        
        // List of records in a specific session
        public List<sessions_payers> session { get; set; }

        // List of sessions for a specific payer
        public List<sessions_payers> sessions { get; set; }

        public List<SelectListItem> payers { get; set; }

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