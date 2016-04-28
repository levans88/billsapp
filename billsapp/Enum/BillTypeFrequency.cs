using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace billsapp.Enum {
    public enum BillTypeFrequency {
        [Display(Name = "Monthly")]
        Monthly = 1,
        [Display(Name = "Yearly")]
        Yearly = 2,
        [Display(Name = "Once")]
        Once = 3,
        [Display(Name = "As Needed")]
        AsNeeded = 4
    }
}