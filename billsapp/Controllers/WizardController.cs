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
    public class WizardController : Controller {
        private billsappdbEntities db = new billsappdbEntities();

        // GET: Wizard
        public ActionResult Index() {

            // Get the current user's payer_id
            var userID = User.Identity.GetUserId();
            var payer = db.payer.Single(p => p.user_id == userID);
            var payerID = payer.payer_id;

            // Get payment methods select list
            List<SelectListItem> paymentMethodSelectList = new List<SelectListItem>();
            paymentMethodSelectList = db.payment_method.Where(x => x.payer_id == payerID).ToList().Select(x => new SelectListItem {
                Text = x.payment_method_name,
                Value = x.payment_method_id.ToString()
            }).ToList();

            // Get a regular list of payment methods (non-select)
            var paymentMethods = db.payment_method.Where(x => x.payer_id == payerID).ToList();

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
            model.PaymentMethodSelectList = paymentMethodSelectList;
            model.PaymentMethods = paymentMethods;
            model.BillTypes = billTypes;
            model.Permissions = permissions;
            model.Status = Enum.Status.Active;

            return View(model);
        }

        // POST: Wizard payment methods
        [HttpPost]
        [ValidateOnlyIncomingValues]
        public ActionResult WizardAddEditPaymentMethod(WizardViewModel model, int? paymentMethodID) {
            //if (!ModelState.IsValidField("PaymentMethodName") || !ModelState.IsValidField("PaymentMethodAbbreviation") || !ModelState.IsValidField("Status")) {
            if (!ModelState.IsValid) {

                //var errors = ModelState.Where(a => a.Value.Errors.Count > 0)
                //.Select(b => new { b.Key, b.Value.Errors })
                //.ToArray();

                // Get the current user's payer_id
                var userID = User.Identity.GetUserId();
                var payer = db.payer.Single(p => p.user_id == userID);
                var payerID = payer.payer_id;

                // Repopulate the portion of the model needed for rendering the partial
                // (and anything that has changed)
                //
                // Get new payment methods select list
                List<SelectListItem> paymentMethodSelectList = new List<SelectListItem>();
                paymentMethodSelectList = db.payment_method.Where(x => x.payer_id == payerID).ToList().Select(x => new SelectListItem {
                    Text = x.payment_method_name,
                    Value = x.payment_method_id.ToString()
                }).ToList();

                // Get new regular list of payment methods (non-select)
                var paymentMethods = db.payment_method.Where(x => x.payer_id == payerID).ToList();

                // Update model with new payment methods
                model.PaymentMethodSelectList = paymentMethodSelectList;
                model.PaymentMethods = paymentMethods;

                return PartialView("_WizardStep1", model);
            }
            else {

                if (paymentMethodID.HasValue) {
                    var paymentMethod = db.payment_method.Single(x => x.payment_method_id == paymentMethodID);
                    db.Entry(paymentMethod).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else {
                    var paymentMethod = new payment_method();

                    // Get the current user's payer_id
                    var userID = User.Identity.GetUserId();
                    var payer = db.payer.Single(p => p.user_id == userID);
                    var payerID = payer.payer_id;

                    // Create payment_method object for writing
                    paymentMethod.payer_id = payerID;
                    paymentMethod.payment_method_name = model.PaymentMethodName;
                    paymentMethod.payment_method_abbreviation = model.PaymentMethodAbbreviation;
                    paymentMethod.status_id = (int)model.Status;
                    db.payment_method.Add(paymentMethod);
                    db.SaveChanges();

                    // Get new payment methods select list
                    List<SelectListItem> paymentMethodSelectList = new List<SelectListItem>();
                    paymentMethodSelectList = db.payment_method.Where(x => x.payer_id == payerID).ToList().Select(x => new SelectListItem {
                        Text = x.payment_method_name,
                        Value = x.payment_method_id.ToString()
                    }).ToList();

                    // Get new regular list of payment methods (non-select)
                    var paymentMethods = db.payment_method.Where(x => x.payer_id == payerID).ToList();

                    // Update model with new payment methods
                    model.PaymentMethodSelectList = paymentMethodSelectList;
                    model.PaymentMethods = paymentMethods;
                }

                return PartialView("_WizardStep1", model);
            }
        }

        //[HttpPost]
        public ActionResult RefreshPaymentMethods() {
            var model = new WizardViewModel();

            // Get the current user's payer_id
            var userID = User.Identity.GetUserId();
            var payer = db.payer.Single(p => p.user_id == userID);
            var payerID = payer.payer_id;

            // Get new payment methods select list
            List<SelectListItem> paymentMethodSelectList = new List<SelectListItem>();
            paymentMethodSelectList = db.payment_method.Where(x => x.payer_id == payerID).ToList().Select(x => new SelectListItem {
                Text = x.payment_method_name,
                Value = x.payment_method_id.ToString()
            }).ToList();

            // Update model with new payment methods
            model.PaymentMethodSelectList = paymentMethodSelectList;

            return PartialView("_PaymentMethodFormGroup", model);
        }
    }
}