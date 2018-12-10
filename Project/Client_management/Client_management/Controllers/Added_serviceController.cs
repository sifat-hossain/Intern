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
    public class Added_serviceController : Controller
    {
        private Client_ManagementEntities db = new Client_ManagementEntities();

        // GET: Added_service
        public ActionResult Index()
        {
            var tblAdded_service = db.tblAdded_service.Include(t => t.tblClient_profile).Include(t => t.tblCompany_services);
            return View(tblAdded_service.ToList());
        }

        // GET: Added_service/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAdded_service tblAdded_service = db.tblAdded_service.Find(id);
            if (tblAdded_service == null)
            {
                return HttpNotFound();
            }
            return View(tblAdded_service);
        }

        // GET: Added_service/Create
        public ActionResult Create()
        {
            ViewBag.Client_id = new SelectList(db.tblClient_profile, "ID", "Company_name");
            ViewBag.Service_id = new SelectList(db.tblCompany_services, "ID", "C_service_name");
            return View();
        }

        // POST: Added_service/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Client_id,Service_id,Quantity,Payable_amount,Added_date")] tblAdded_service tblAdded_service)
        {
            if (ModelState.IsValid)
            {
                tblAdded_service.Added_date = DateTime.Now;
                db.tblAdded_service.Add(tblAdded_service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Client_id = new SelectList(db.tblClient_profile, "ID", "Company_name", tblAdded_service.Client_id);
            ViewBag.Service_id = new SelectList(db.tblCompany_services, "ID", "C_service_name", tblAdded_service.Service_id);
            return View(tblAdded_service);
        }

        // GET: Added_service/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAdded_service tblAdded_service = db.tblAdded_service.Find(id);
            if (tblAdded_service == null)
            {
                return HttpNotFound();
            }
            ViewBag.Client_id = new SelectList(db.tblClient_profile, "ID", "Company_name", tblAdded_service.Client_id);
            ViewBag.Service_id = new SelectList(db.tblCompany_services, "ID", "C_service_name", tblAdded_service.Service_id);
            return View(tblAdded_service);
        }

        // POST: Added_service/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Client_id,Service_id,Quantity,Payable_amount,Added_date")] tblAdded_service tblAdded_service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblAdded_service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Client_id = new SelectList(db.tblClient_profile, "ID", "Company_name", tblAdded_service.Client_id);
            ViewBag.Service_id = new SelectList(db.tblCompany_services, "ID", "C_service_name", tblAdded_service.Service_id);
            return View(tblAdded_service);
        }

        // GET: Added_service/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAdded_service tblAdded_service = db.tblAdded_service.Find(id);
            if (tblAdded_service == null)
            {
                return HttpNotFound();
            }
            return View(tblAdded_service);
        }

        // POST: Added_service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblAdded_service tblAdded_service = db.tblAdded_service.Find(id);
            db.tblAdded_service.Remove(tblAdded_service);
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
