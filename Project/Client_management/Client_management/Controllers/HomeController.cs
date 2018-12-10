using System;
using System.Collections.Generic;
using System.Linq;
using Rotativa;
using System.Web;
using System.Web.Mvc;
using Client_management.Models;
using Newtonsoft.Json;
using Microsoft.Reporting.WebForms;


namespace Client_management.Controllers
{
    public class HomeController : Controller
    {
        tblInvoice invoice = new tblInvoice();
        //tblClient_transaction tblClient_transaction = new tblClient_transaction();
        tblCompany_services tblCompany_Services = new tblCompany_services();
        tblAdded_service tblAdded_Service = new tblAdded_service();

        Random r = new Random();
        private Client_ManagementEntities db = new Client_ManagementEntities();

        public ActionResult Home()
        {
            var month = 0;

            var invoiceDate = db.tblInvoices.OrderByDescending(p => p.ID).FirstOrDefault();
            if (invoiceDate == null)
            {
                month = 0;
            }
            else
            {
                month = invoiceDate.Date.Value.Month;
            }


            //while (month != DateTime.Now.Month)
            //{
            //    Auto_Invoice();


            //    invoiceDate = db.tblInvoices.OrderByDescending(p => p.ID).FirstOrDefault();
            //    month = invoiceDate.Date.Value.Month;
            //}

            return RedirectToAction("Index", "Invoice");
        }

        //public void Auto_Invoice()
        //{

        //    var serviceData = db.tblAdded_service.ToList();

        //    foreach (var service in serviceData)
        //    {
        //        var serviceCategory = db.tblCompany_services.Where(s => s.ID ==service.Service_id ).FirstOrDefault();
        //        var servicePrice = db.tblCompany_services.Where(s => s.ID == service.Service_id).FirstOrDefault();

        //        invoice.Invoice_no = r.Next(0, 1000000);
        //        invoice.Payment_status = 0;

        //        invoice.Payment_type = serviceCategory.Category;

        //        invoice.Due = service.Quantity * servicePrice.Price;
        //        invoice.Added_service_id = service.ID;
        //        invoice.Date = DateTime.Now;
        //        db.tblInvoices.Add(invoice);
        //        db.SaveChanges();

        //    }

        //}



        public ActionResult Invoice_Detail(int? id)
        {
            var invoice = db.tblInvoices.FirstOrDefault(a => a.ID == id);
            var addedService = db.tblAdded_service.FirstOrDefault(a => a.ID == invoice.Added_service_id);
            var client = db.tblClient_profile.FirstOrDefault(a => a.ID == addedService.Client_id);
            var details = db.tblInvoices.Where(s => s.tblAdded_service.tblClient_profile.ID == client.ID).ToList();
            ViewBag.invoiceId = id;
            return View(details);
        }

        public ActionResult Invoice_print(int? id)
        {
            var invoice = db.tblInvoices.FirstOrDefault(a => a.ID == id);
            var addedService = db.tblAdded_service.FirstOrDefault(a => a.ID == invoice.Added_service_id);
            var client = db.tblClient_profile.FirstOrDefault(a => a.ID == addedService.Client_id);
            var details = db.tblInvoices.Where(s => s.tblAdded_service.tblClient_profile.ID == client.ID).ToList();
            //ViewBag.ClientProfile = client;
            return View(details);
        }
        public ActionResult PDF(int id)
        {
            ActionAsPdf pdf = new ActionAsPdf("Invoice_print/"+(id.ToString()))
            {
                FileName =Server.MapPath("~/Report/invoice.pdf")
            };
            return pdf;
        }

        //public ActionResult Report(string ReportType)
        //{
        //    LocalReport localreport = new LocalReport();
        //    localreport.ReportPath = Server.MapPath("~/Report");
        //    ReportDataSource reportDataSource = new ReportDataSource();
        //    reportDataSource.Name = "Client_Management";
        //    reportDataSource.Value = db.tblInvoices.ToList();
        //    localreport.DataSources.Add(reportDataSource);

        //    string reportType = ReportType;
        //    string mimeType;
        //    string encoding;
        //    string fileExtention;
        //    if(reportType=="PDF")
        //    {
        //        fileExtention = "pdf";
        //    }
        //    string[] stream;
        //    Warning[] warnings;
        //    byte[] renderByte;
        //    renderByte = localreport.Render(reportType, "", out mimeType, out encoding, out fileExtention, out stream, out warnings);
        //    Response.AddHeader("content-disposition", "attachment:filename=invoice." + fileExtention);
        //    return File(renderByte, fileExtention);

        //}
        public ActionResult Index()
        {
            int duelist = db.tblInvoices.Where(x => x.Due > 0).ToList().Count;
            ViewBag.DueList = duelist;
            int paymentrecieve = db.tblClient_transaction.Where(x => x.Paid_amount > 0).ToList().Count;
            ViewBag.Paymentrecieve = paymentrecieve;
            int servicelist = db.tblAdded_service.ToList().Count;
            ViewBag.ServiceList = servicelist;


            var Paid = db.tblClient_transaction.ToList();
            int paid = 0;
            foreach(var item in Paid)
            {
                paid +=(int)item.Paid_amount;
            }
            var Due = db.tblInvoices.ToList();
            int due = 0;
            foreach(var item in Due)
            {
                due += (int)item.Due;
            }

            var Client = db.tblClient_profile.ToList().Count;
            var Project = db.tblAdded_service.ToList().Count;
            
            List<DataPoint> dataPoints = new List<DataPoint>();

            dataPoints.Add(new DataPoint("Total Paid(TK)",paid));
            dataPoints.Add(new DataPoint("Total Due(Tk)",due));
            dataPoints.Add(new DataPoint("Total Client", Client));
            dataPoints.Add(new DataPoint("Total Project", Project));
           
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);


            return View();
        }
        
    }
}