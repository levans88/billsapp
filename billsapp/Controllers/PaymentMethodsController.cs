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
    public class PaymentMethodsController : Controller
    {
        private billsappdbEntities db = new billsappdbEntities();

        // GET: PaymentMethods
        public ActionResult Index()
        {
            var payment_method = db.payment_method.Include(p => p.payer).Include(p => p.status);
            return View(payment_method.ToList());
        }

        // GET: PaymentMethods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment_method payment_method = db.payment_method.Find(id);
            if (payment_method == null)
            {
                return HttpNotFound();
            }
            return View(payment_method);
        }

        // GET: PaymentMethods/Create
        public ActionResult Create()
        {
            ViewBag.payer_id = new SelectList(db.payer, "payer_id", "payer_firstname");
            ViewBag.status_id = new SelectList(db.status, "status_id", "status_description");
            return View();
        }

        // POST: PaymentMethods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "payment_method_id,payer_id,payment_method_name,payment_method_abbreviation,status_id")] payment_method payment_method)
        {
            if (ModelState.IsValid)
            {
                db.payment_method.Add(payment_method);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.payer_id = new SelectList(db.payer, "payer_id", "payer_firstname", payment_method.payer_id);
            ViewBag.status_id = new SelectList(db.status, "status_id", "status_description", payment_method.status_id);
            return View(payment_method);
        }

        // GET: PaymentMethods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment_method payment_method = db.payment_method.Find(id);
            if (payment_method == null)
            {
                return HttpNotFound();
            }
            ViewBag.payer_id = new SelectList(db.payer, "payer_id", "payer_firstname", payment_method.payer_id);
            ViewBag.status_id = new SelectList(db.status, "status_id", "status_description", payment_method.status_id);
            return View(payment_method);
        }

        // POST: PaymentMethods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "payment_method_id,payer_id,payment_method_name,payment_method_abbreviation,status_id")] payment_method payment_method)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment_method).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.payer_id = new SelectList(db.payer, "payer_id", "payer_firstname", payment_method.payer_id);
            ViewBag.status_id = new SelectList(db.status, "status_id", "status_description", payment_method.status_id);
            return View(payment_method);
        }

        // GET: PaymentMethods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment_method payment_method = db.payment_method.Find(id);
            if (payment_method == null)
            {
                return HttpNotFound();
            }
            return View(payment_method);
        }

        // POST: PaymentMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            payment_method payment_method = db.payment_method.Find(id);
            db.payment_method.Remove(payment_method);
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
