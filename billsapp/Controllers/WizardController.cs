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
        int payerID = new CurrentUser().PayerID;

        // GET: Wizard
        public ActionResult Index() {
            var model = GetPaymentMethods();

            // Get bill types for list box
            var billTypes = db.bill_type.Where(x => x.payer_id == payerID).ToList().Select(x => new SelectListItem {
                Text = x.type_name,
                Value = x.type_id.ToString()
            }).ToList();

            // Get permission settings
            var permissions = db.payers_permissions.Include(x => x.permission).Include(x => x.permission_level).Where(x => x.source_payer_id == payerID).ToList();

            model.BillTypes = billTypes;
            model.Permissions = permissions;
            model.Status = Enum.Status.Active;

            return View(model);
        }

        // POST: Wizard payment methods
        [HttpPost]
        [ValidateOnlyIncomingValues]
        public ActionResult AddEditPaymentMethod(WizardViewModel model, int? paymentMethodID) {
            if (!ModelState.IsValid) {
                model = GetPaymentMethods();

                return PartialView("_WizardStep1", model);
            }
            else {
                // Check for duplicates
                if (paymentMethodID.HasValue) {
                    model = CheckForDuplicatePaymentMethod(model, paymentMethodID);
                }
                else {
                    model = CheckForDuplicatePaymentMethod(model);
                }

                if (model.DuplicatePaymentMethodAbbreviation) {
                    ModelState.AddModelError("DuplicatePaymentMethodAbbreviation", "Payment method abbreviation must be unique.");
                }

                if (model.DuplicatePaymentMethodName) {
                    ModelState.AddModelError("DuplicatePaymentMethodName", "Payment method names must be unique.");
                }

                if (model.DuplicatePaymentMethodName || model.DuplicatePaymentMethodAbbreviation) {
                    model = GetPaymentMethods();

                    return PartialView("_WizardStep1", model);
                }

                if (paymentMethodID.HasValue) {
                    // Store the input fields from the current model
                    var paymentMethodName = model.PaymentMethodName;
                    var paymentMethodAbbreviation = model.PaymentMethodAbbreviation;
                    var paymentMethodStatus = (int)model.Status;

                    // Get new model with specific payment method
                    model = GetPaymentMethods(paymentMethodID);
                    var paymentMethod = model.PaymentMethod;
                    
                    // Put input field values on existing payment method
                    paymentMethod.payment_method_name = paymentMethodName;
                    paymentMethod.payment_method_abbreviation = paymentMethodAbbreviation;
                    paymentMethod.status_id = paymentMethodStatus;

                    // Write
                    db.Entry(paymentMethod).State = EntityState.Modified;
                    db.SaveChanges();

                    ModelState.Clear();
                    model = GetPaymentMethods();

                    return PartialView("_WizardStep1", model);
                }
                else {
                    // Create payment_method object for writing
                    var paymentMethod = new payment_method();
                    paymentMethod.payer_id = payerID;
                    paymentMethod.payment_method_name = model.PaymentMethodName;
                    paymentMethod.payment_method_abbreviation = model.PaymentMethodAbbreviation;
                    paymentMethod.status_id = (int)model.Status;

                    // Write
                    db.payment_method.Add(paymentMethod);
                    db.SaveChanges();

                    ModelState.Clear();
                    model = GetPaymentMethods();

                    return PartialView("_WizardStep1", model);
                }
            }
        }

        public ActionResult RefreshPaymentMethods() {
            var model = GetPaymentMethods();

            return PartialView("_PaymentMethodFormGroup", model);
        }

        public ActionResult EditPaymentMethod(int paymentMethodID) {
            // Get single payment method
            var model = GetPaymentMethods(paymentMethodID);
            var paymentMethod = model.PaymentMethod;

            // Get all payment methods
            model = GetPaymentMethods();

            // Restore single payment method to model
            model.PaymentMethodID = paymentMethod.payment_method_id;
            model.PaymentMethodName = paymentMethod.payment_method_name;
            model.PaymentMethodAbbreviation = paymentMethod.payment_method_abbreviation;
            model.Status = (billsapp.Enum.Status)paymentMethod.status_id;

            return PartialView("_WizardStep1", model);
        }

        public ActionResult CancelForm(string partial) {
            var model = new WizardViewModel();
            model = GetPaymentMethods();

            return PartialView(partial, model);
        }

        public ActionResult DeletePaymentMethod(int paymentMethodID) {
            // Check if payment method is in use
            var usedByTemplate = db.bill_type.FirstOrDefault(x => x.payment_method_id == paymentMethodID);
            var usedByPayment = db.payment.FirstOrDefault(x => x.payment_method_id == paymentMethodID);

            if (usedByTemplate != null) {
                var model = GetPaymentMethods();
                ModelState.AddModelError("PaymentMethodUsedByTemplate", "Payment method is currently in use by a template and cannot be removed.");
                model.PaymentMethodUsedByTemplate = true;

                return PartialView("_WizardStep1", model);
            }
            else if (usedByPayment != null) {
                var model = GetPaymentMethods();
                ModelState.AddModelError("PaymentMethodUsedByPayment", "Payment method has been used to pay a previous bill and cannot be removed.");
                model.PaymentMethodUsedByPayment = true;

                return PartialView("_WizardStep1", model);
            }
            else {
                // Get specific payment method
                var model = GetPaymentMethods(paymentMethodID);
                var paymentMethod = model.PaymentMethod;

                // Delete the payment method
                db.payment_method.Remove(paymentMethod);
                db.SaveChanges();

                // Regather payment methods for model / partial
                model = GetPaymentMethods();

                return PartialView("_WizardStep1", model);
            }
        }

        private WizardViewModel CheckForDuplicatePaymentMethod(WizardViewModel model, int? paymentMethodID = null) {
            var existingPaymentMethodName = new payment_method();
            var existingPaymentMethodAbbreviation = new payment_method();

            if (paymentMethodID.HasValue) {
                // Exclude the current payment method when checking for duplicates
                existingPaymentMethodName = db.payment_method.FirstOrDefault(x => x.payer_id == payerID && x.payment_method_name == model.PaymentMethodName && x.payment_method_id != model.PaymentMethodID);

                existingPaymentMethodAbbreviation = db.payment_method.FirstOrDefault(x => x.payer_id == payerID && x.payment_method_abbreviation == model.PaymentMethodAbbreviation && x.payment_method_id != model.PaymentMethodID);
            }
            else {
                // There is no specific payment method, so just check for any duplicate
                existingPaymentMethodName = db.payment_method.FirstOrDefault(x => x.payer_id == payerID && x.payment_method_name == model.PaymentMethodName);

                existingPaymentMethodAbbreviation = db.payment_method.FirstOrDefault(x => x.payer_id == payerID && x.payment_method_abbreviation == model.PaymentMethodAbbreviation);
            }

            if (existingPaymentMethodName != null) {                
                model.DuplicatePaymentMethodName = true;
            }

            if (existingPaymentMethodAbbreviation != null) {
                model.DuplicatePaymentMethodAbbreviation = true;
            }

            return model;
        }

        private WizardViewModel GetPaymentMethods(int? paymentMethodID = null) {
            // Get payment method if given a specific ID
            if (paymentMethodID.HasValue) {
                var paymentMethod = db.payment_method.Single(x => x.payer_id == payerID && x.payment_method_id == paymentMethodID);

                var model = new WizardViewModel();
                model.PaymentMethod = paymentMethod;
                model.PaymentMethodID = paymentMethod.payment_method_id;
                model.PaymentMethodName = paymentMethod.payment_method_name;
                model.PaymentMethodAbbreviation = paymentMethod.payment_method_abbreviation;
                model.Status = (billsapp.Enum.Status)paymentMethod.status_id;

                return model;
            }
            else {
                // Get payment methods
                var paymentMethods = db.payment_method.Where(x => x.payer_id == payerID).ToList();

                // Get payment methods for select list
                var paymentMethodSelectList = db.payment_method.Where(x => x.payer_id == payerID && x.status_id != (int)Enum.Status.Disabled).ToList().Select(x => new SelectListItem {
                    Text = x.payment_method_name,
                    Value = x.payment_method_id.ToString()
                }).ToList();

                var model = new WizardViewModel();
                model.PaymentMethods = paymentMethods;
                model.PaymentMethodSelectList = paymentMethodSelectList;
                model.Status = billsapp.Enum.Status.Active;

                return model;
            }
        }
    }
}