using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Client_management.Models;

namespace Client_management.Controllers
{
    public class LoginController : Controller
    {
        private Client_ManagementEntities db = new Client_ManagementEntities();

        [HttpGet]
       public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            string message="";
           
            if (Email!="" && Password!="")
            {
                var password = Crypto.Hash(Password);
                var loginId = db.tblClient_profile.Where(l => l.Company_email == Email).FirstOrDefault();

                if (loginId.Company_email == Email && loginId.Password == password)
                {
                    Session["id"] = loginId.ID;

                    return RedirectToAction("Index", "Client_site");
                }
                else
                {
                    message = "Invalid User Email or Password";
                }
            }
            else
            {
                ViewBag.Email = "Email is required";
                ViewBag.Password = "Password is required";
            }
            ViewBag.Message = message;
            return View();
        }
        [HttpPost]
       public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login"); 
        }
    }
}