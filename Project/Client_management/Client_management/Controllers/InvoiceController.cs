using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using Client_management.Models;
using PagedList;

namespace Client_management.Controllers
{
    public class InvoiceController : Controller
    {
        private Client_ManagementEntities db = new Client_ManagementEntities();

        tblClient_transaction tblClient_Transaction = new tblClient_transaction();
        
        // GET: Invoice
        public ActionResult Index(int? page, int? pageSize)
        {
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int defaultSize = (pageSize ?? 5);
            ViewBag.pagesize = defaultSize;
            ViewBag.PageSize = new List<SelectListItem>()
         {
             new SelectListItem() { Value="5", Text= "5" },
             new SelectListItem() { Value="10", Text= "10" },
             new SelectListItem() { Value="15", Text= "15" },
             new SelectListItem() { Value="25", Text= "25" },
             new SelectListItem() { Value="50", Text= "50" },
         };
            
            IPagedList<tblInvoice> involst = null;
            involst = db.tblInvoices.OrderBy(p => p.ID).ToPagedList(pageIndex, defaultSize);
            //var Invoice = db.tblInvoices.Include(i => i.tblAdded_service);

          
            return View(involst);
        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Update(int? id)
        {
            tblInvoice tblInvoice = db.tblInvoices.Find(id);
            ViewBag.Added_service_id = new SelectList(db.tblAdded_service, "ID", "ID", tblInvoice.Added_service_id);
            return View(tblInvoice);
        }
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include ="ID,Invoice_no,Added_service_id,Payment_status,Payment_type,Due,Date")]tblInvoice tblInvoice, int PaidAmount)
        {
            if(ModelState.IsValid)
            {
                int status =Convert.ToInt32(tblInvoice.Due - PaidAmount);
                if(status==0)
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
                //db.SaveChanges();

                var ClientId = db.tblAdded_service.Where(s => s.ID == tblInvoice.Added_service_id).FirstOrDefault();
                tblClient_Transaction.Client_id = ClientId.Client_id;
                tblClient_Transaction.Invoice_id = tblInvoice.ID;
                tblClient_Transaction.Paid_amount = PaidAmount;
                tblClient_Transaction.Paid_date = DateTime.Now;
                tblClient_Transaction.Payment_type = tblInvoice.Payment_type;
                db.tblClient_transaction.Add(tblClient_Transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Added_service_id = new SelectList(db.tblAdded_service, "ID", "ID", tblInvoice.Added_service_id);
            return View(tblInvoice);
        }
       
    }
}