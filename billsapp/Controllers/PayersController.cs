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
    public class PayersController : Controller
    {
        private billsappdbEntities db = new billsappdbEntities();

        // GET: Payers
        public ActionResult Index()
        {
            return View(db.payer.ToList());
        }

        // GET: Payers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payer payer = db.payer.Find(id);
            if (payer == null)
            {
                return HttpNotFound();
            }
            return View(payer);
        }

        // GET: Payers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "payer_id,payer_firstname,payer_lastname")] payer payer)
        {
            if (ModelState.IsValid)
            {
                db.payer.Add(payer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(payer);
        }

        // GET: Payers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payer payer = db.payer.Find(id);
            if (payer == null)
            {
                return HttpNotFound();
            }
            return View(payer);
        }

        // POST: Payers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "payer_id,payer_firstname,payer_lastname")] payer payer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payer);
        }

        // GET: Payers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payer payer = db.payer.Find(id);
            if (payer == null)
            {
                return HttpNotFound();
            }
            return View(payer);
        }

        // POST: Payers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            payer payer = db.payer.Find(id);
            db.payer.Remove(payer);
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
