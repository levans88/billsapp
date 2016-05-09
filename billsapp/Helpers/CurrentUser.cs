using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using billsapp.Enum;
using System.ComponentModel;
using System.Web.Mvc;
using billsapp.Models;
using Microsoft.AspNet.Identity;

namespace System.Web.Mvc {
    public class CurrentUser {
        private billsappdbEntities db = new billsappdbEntities();

        // Properties and default values
        public string UserID;
        public payer Payer;
        public int PayerID;

        // CurrentUser class constructor
        public CurrentUser () {
            UserID = HttpContext.Current.User.Identity.GetUserId();
            Payer = db.payer.Single(p => p.user_id == UserID);
            PayerID = Payer.payer_id;
        }
    }
}