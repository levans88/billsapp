using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace billsapp.Enum {
    public enum PermissionLevel {
        [Display(Name = "Read")]
        Read = 1,
        [Display(Name = "Modify")]
        Modify = 2,
        [Display(Name = "Full")]
        Full = 3
    }
}