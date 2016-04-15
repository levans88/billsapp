using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using billsapp.Models;

namespace billsapp.Controllers
{
    public class SessionPayerDetailsController : Controller
    {
        private billsappdbEntities db = new billsappdbEntities();

        // GET: SessionPayerDetails
        public ActionResult Index()
        {
            var sessions_payers = db.sessions_payers.Include(s => s.payer).Include(s => s.session);
            return View(sessions_payers.ToList());
        }

        // GET: SessionPayerDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sessions_payers sessions_payers = db.sessions_payers.Find(id);
            if (sessions_payers == null)
            {
                return HttpNotFound();
            }
            return View(sessions_payers);
        }

        // GET: SessionPayerDetails/Create
        public ActionResult Create()
        {
            ViewBag.payer_id = new SelectList(db.payer, "payer_id", "payer_property_01");
            ViewBag.session_id = new SelectList(db.session, "session_id", "session_status");
            return View();
        }

        // POST: SessionPayerDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "balance_starting,balance_remaining,bills_sum,session_id,payer_id,transfer_amount")] sessions_payers sessions_payers)
        {
            if (ModelState.IsValid)
            {
                db.sessions_payers.Add(sessions_payers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.payer_id = new SelectList(db.payer, "payer_id", "payer_property_01", sessions_payers.payer_id);
            ViewBag.session_id = new SelectList(db.session, "session_id", "session_status", sessions_payers.session_id);
            return View(sessions_payers);
        }

        // GET: SessionPayerDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sessions_payers sessions_payers = db.sessions_payers.Find(id);
            if (sessions_payers == null)
            {
                return HttpNotFound();
            }
            ViewBag.payer_id = new SelectList(db.payer, "payer_id", "payer_property_01", sessions_payers.payer_id);
            ViewBag.session_id = new SelectList(db.session, "session_id", "session_status", sessions_payers.session_id);
            return View(sessions_payers);
        }

        // POST: SessionPayerDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "balance_starting,balance_remaining,bills_sum,session_id,payer_id,transfer_amount")] sessions_payers sessions_payers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessions_payers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.payer_id = new SelectList(db.payer, "payer_id", "payer_property_01", sessions_payers.payer_id);
            ViewBag.session_id = new SelectList(db.session, "session_id", "session_status", sessions_payers.session_id);
            return View(sessions_payers);
        }

        // GET: SessionPayerDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sessions_payers sessions_payers = db.sessions_payers.Find(id);
            if (sessions_payers == null)
            {
                return HttpNotFound();
            }
            return View(sessions_payers);
        }

        // POST: SessionPayerDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sessions_payers sessions_payers = db.sessions_payers.Find(id);
            db.sessions_payers.Remove(sessions_payers);
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
