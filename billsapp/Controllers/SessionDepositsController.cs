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
    public class SessionDepositsController : Controller
    {
        private billsappdbEntities db = new billsappdbEntities();

        // GET: SessionDeposits
        public ActionResult Index()
        {
            var session_deposit = db.session_deposit.Include(s => s.payer).Include(s => s.session);
            return View(session_deposit.ToList());
        }

        // GET: SessionDeposits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            session_deposit session_deposit = db.session_deposit.Find(id);
            if (session_deposit == null)
            {
                return HttpNotFound();
            }
            return View(session_deposit);
        }

        // GET: SessionDeposits/Create
        public ActionResult Create()
        {
            ViewBag.session_id = new SelectList(db.sessions_payers, "session_id", "session_id");
            return View();
        }

        // POST: SessionDeposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "deposit_id,session_id,payer_id,deposit_amount,deposit_note,deposit_date")] session_deposit session_deposit)
        {
            if (ModelState.IsValid)
            {
                db.session_deposit.Add(session_deposit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.session_id = new SelectList(db.sessions_payers, "session_id", "session_id", session_deposit.session_id);
            return View(session_deposit);
        }

        // GET: SessionDeposits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            session_deposit session_deposit = db.session_deposit.Find(id);
            if (session_deposit == null)
            {
                return HttpNotFound();
            }
            ViewBag.session_id = new SelectList(db.sessions_payers, "session_id", "session_id", session_deposit.session_id);
            return View(session_deposit);
        }

        // POST: SessionDeposits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "deposit_id,session_id,payer_id,deposit_amount,deposit_note,deposit_date")] session_deposit session_deposit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session_deposit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.session_id = new SelectList(db.sessions_payers, "session_id", "session_id", session_deposit.session_id);
            return View(session_deposit);
        }

        // GET: SessionDeposits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            session_deposit session_deposit = db.session_deposit.Find(id);
            if (session_deposit == null)
            {
                return HttpNotFound();
            }
            return View(session_deposit);
        }

        // POST: SessionDeposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            session_deposit session_deposit = db.session_deposit.Find(id);
            db.session_deposit.Remove(session_deposit);
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
