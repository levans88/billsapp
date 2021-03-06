﻿using System;
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
    public class SessionController : Controller
    {
        private billsappdbEntities db = new billsappdbEntities();

        // GET: Session
        public ActionResult Index()
        {
            // Get the current user's payer_id
            var userID = User.Identity.GetUserId();
            var payer = db.payer.Single(p => p.user_id == userID);
            var payerID = payer.payer_id;

            // Retrieve session data from database for the payer
            var sessions = db.sessions_payers.Where(x => x.payer_id == payerID).ToList();
            
            // Assign data to ViewModel
            SessionPayersViewModel model = new SessionPayersViewModel();
            model.sessions = sessions;
            
            return View(model);
        }

        // GET: Session/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Get the current user's payer_id
            var userID = User.Identity.GetUserId();
            var payer = db.payer.Single(p => p.user_id == userID);
            var payerID = payer.payer_id;

            // Retrieve session data from database for the payer
            var session = db.sessions_payers.Where(x => x.session_id == id).ToList();

            if (session == null)
            {
                return HttpNotFound();
            }

            // Get payers for list box
            //List<SelectListItem> multiSelectPayers = new List<SelectListItem>();

            var userIDs = session.Select(x => x.payer.user_id).ToList();

            // Get user details from user table where user.Id == payer.user_id (for userID's in this session)
            var multiSelectPayers = db.AspNetUsers.Select(x => new { x.Id, x.first_name, x.last_name, x.Email }).Where(x => userIDs.Contains(x.Id)).ToList().Select(x => new SelectListItem { Text = x.first_name + " " + x.last_name.Truncate(1) + ".", Value = x.Id}).ToList();

            // Get user info for payers in session
            var users = db.AspNetUsers.Where(x => userIDs.Contains(x.Id)).ToList();

            var payerIDs = session.Select(x => x.payer_id).ToList();
            var payers = db.payer.Where(x => payerIDs.Contains(x.payer_id)).ToList();

            // Get payment methods for all users in session for list box
            //List<SelectListItem> paymentMethods = new List<SelectListItem>();

            //var pmtMth = db.sessions_payers.Join(db.payment_method, sp => sp.payer_id, pm => pm.payer_id, (sp, pm) => new { paymentMethodID = pm.payment_method_id, paymentMethodName = pm.payment_method_name, payerID = sp.payer_id, sessionID = sp.session_id}).Where(x => x.sessionID == id).ToList();

            //var paymentMethods = pmtMth.Select(x => new SelectListItem { Text = x.paymentMethodName, Value = x.paymentMethodID.ToString()}).ToList();

            //var pmtsum = db.payment.Include(x => x.bills_payments).Where(x => x.payme) //Where(x => x.bills_payments.).Sum(x => x.payment_amount)

            var paymentMethods = db.payment_method.Where(x => payerIDs.Contains(x.payer_id)).ToList();

            SessionPayersViewModel model = new SessionPayersViewModel();
            model.userID = userID;
            model.session = session;
            model.multiSelectPayers = multiSelectPayers;
            model.users = users;
            model.payers = payers;
            model.paymentMethods = paymentMethods;

            return View(model);
        }

        // GET: Session/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Session/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "session_id,session_date,session_total_spent,session_status")] session session)
        {
            if (ModelState.IsValid)
            {
                db.session.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(session);
        }

        // GET: Session/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            session session = db.session.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Session/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "session_id,session_date,session_total_spent,session_status")] session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(session);
        }

        // GET: Session/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            session session = db.session.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Session/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            session session = db.session.Find(id);
            db.session.Remove(session);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
