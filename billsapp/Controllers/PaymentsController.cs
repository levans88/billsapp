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
    public class PaymentsController : Controller
    {
        private billsappdbEntities db = new billsappdbEntities();

        // GET: Payments
        public ActionResult Index()
        {
            var payment = db.payment.Include(p => p.payer);
            return View(payment.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment payment = db.payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.payer_id = new SelectList(db.payer, "payer_id", "payer_firstname");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "payment_id,payment_method_id,payment_amount,payment_date,payer_id")] payment payment)
        {
            if (ModelState.IsValid)
            {
                db.payment.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.payer_id = new SelectList(db.payer, "payer_id", "payer_firstname", payment.payer_id);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment payment = db.payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.payer_id = new SelectList(db.payer, "payer_id", "payer_firstname", payment.payer_id);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "payment_id,payment_method_id,payment_amount,payment_date,payer_id")] payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.payer_id = new SelectList(db.payer, "payer_id", "payer_firstname", payment.payer_id);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment payment = db.payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            payment payment = db.payment.Find(id);
            db.payment.Remove(payment);
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
