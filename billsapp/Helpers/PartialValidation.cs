using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using billsapp.Enum;
using System.ComponentModel;

namespace System.Web.Mvc {
    public class ValidateOnlyIncomingValuesAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            var modelState = filterContext.Controller.ViewData.ModelState;
            var valueProvider = filterContext.Controller.ValueProvider;

            var keysWithNoIncomingValue = modelState.Keys.Where(x => !valueProvider.ContainsPrefix(x));
            foreach (var key in keysWithNoIncomingValue)
                modelState[key].Errors.Clear();
        }
    }
}