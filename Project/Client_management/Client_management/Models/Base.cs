using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Data;
using Client_management.Models;
using System.Web.Mvc;

namespace Client_management.Models
{
    public class Base
    {
       public string connectionString = ConfigurationManager.ConnectionStrings["Client_Management"].ConnectionString;

       

        //public void Add_Client()
        //{
        //    tblClient_profile client_Registration=new tblClient_profile();



        //    string connection = ConfigurationManager.ConnectionStrings["Client_Management"].ConnectionString;
        //    SqlConnection con = new SqlConnection(connection);
        //    string query = "insert into tblClient_profile(Company_name,Password, Logo, Company_email, Contact_person_name, Designation, Email, Cell_1, cell_2, cell_3, City,Address) values (@Company_name, @Password, @Logo, @Company_email, @Contact_person_name, @Designation, @Email, @Cell_1, @cell_2, @cell_3, @City, @Address)";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    con.Open();

        //    cmd.Parameters.AddWithValue("@Company_name", client_Registration.Company_name);
        //    cmd.Parameters.AddWithValue("@Password", client_Registration.Password);
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        string filename = Path.GetFileName(file.FileName);
        //        string imagepath = Path.Combine(Server.MapPath("~/Logo/"), filename);
        //        file.SaveAs(imagepath);
        //    }
        //    cmd.Parameters.AddWithValue("@Logo", "~/Logo/" + file.FileName);
        //    cmd.Parameters.AddWithValue("@Company_email", client_Registration.Company_email);
        //    cmd.Parameters.AddWithValue("@Contact_person_name", client_Registration.Contact_person_name);
        //    cmd.Parameters.AddWithValue("@Designation", client_Registration.Designation);
        //    cmd.Parameters.AddWithValue("@Email", client_Registration.Email);
        //    cmd.Parameters.AddWithValue("@Cell_1", client_Registration.Cell_1);
        //    cmd.Parameters.AddWithValue("@Cell_2", client_Registration.Cell_2);
        //    cmd.Parameters.AddWithValue("@Cell_3", client_Registration.Cell_3);
        //    cmd.Parameters.AddWithValue("@City", client_Registration.City);
        //    cmd.Parameters.AddWithValue("@Address", client_Registration.Address);


        //    cmd.ExecuteNonQuery();

        //    return;
        //}

        public void AddService(tblCompany_services tblCompany_Services)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SpAddService", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pservicename = new SqlParameter();
                pservicename.ParameterName = "@C_service_name";
                pservicename.Value = tblCompany_Services.C_service_name;
                cmd.Parameters.Add(pservicename);


                SqlParameter pcategory = new SqlParameter();
                pcategory.ParameterName = "@Category";
                pcategory.Value = tblCompany_Services.Category;
                cmd.Parameters.Add(pcategory);


                SqlParameter pprice = new SqlParameter();
                pprice.ParameterName = "@Price";
                pprice.Value = tblCompany_Services.Price;
                cmd.Parameters.Add(pprice);


                SqlParameter psubscription = new SqlParameter();
                psubscription.ParameterName = "@Subscription_fee";
                psubscription.Value = tblCompany_Services.Subscription_fee;
                cmd.Parameters.Add(psubscription);

                SqlParameter pdetails = new SqlParameter();
                pdetails.ParameterName = "@Details";
                pdetails.Value = tblCompany_Services.Details;
                cmd.Parameters.Add(pdetails);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        internal void Create_invoice(int invoice_no, int added_service_id, int payment_status, int payment_type, int due)
        {
            throw new NotImplementedException();
        }

        //public void Added_service(tblAdded_service added_Service)
        //{
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("SpAddedService", con);
        //        cmd.CommandType = CommandType.StoredProcedure;


        //        SqlParameter pclient = new SqlParameter();
        //        pclient.ParameterName = "@Client_id";
        //        pclient.Value = added_Service.Client_id;
        //        cmd.Parameters.Add(pclient);

        //        SqlParameter pservice = new SqlParameter();
        //        pservice.ParameterName = "@Service_id";
        //        pservice.Value = added_Service.Service_id;
        //        cmd.Parameters.Add(pservice);

        //        SqlParameter pquantity = new SqlParameter();
        //        pquantity.ParameterName = "@Quantity";
        //        pquantity.Value = added_Service.Quantity;
        //        cmd.Parameters.Add(pquantity);

        //        SqlParameter ppayable = new SqlParameter();
        //        ppayable.ParameterName = "@Payable_amount";
        //        ppayable.Value = added_Service.Payable_amount;
        //        cmd.Parameters.Add(ppayable);

        //        SqlParameter pdate = new SqlParameter();
        //        pdate.ParameterName = "@Added_date";
        //        pdate.Value = added_Service.Added_date;
        //        cmd.Parameters.Add(pdate);

        //        con.Open();
        //        cmd.ExecuteNonQuery();

        //    }

        //}

        
    }
}