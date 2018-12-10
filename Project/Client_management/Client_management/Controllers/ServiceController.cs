using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client_management.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace Client_management.Controllers
{
    public class ServiceController : Controller
    {
        private Client_ManagementEntities db = new Client_ManagementEntities();
        Base b = new Base();

        [HttpGet]
        public ActionResult Add_Service()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Add_service(tblCompany_services company_Services)
        {
            if(ModelState.IsValid)
            {
                b.AddService(company_Services);
                RedirectToAction("Service_List");
            }
            return View();
        }

        public ActionResult Service_List()
        {
            
            return View(db.tblCompany_services.ToList());
        }
    }
}