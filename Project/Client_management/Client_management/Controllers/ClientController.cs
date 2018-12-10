using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client_management.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Data.SqlTypes;
using System.Net.Mail;
using System.Net;

namespace Client_management.Controllers
{
    public class ClientController : Controller
    {
        tblInvoice invoice = new tblInvoice();
        tblClient_transaction Client_Transaction = new tblClient_transaction();
        tblCompany_services Company_Services = new tblCompany_services();

        Random r = new Random();
        private Client_ManagementEntities db = new Client_ManagementEntities();
        Base b = new Base();
        string message = "";

        [HttpGet]
        public ActionResult Client_Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Client_Registration(tblClient_profile client_Registration )
        {
            string connection = ConfigurationManager.ConnectionStrings["Client_Management"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            string query = "insert into tblClient_profile(Company_name,Password, Logo, Company_email, Contact_person_name, Designation, Email, Cell_1, cell_2, cell_3, City,Address) values (@Company_name, @Password, @Logo, @Company_email, @Contact_person_name, @Designation, @Email, @Cell_1, @cell_2, @cell_3, @City, @Address)";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            var pass = client_Registration.Password;
                client_Registration.Password = Crypto.Hash(client_Registration.Password);
                cmd.Parameters.AddWithValue("@Company_name", client_Registration.Company_name);
                cmd.Parameters.AddWithValue("@Password", client_Registration.Password);


                if (client_Registration.Imagefile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(client_Registration.Imagefile.FileName);
                    string extension = Path.GetExtension(client_Registration.Imagefile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    client_Registration.Logo = "~/Image/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                    client_Registration.Imagefile.SaveAs(filename);
                }

                cmd.Parameters.AddWithValue("@Logo", client_Registration.Logo);
                cmd.Parameters.AddWithValue("@Company_email", client_Registration.Company_email);
                cmd.Parameters.AddWithValue("@Contact_person_name", client_Registration.Contact_person_name);
                cmd.Parameters.AddWithValue("@Designation", client_Registration.Designation);
                cmd.Parameters.AddWithValue("@Email", client_Registration.Email);
                cmd.Parameters.AddWithValue("@Cell_1", client_Registration.Cell_1);
                cmd.Parameters.AddWithValue("@Cell_2", client_Registration.cell_2);
                cmd.Parameters.AddWithValue("@Cell_3", client_Registration.cell_3);
                cmd.Parameters.AddWithValue("@City", client_Registration.City);
                cmd.Parameters.AddWithValue("@Address", client_Registration.Address);
                cmd.ExecuteNonQuery();

            SendEmail(client_Registration.Company_email, pass);
            message = "Registration successfully done. Account activation link " +
                " has been sent to your email id:" + client_Registration.Company_email;

            return RedirectToAction("Client_list", "Client");
            
           
        }

        
        public ActionResult Client_list()
        {
            return View(db.tblClient_profile.ToList());
        }

        [ActionName("Details")]
        public ActionResult Client_details(int? id)
        {
            tblClient_profile tblClient_Profile = db.tblClient_profile.Find(id);
            int length = tblClient_Profile.Logo.Count();
            tblClient_Profile.Logo = tblClient_Profile.Logo.Substring(tblClient_Profile.Logo.IndexOf("/"));
            return View(tblClient_Profile);
        }

        [HttpGet]
        public ActionResult Add_service_for_client(int id)
        {
          

            ViewBag.Client_id = db.tblClient_profile.Where(c => c.ID == id).FirstOrDefault().ID;
            ViewBag.Client_Name = db.tblClient_profile.Where(c => c.ID == id).FirstOrDefault().Company_name;
            ViewBag.Service_id = new SelectList(db.tblCompany_services, "ID", "C_service_name");
            return View();
        }


        [HttpPost]
        public ActionResult Add_service_for_client([Bind(Include = "ID,Client_id,Service_id,Quantity,Payable_amount,Added_date")] tblAdded_service tblAdded_service, int paidAmount=0)
        {

            if (ModelState.IsValid)
            {    
                 
                invoice.Invoice_no =r.Next(0,1000000);
                invoice.Payment_status = 0;
                invoice.Payment_type = 1; 
                invoice.Due = tblAdded_service.Payable_amount - paidAmount;  
                db.tblAdded_service.Add(tblAdded_service);
                db.SaveChanges();               
                invoice.Added_service_id = tblAdded_service.ID;
                invoice.Date = DateTime.Now;
                db.tblInvoices.Add(invoice);
                db.SaveChanges();

                Invoice_for_price(tblAdded_service);

                Client_Transaction.Client_id = tblAdded_service.Client_id;
                Client_Transaction.Paid_amount = paidAmount;
                Client_Transaction.Paid_date = tblAdded_service.Added_date;
               var category = db.tblCompany_services.Where(c => c.ID == tblAdded_service.Service_id).FirstOrDefault();
                Client_Transaction.Payment_type = category.Category;

                var Invoice = db.tblInvoices.Where(i => i.Added_service_id == tblAdded_service.ID).Select(p => new
                {
                    invoiceId = p.ID
                }).Single();

                Client_Transaction.Invoice_id = Invoice.invoiceId;
                db.tblClient_transaction.Add(Client_Transaction);
                db.SaveChanges();

                RedirectToAction("Index", "Added_service");
            }
            

            ViewBag.Service_id = new SelectList(db.tblCompany_services, "ID", "C_service_name");
            return RedirectToAction("Index", "Added_service");
        }

        public void Invoice_for_price(tblAdded_service tblAdded_service)
        {
            invoice.Invoice_no = r.Next(0, 1000000);
            invoice.Payment_status = 0;
            invoice.Payment_type = 2;
            var price = db.tblCompany_services.Where(s => s.ID == tblAdded_service.Service_id).Select(p => new
            {
                service_price = p.Price
            }).Single();

            invoice.Due = (tblAdded_service.Quantity * price.service_price);
            invoice.Added_service_id = tblAdded_service.ID;
            invoice.Date = DateTime.Now;
            db.tblInvoices.Add(invoice);
        }

        [NonAction]
        public void SendEmail(string emailID, string Password)
        {

            var fromEmail = new MailAddress("sifat1258@gmail.com", "Dotnet Awesome");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "rif@t#ossain"; // Replace with actual password
            string subject = "Your account is successfully created!";

            string body = "<br/><br/> This is your Login mail: " + emailID + " and this is your login password:" + Password;



            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
    }
}