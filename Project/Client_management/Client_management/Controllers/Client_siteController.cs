using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client_management.Models;

namespace Client_management.Controllers
{
    public class Client_siteController : Controller
    {
        private Client_ManagementEntities db = new Client_ManagementEntities();
        // GET: Client_site

       
        public ActionResult Index()
        {
            if(Session["id"]==null)
            {
               return RedirectToAction("Login", "Login");
            }
            var user = Convert.ToString(Session["id"]);
            var client = db.tblClient_profile.Where(c => c.ID.ToString() == user).ToList();
            return View(client);
        }

        public ActionResult Details(int? id)
        {
            id = Convert.ToInt32(Session["id"]);
            tblClient_profile tblClient_Profile = db.tblClient_profile.Find(id);
            int length = tblClient_Profile.Logo.Count();
            tblClient_Profile.Logo = tblClient_Profile.Logo.Substring(tblClient_Profile.Logo.IndexOf("/"));
            return View(tblClient_Profile);
        }

        public ActionResult Service(int? id)
        {
            id = Convert.ToInt32(Session["id"]);
            var service = db.tblAdded_service.Where(s => s.Client_id == id).ToList();
            return View(service);
        }
        public ActionResult Payment(int? id)
        {
            id = Convert.ToInt32(Session["id"]);
            var payment = db.tblClient_transaction.Where(s => s.Client_id == id).ToList();
            return View(payment);
        }
        public ActionResult Invoice(int? id)
        {
            id = Convert.ToInt32(Session["id"]);
            var invoice = db.tblInvoices.Where(s => s.tblAdded_service.Client_id == id).ToList();
            return View(invoice);
        }
    }
}