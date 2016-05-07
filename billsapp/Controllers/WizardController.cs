using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using billsapp.Models;
using Microsoft.AspNet.Identity;

namespace billsapp.Controllers
{
    public class WizardController : Controller
    {
        private billsappdbEntities db = new billsappdbEntities();

        // GET: Wizard
        public ActionResult Index() {

            // Get the current user's payer_id
            var userID = User.Identity.GetUserId();
            var payer = db.payer.Single(p => p.user_id == userID);
            var payerID = payer.payer_id;

            // Get payment methods
            List<SelectListItem> paymentMethods = new List<SelectListItem>();

            paymentMethods = db.payment_method.Where(x => x.payer_id == payerID).ToList().Select(x => new SelectListItem {
                Text = x.payment_method_name,
                Value = x.payment_method_id.ToString()
            }).ToList();

            // Get bill types for list box
            List<SelectListItem> billTypes = new List<SelectListItem>();

            billTypes = db.bill_type.Where(x => x.payer_id == payerID).ToList().Select(x => new SelectListItem {
                Text = x.type_name,
                Value = x.type_id.ToString()
            }).ToList();

            // Get permission settings
            var permissions = db.payers_permissions.Include(x => x.permission).Include(x => x.permission_level).Where(x => x.source_payer_id == payerID).ToList();

            // Create and populate ViewModel
            var model = new WizardViewModel();
            model.PaymentMethods = paymentMethods;
            model.BillTypes = billTypes;
            model.Permissions = permissions;

            return View(model);
        }

        // POST: Wizard payment methods
        [HttpPost]
        public ActionResult WizardAddEditPaymentMethod(WizardViewModel model, int? paymentMethodID) {
            if (!ModelState.IsValid) {
                return PartialView("_WizardStep1", model);
            }
            else {

                if (paymentMethodID.HasValue) {
                    var paymentMethod = db.payment_method.Single(x => x.payment_method_id == paymentMethodID);
                    db.Entry(paymentMethod).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else {
                    var paymentMethod = model.PaymentMethod;
                    db.payment_method.Add(paymentMethod);
                    db.SaveChanges();
                }

                return PartialView("_WizardStep1", model);
            }
        }
    }
}