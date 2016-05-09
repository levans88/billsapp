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

            //model.PaymentMethodID = 1;

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
                //List<SelectListItem> paymentMethodSelectList = new List<SelectListItem>();
                //paymentMethodSelectList = db.payment_method.Where(x => x.payer_id == payerID).ToList().Select(x => new SelectListItem {
                //    Text = x.payment_method_name,
                //    Value = x.payment_method_id.ToString()
                //}).ToList();

                // Get new regular list of payment methods (non-select)
                var paymentMethods = db.payment_method.Where(x => x.payer_id == payerID).ToList();

                // Update model with new payment methods
                //model.PaymentMethodSelectList = paymentMethodSelectList;
                model.PaymentMethods = paymentMethods;

                return PartialView("_WizardStep1", model);
            }
            else {

                // Get the current user's payer_id
                var userID = User.Identity.GetUserId();
                var payer = db.payer.Single(p => p.user_id == userID);
                var payerID = payer.payer_id;

                if (paymentMethodID.HasValue) {
                    var existingPaymentMethodName = db.payment_method.FirstOrDefault(x => x.payer_id == payerID && x.payment_method_name == model.PaymentMethodName && x.payment_method_id != paymentMethodID);

                    var existingPaymentMethodAbbreviation = db.payment_method.FirstOrDefault(x => x.payer_id == payerID && x.payment_method_abbreviation == model.PaymentMethodAbbreviation && x.payment_method_id != paymentMethodID);

                    if (existingPaymentMethodName != null) {
                        ModelState.AddModelError("DuplicatePaymentMethodName", "Payment method names must be unique.");

                        // Get new regular list of payment methods (non-select)
                        var paymentMethods = db.payment_method.Where(x => x.payer_id == payerID).ToList();

                        // Update model with payment methods
                        model.PaymentMethods = paymentMethods;

                        return PartialView("_WizardStep1", model);
                    }
                    else if (existingPaymentMethodAbbreviation != null) {
                        ModelState.AddModelError("DuplicatePaymentMethodAbbreviation", "Payment method abbreviation must be unique.");

                        // Get new regular list of payment methods (non-select)
                        var paymentMethods = db.payment_method.Where(x => x.payer_id == payerID).ToList();

                        // Update model with payment methods
                        model.PaymentMethods = paymentMethods;

                        return PartialView("_WizardStep1", model);
                    }
                    else {

                        var paymentMethod = db.payment_method.Single(x => x.payer_id == payerID && paymentMethodID == x.payment_method_id);

                        // Update payment_method object fields
                        paymentMethod.payment_method_name = model.PaymentMethodName;
                        paymentMethod.payment_method_abbreviation = model.PaymentMethodAbbreviation;
                        paymentMethod.status_id = (int)model.Status;

                        db.Entry(paymentMethod).State = EntityState.Modified;
                        db.SaveChanges();

                        // Clear update form
                        ModelState.Clear();
                        model.PaymentMethodID = null;
                        model.PaymentMethodAbbreviation = null;
                        model.PaymentMethodName = null;
                        model.Status = billsapp.Enum.Status.Active;

                        var paymentMethods = db.payment_method.Where(x => x.payer_id == payerID).ToList();
                        model.PaymentMethods = paymentMethods;

                        return PartialView("_WizardStep1", model);
                    }
                }
                else {
                    var existingPaymentMethodName = db.payment_method.FirstOrDefault(x => x.payer_id == payerID && x.payment_method_name == model.PaymentMethodName);

                    var existingPaymentMethodAbbreviation = db.payment_method.FirstOrDefault(x => x.payer_id == payerID && x.payment_method_abbreviation == model.PaymentMethodAbbreviation);

                    if (existingPaymentMethodName != null) {
                        ModelState.AddModelError("DuplicatePaymentMethodName", "Payment method names must be unique.");

                        // Get new regular list of payment methods (non-select)
                        var paymentMethods = db.payment_method.Where(x => x.payer_id == payerID).ToList();

                        // Update model with payment methods
                        model.PaymentMethods = paymentMethods;

                        return PartialView("_WizardStep1", model);
                    }
                    else if (existingPaymentMethodAbbreviation != null) {
                        ModelState.AddModelError("DuplicatePaymentMethodAbbreviation", "Payment method abbreviation must be unique.");

                        // Get new regular list of payment methods (non-select)
                        var paymentMethods = db.payment_method.Where(x => x.payer_id == payerID).ToList();

                        // Update model with payment methods
                        model.PaymentMethods = paymentMethods;

                        return PartialView("_WizardStep1", model);
                    }
                    else {
                        // Create payment_method object for writing
                        var paymentMethod = new payment_method();
                        paymentMethod.payer_id = payerID;
                        paymentMethod.payment_method_name = model.PaymentMethodName;
                        paymentMethod.payment_method_abbreviation = model.PaymentMethodAbbreviation;
                        paymentMethod.status_id = (int)model.Status;
                        db.payment_method.Add(paymentMethod);
                        db.SaveChanges();

                        // Get new payment methods select list
                        //List<SelectListItem> paymentMethodSelectList = new List<SelectListItem>();
                        //paymentMethodSelectList = db.payment_method.Where(x => x.payer_id == payerID).ToList().Select(x => new SelectListItem {
                        //    Text = x.payment_method_name,
                        //    Value = x.payment_method_id.ToString()
                        //}).ToList();

                        // Clear update form
                        ModelState.Clear();
                        model.PaymentMethodID = null;
                        model.PaymentMethodAbbreviation = null;
                        model.PaymentMethodName = null;
                        model.Status = billsapp.Enum.Status.Active;

                        // Get new regular list of payment methods (non-select)
                        var paymentMethods = db.payment_method.Where(x => x.payer_id == payerID).ToList();

                        // Update model with new payment methods
                        //model.PaymentMethodSelectList = paymentMethodSelectList;
                        model.PaymentMethods = paymentMethods;

                        return PartialView("_WizardStep1", model);
                    }
                }
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

        public ActionResult WizardEditPaymentMethod(int paymentMethodID) {

            // Get the current user's payer_id
            var userID = User.Identity.GetUserId();
            var payer = db.payer.Single(p => p.user_id == userID);
            var payerID = payer.payer_id;

            // Get a payment method object given ID
            var paymentMethod = db.payment_method.Single(x => x.payer_id == payerID && x.payment_method_id == paymentMethodID);

            // Regather payment methods for model for reloading partial
            var paymentMethods = db.payment_method.Where(x => x.payer_id == payerID).ToList();

            var model = new WizardViewModel();
            model.PaymentMethods = paymentMethods;
            model.PaymentMethodID = paymentMethod.payment_method_id;
            model.PaymentMethodName = paymentMethod.payment_method_name;
            model.PaymentMethodAbbreviation = paymentMethod.payment_method_abbreviation;
            model.Status = (billsapp.Enum.Status)paymentMethod.status_id;

            return PartialView("_WizardStep1", model);
        }

        public ActionResult WizardCancelForm(string partial) {
            var model = new WizardViewModel();

            // Get the current user's payer_id
            var userID = User.Identity.GetUserId();
            var payer = db.payer.Single(p => p.user_id == userID);
            var payerID = payer.payer_id;

            // Clear update form
            ModelState.Clear();
            model.PaymentMethodID = null;
            model.PaymentMethodAbbreviation = null;
            model.PaymentMethodName = null;
            model.Status = billsapp.Enum.Status.Active;

            var paymentMethods = db.payment_method.Where(x => x.payer_id == payerID).ToList();
            model.PaymentMethods = paymentMethods;

            return PartialView("_WizardStep1", model);
        }

        public ActionResult WizardDeletePaymentMethod(int paymentMethodID) {
            // Get the current user's payer_id
            var userID = User.Identity.GetUserId();
            var payer = db.payer.Single(p => p.user_id == userID);
            var payerID = payer.payer_id;

            // Get a payment method object given ID
            var paymentMethod = db.payment_method.Single(x => x.payer_id == payerID && x.payment_method_id == paymentMethodID);

            // Delete
            db.payment_method.Remove(paymentMethod);
            db.SaveChanges();

            // Regather payment methods for model for reloading partial
            var paymentMethods = db.payment_method.Where(x => x.payer_id == payerID).ToList();

            var model = new WizardViewModel();
            model.PaymentMethods = paymentMethods;            

            return PartialView("_WizardStep1", model);
        }
    }
}