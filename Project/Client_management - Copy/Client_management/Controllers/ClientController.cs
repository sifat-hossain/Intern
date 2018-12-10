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

namespace Client_management.Controllers
{
    public class ClientController : Controller
    {
        private int a = 0;
      private Client_ManagementEntities db = new Client_ManagementEntities();

        [HttpGet]
        public ActionResult Client_Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Client_Registration(tblClient_profile client_Registration)
        {
            string connection = ConfigurationManager.ConnectionStrings["Client_Management"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            string query = "insert into tblClient_profile(Company_name,Password, Logo, Company_email, Contact_person_name, Designation, Email, Cell_1, cell_2, cell_3, City,Address) values (@Company_name, @Password, @Logo, @Company_email, @Contact_person_name, @Designation, @Email, @Cell_1, @cell_2, @cell_3, @City, @Address)";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            cmd.Parameters.AddWithValue("@Company_name", client_Registration.Company_name);
            cmd.Parameters.AddWithValue("@Password", client_Registration.Password);
            if (client_Registration.Imagefile != null && client_Registration.Imagefile.ContentLength > 0)
            {
                string filename = Path.GetFileNameWithoutExtension(client_Registration.Imagefile.FileName);
                string extension = Path.GetExtension(client_Registration.Imagefile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                client_Registration.Logo = "~/Logo" + filename;
                filename = Path.Combine(Server.MapPath("~/Logo"), filename);
                client_Registration.Imagefile.SaveAs(filename);
            }
            cmd.Parameters.AddWithValue("@Logo", "~/Logo" + client_Registration.Imagefile.FileName);
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

            return View();
        }

        
        public ActionResult Client_details()
        {
            return View(db.tblClient_profile.ToList());
        }

        [HttpGet]
        public ActionResult Add_service_for_client(int id)
        {
            ViewBag.Client_id = db.tblClient_profile.Where(c => c.ID == id).FirstOrDefault().Company_name;
            ViewBag.Client_Id = id;
            ViewBag.Service_id = new SelectList(db.tblCompany_services, "ID", "C_service_name");
            return View();
        }


        [HttpPost]
        public ActionResult Add_service_for_client([Bind(Include = "ID,Client_id,Service_id,Quantity,Payable_amount,Added_date")] tblAdded_service tblAdded_service)
        {
          if(ModelState.IsValid)
            {
                db.tblAdded_service.Add(tblAdded_service);
                db.SaveChanges();
            }
            return View();
        }
    }
}