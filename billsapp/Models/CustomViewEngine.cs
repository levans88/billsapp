﻿using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;
using System.Web.Mvc;

namespace billsapp.Models {
    public class CustomViewEngine : RazorViewEngine {

        private static string[] NewPartialViewFormats = new[] {
        //"~/Views/{1}/Partials/{0}.cshtml",
        "~/Views/Shared/Partials/{0}.cshtml"
    };

        public CustomViewEngine() {
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(NewPartialViewFormats).ToArray();
        }

    }
}