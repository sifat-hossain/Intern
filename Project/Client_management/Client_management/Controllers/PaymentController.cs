using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Client_management.Models;

namespace Client_management.Controllers
{
    public class PaymentController : Controller
    {
        private Client_ManagementEntities db = new Client_ManagementEntities();

        // GET: Payment
        public ActionResult Index()
        {
            var tblClient_transaction = db.tblClient_transaction.Include(t => t.tblClient_profile);
            return View(tblClient_transaction.ToList());
        }

        // GET: Payment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClient_transaction tblClient_transaction = db.tblClient_transaction.Find(id);
            if (tblClient_transaction == null)
            {
                return HttpNotFound();
            }
            return View(tblClient_transaction);
        }

       [HttpGet]
        public ActionResult Create(int id)
        {
            
            ViewBag.Client_id = db.tblClient_profile.Where(p => p.ID == id).FirstOrDefault().ID;
            ViewBag.Client_Name = db.tblClient_profile.Where(p => p.ID == id).FirstOrDefault().Company_name;
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Client_id,Invoice_id,Paid_amount,Paid_date")] tblClient_transaction tblClient_transaction)
        {
            
            if (ModelState.IsValid)
            {
                db.tblClient_transaction.Add(tblClient_transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

          
            return View(tblClient_transaction);
        }

        // GET: Payment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClient_transaction tblClient_transaction = db.tblClient_transaction.Find(id);
            if (tblClient_transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.Client_id = new SelectList(db.tblClient_profile, "ID", "Company_name", tblClient_transaction.Client_id);
            return View(tblClient_transaction);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Client_id,Invoice_id,Paid_amount,Paid_date")] tblClient_transaction tblClient_transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblClient_transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Client_id = new SelectList(db.tblClient_profile, "ID", "Company_name", tblClient_transaction.Client_id);
            return View(tblClient_transaction);
        }

        // GET: Payment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClient_transaction tblClient_transaction = db.tblClient_transaction.Find(id);
            if (tblClient_transaction == null)
            {
                return HttpNotFound();
            }
            return View(tblClient_transaction);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblClient_transaction tblClient_transaction = db.tblClient_transaction.Find(id);
            db.tblClient_transaction.Remove(tblClient_transaction);
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
