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
    public class tblInvoicesController : Controller
    {
        private Client_ManagementEntities db = new Client_ManagementEntities();

        // GET: tblInvoices
        public ActionResult Index()
        {
            var tblInvoices = db.tblInvoices.Include(t => t.tblAdded_service);
            return View(tblInvoices.ToList());
        }

        // GET: tblInvoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblInvoice tblInvoice = db.tblInvoices.Find(id);
            if (tblInvoice == null)
            {
                return HttpNotFound();
            }
            return View(tblInvoice);
        }

        // GET: tblInvoices/Create
        public ActionResult Create()
        {
            ViewBag.Added_service_id = new SelectList(db.tblAdded_service, "ID", "ID");
            return View();
        }

        // POST: tblInvoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Invoice_no,Added_service_id,Payment_status,Payment_type,Due,Date")] tblInvoice tblInvoice)
        {
            if (ModelState.IsValid)
            {
                db.tblInvoices.Add(tblInvoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Added_service_id = new SelectList(db.tblAdded_service, "ID", "ID", tblInvoice.Added_service_id);
            return View(tblInvoice);
        }

        // GET: tblInvoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblInvoice tblInvoice = db.tblInvoices.Find(id);
            if (tblInvoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.Added_service_id = new SelectList(db.tblAdded_service, "ID", "ID", tblInvoice.Added_service_id);
            return View(tblInvoice);
        }

        // POST: tblInvoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Invoice_no,Added_service_id,Payment_status,Payment_type,Due,Date")] tblInvoice tblInvoice, int PaidAmount)
        {
            if (ModelState.IsValid)
            {
                int status = Convert.ToInt32(tblInvoice.Due - PaidAmount);
                if (status == 0)
                {
                    tblInvoice.Payment_status = 1;
                    tblInvoice.Due = 0;
                }
                else
                {
                    tblInvoice.Payment_status = 0;
                    tblInvoice.Due = status;
                }
                db.Entry(tblInvoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Added_service_id = new SelectList(db.tblAdded_service, "ID", "ID", tblInvoice.Added_service_id);
            return View(tblInvoice);
        }

        // GET: tblInvoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblInvoice tblInvoice = db.tblInvoices.Find(id);
            if (tblInvoice == null)
            {
                return HttpNotFound();
            }
            return View(tblInvoice);
        }

        // POST: tblInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblInvoice tblInvoice = db.tblInvoices.Find(id);
            db.tblInvoices.Remove(tblInvoice);
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
