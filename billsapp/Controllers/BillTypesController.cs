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
    public class BillTypesController : Controller
    {
        private billsappdbEntities db = new billsappdbEntities();

        // GET: BillTypes
        public ActionResult Index()
        {
            var model = new BillTypesViewModel();
            var billTypes = db.bill_type.Include(b => b.frequency).Include(b => b.payer).Include(b => b.payment_method).Include(b => b.status).ToList();
            return View(billTypes);
            
        }

        // GET: Wizard
        public ActionResult Wizard() {
            var bill_type = db.bill_type.Include(b => b.frequency).Include(b => b.payer).Include(b => b.payment_method).Include(b => b.status);
            return View(bill_type.ToList());
        }

        // GET: BillTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill_type bill_type = db.bill_type.Find(id);
            if (bill_type == null)
            {
                return HttpNotFound();
            }
            return View(bill_type);
        }

        // GET: BillTypes/Create
        public ActionResult Create()
        {
            ViewBag.frequency_id = new SelectList(db.frequency, "frequency_id", "frequency_name");
            ViewBag.payer_id = new SelectList(db.payer, "payer_id", "payer_property_01");
            ViewBag.payment_method_id = new SelectList(db.payment_method, "payment_method_id", "payment_method_name");
            ViewBag.status_id = new SelectList(db.status, "status_id", "status_description");
            return View();
        }

        // POST: BillTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "type_id,type_name,task,url,day_due,month_due,year_due,auto,amount_due,frequency_id,payment_method_id,payer_id,status_id,bill_type_note")] bill_type bill_type)
        {
            if (ModelState.IsValid)
            {
                db.bill_type.Add(bill_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.frequency_id = new SelectList(db.frequency, "frequency_id", "frequency_name", bill_type.frequency_id);
            ViewBag.payer_id = new SelectList(db.payer, "payer_id", "payer_property_01", bill_type.payer_id);
            ViewBag.payment_method_id = new SelectList(db.payment_method, "payment_method_id", "payment_method_name", bill_type.payment_method_id);
            ViewBag.status_id = new SelectList(db.status, "status_id", "status_description", bill_type.status_id);
            return View(bill_type);
        }

        // GET: BillTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill_type bill_type = db.bill_type.Find(id);
            if (bill_type == null)
            {
                return HttpNotFound();
            }
            ViewBag.frequency_id = new SelectList(db.frequency, "frequency_id", "frequency_name", bill_type.frequency_id);
            ViewBag.payer_id = new SelectList(db.payer, "payer_id", "payer_property_01", bill_type.payer_id);
            ViewBag.payment_method_id = new SelectList(db.payment_method, "payment_method_id", "payment_method_name", bill_type.payment_method_id);
            ViewBag.status_id = new SelectList(db.status, "status_id", "status_description", bill_type.status_id);
            return View(bill_type);
        }

        // POST: BillTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "type_id,type_name,task,url,day_due,month_due,year_due,auto,amount_due,frequency_id,payment_method_id,payer_id,status_id,bill_type_note")] bill_type bill_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.frequency_id = new SelectList(db.frequency, "frequency_id", "frequency_name", bill_type.frequency_id);
            ViewBag.payer_id = new SelectList(db.payer, "payer_id", "payer_property_01", bill_type.payer_id);
            ViewBag.payment_method_id = new SelectList(db.payment_method, "payment_method_id", "payment_method_name", bill_type.payment_method_id);
            ViewBag.status_id = new SelectList(db.status, "status_id", "status_description", bill_type.status_id);
            return View(bill_type);
        }

        // GET: BillTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill_type bill_type = db.bill_type.Find(id);
            if (bill_type == null)
            {
                return HttpNotFound();
            }
            return View(bill_type);
        }

        // POST: BillTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bill_type bill_type = db.bill_type.Find(id);
            db.bill_type.Remove(bill_type);
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
