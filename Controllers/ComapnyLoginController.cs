using Clgproject.Data;
using Clgproject.Models;
using Clgproject.Models.MainSuppliers;
using Clgproject.Models.MainTrans;
using Clgproject.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace Clgproject.Controllers
{
    public class ComapnyLoginController : Controller
    {
        private ClgDbContext clgDbContext;

      
        public ComapnyLoginController(ClgDbContext clgDbContext)
        {
            this.clgDbContext = clgDbContext;
        }

        public IActionResult Login()
        {
         

            return View();
        }
        public IActionResult Dashboard()
        {
            int Id = (int)HttpContext.Session.GetInt32("id");
            string company_name = HttpContext.Session.GetString("company_name");
            string Email = HttpContext.Session.GetString("email");
            string Mobile_no = HttpContext.Session.GetString("mobile_no");
            string Password=HttpContext.Session.GetString("pass_word");
           string Compaddress= HttpContext.Session.GetString("companyaddress");
           string Country= HttpContext.Session.GetString("country");
            //HttpContext.Session.GetInt32("is_profile_complete");
            string Company_state = HttpContext.Session.GetString("comapany_state");
            string City = HttpContext.Session.GetString("city");
            string Pincode = HttpContext.Session.GetString("pincode");

            ViewBag.company_name = company_name;
            ViewBag.id = Id;
            ViewBag.email = Email;
            ViewBag.mobile_no = Mobile_no;
            ViewBag.pass_word= Password;
            ViewBag.companyaddress = Compaddress;
            ViewBag.country = Country;
            ViewBag.comapany_state = Company_state;
            ViewBag.city = City;
            ViewBag.pincode = Pincode;



            //string con = "Data Source=DESKTOP-U9BEN7N\\SQLEXPRESS;Initial Catalog=inventory;Integrated Security=True;";
            //string query = "SELECT SUM(quantity),order_details  FROM Products  GROUP BY order_details ";
            //using (SqlConnection conn = new SqlConnection(con))
            //{

            //    using (SqlCommand cmd = new SqlCommand(query, conn))
            //    {

            //        conn.Open();
            //        SqlDataReader reader = cmd.ExecuteReader();
            //        List<int> integerList = new List<int>();
            //        List<string> stringlist = new List<string>();
            //        while (reader.Read())
            //        {

            //            int Values = reader.GetInt32(0);
            //            string Lables = reader.GetString(1);
                        
            //            integerList.Add(Values);
            //            stringlist.Add(Lables);
            //        }
            //        int[] integerArray = integerList.ToArray();
            //      string[] stringArray = stringlist.ToArray();

                    
            //        ViewBag.Values = integerArray;
            //        ViewBag.Lables = stringArray.ToArray();
                   

            //    }
            //}
            return View();
        }
        
        [HttpPost]

        public IActionResult Login(Registration register)
        {
         
            try
            {
                string con = "Data Source=DESKTOP-U9BEN7N\\SQLEXPRESS;Initial Catalog=inventory;Integrated Security=True;";
                string query = "SELECT * FROM company_basic where company_name='" + register.company_name + "' and pass_word='" + register.pass_word + "'";
                using (SqlConnection conn = new SqlConnection(con))
                {

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        //cmd.Parameters.AddWithValue("@company_name", register.company_name);
                        //cmd.Parameters.AddWithValue("@pass_word", register.pass_word);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            register.id = Convert.ToInt32(reader["id"]);
                            register.company_name = reader["company_name"].ToString();
                            register.email = reader["email"].ToString();
                            register.mobile_no = reader["mobile_no"].ToString();
                            register.pass_word = reader["pass_word"].ToString();
                            register.companyaddress = reader["companyaddress"].ToString();
                            register.country = reader["country"].ToString();
                            register.is_profile_complete = Convert.ToInt32(reader["is_profile_complete"]);
                            register.comapany_state = reader["comapany_state"].ToString();
                            register.city = reader["city"].ToString();
                            register.pincode = reader["pincode"].ToString();
                        }

                        if (reader.HasRows)
                        {
                           
                            HttpContext.Session.SetInt32("id", register.id);
                            HttpContext.Session.SetString("company_name", register.company_name);
                            HttpContext.Session.SetString("email", register.email);
                            HttpContext.Session.SetString("mobile_no", register.mobile_no);
                            HttpContext.Session.SetString("pass_word", register.pass_word);
                            HttpContext.Session.SetString("companyaddress", register.companyaddress);
                            HttpContext.Session.SetString("country", register.country);
                            HttpContext.Session.SetInt32("is_profile_complete", register.is_profile_complete);
                            HttpContext.Session.SetString("company_state", register.comapany_state);
                            HttpContext.Session.SetString("city", register.city);
                            HttpContext.Session.SetString("pincode", register.pincode);



                            Random random = new Random();
                            string otp = random.Next(100000, 999999).ToString();
                            HttpContext.Session.SetString("otp", otp);


                            MailMessage message = new MailMessage();
                            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                            message.From = new MailAddress("khushiposhiya708@gmail.com");
                            message.To.Add(new MailAddress(register.email));
                            message.Subject = "OTP for Login Verification";
                            message.Body = $"Your OTP is: {otp}";
                            smtpClient.Port = 587;
                            smtpClient.Credentials = new NetworkCredential("khushiposhiya708@gmail.com", "vner mksc qibd wzfc");
                            smtpClient.EnableSsl = true;
                            smtpClient.Send(message);
                          
                            return RedirectToAction("Otp");

                        }

        
                        else
                        {
                            ViewBag.errorMessage = "Invalid id or password";
                            return RedirectToAction("Login");
                        }
                        
                    }
                
                }
            }

            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return View();
            }

        }

        public IActionResult Otp()
        {
            string company_name = HttpContext.Session.GetString("company_name");
            ViewBag.company_name = company_name;
            return View();
        }
        [HttpPost]  
        public IActionResult Otp(Otpp otpp)
        {
            string otp = HttpContext.Session.GetString("otp");
            string data = otp;
            if (otpp.otp == otp)
            {
                int profile   = (int)HttpContext.Session.GetInt32("is_profile_complete");

                if (profile == 0)
                {
                    return RedirectToAction("Companydetails");
                }
                else
                {
                    return RedirectToAction("Dashboard");
                }
            }
            else
            {
                string error = "OTP DOES NOT MATCH";
                ViewBag.error = error;
                return RedirectToAction("Otp");
            }
            return View();
        }




        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Registration register)
        {

            try
            {

                string con = "Data Source=DESKTOP-U9BEN7N\\SQLEXPRESS;Initial Catalog=inventory;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    string query = "INSERT INTO company_basic (company_name,email,mobile_no,pass_word,companyaddress,country,comapany_state,city,pincode,is_profile_complete)VALUES" +
                        "('" + register.company_name + "','" + register.email + "','" + register.mobile_no + "','" + register.pass_word + "','" + register.companyaddress + "','" + register.country + "','" + register.comapany_state + "','" + register.city + "','" + register.pincode + "','"+0+"')";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {

                        cmd.Parameters.AddWithValue("@company_name", register.company_name);
                        cmd.Parameters.AddWithValue("@email", register.email);
                        cmd.Parameters.AddWithValue("@mobile_no", register.mobile_no);
                        cmd.Parameters.AddWithValue("@pass_word", register.pass_word);
                        cmd.Parameters.AddWithValue("@companyaddress", register.companyaddress);
                        cmd.Parameters.AddWithValue("@country", register.country);
                        cmd.Parameters.AddWithValue("@company_state", register.comapany_state);
                        cmd.Parameters.AddWithValue("@city", register.city);
                        cmd.Parameters.AddWithValue("@pincode", register.pincode);
                        cmd.Parameters.AddWithValue("@is_profile_complete", 0);
                        cmd.ExecuteNonQuery();
                        //SqlDataReader reader = cmd.ExecuteReader();

                        return RedirectToAction("Login");
                       
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return View();
            }


            return RedirectToAction("Companydetails");
        }


        [HttpGet]
        public IActionResult ProfileShow()
        {
            var model = new List<Registration>();
            try
            {

                string con = "Data Source=DESKTOP-U9BEN7N\\SQLEXPRESS;Initial Catalog=inventory;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    string query = "Select * from company_basic";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var view = new Registration();


                                view.id = Convert.ToInt32(reader["id"]);
                                view.company_name = reader["company_name"].ToString();
                                view.email = reader["email"].ToString();
                                view.mobile_no = reader["mobile_no"].ToString();
                                view.pass_word = reader["pass_word"].ToString();
                                view.companyaddress = reader["companyaddress"].ToString();
                                view.country = reader["country"].ToString();
                                view.comapany_state = reader["comapany_state"].ToString();
                                view.city = reader["city"].ToString();
                                view.pincode = reader["pincode"].ToString();
                                model.Add(view);

                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return View();
            }
           // return View(model);

            var model1 = new List<Companydetail>();
            try
            {

                string con = "Data Source=DESKTOP-U9BEN7N\\SQLEXPRESS;Initial Catalog=inventory;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    string query = "Select * from Companydetails";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var view1 = new Companydetail();
                                view1.d_id = Convert.ToInt32(reader["d_id"]); ;
                                int Id = (int)HttpContext.Session.GetInt32("id");
                                ViewBag.id = Id;
                                // cmd.Parameters.AddWithValue("@companyid", companydetail.companyid);
                                view1.gstno = reader["gstno"].ToString();
                                view1.panno = reader["panno"].ToString();
                                view1.faxno = reader["faxno"].ToString();
                                view1.offwebsite = reader["offwebsite"].ToString();
                                view1.socialmedialink = reader["socialmedialink"].ToString();
                                view1.other = reader["other"].ToString();

                                model1.Add(view1);

                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return View();
            }
            // return View(model1);

            var model2 = new List<Bankdetail>();
            try
            {

                string con = "Data Source=DESKTOP-U9BEN7N\\SQLEXPRESS;Initial Catalog=inventory;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    string query = "Select * from bankdetails";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var view2 = new Bankdetail();
                                view2.bank_id = Convert.ToInt32(reader["bank_id"]);
                                int Id = (int)HttpContext.Session.GetInt32("id");
                                ViewBag.id = Id;
                                // cmd.Parameters.AddWithValue("@companyid", companydetail.companyid);
                                view2.bankname = reader["bankname"].ToString();
                                view2.customername = reader["customername"].ToString();
                                view2.accountno = Convert.ToInt32(reader["accountno"]);
                                view2.ifsccode = reader["ifsccode"].ToString();
                                view2.branchname = reader["branchname"].ToString();
                                view2.branchaddress = reader["branchaddress"].ToString();



                                model2.Add(view2);

                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return View();
            }


            ProfileView profileShow = new ProfileView();
            profileShow.registrations = model;
            profileShow.companydetails = model1;
            profileShow.bankdetails = model2;

            return View(profileShow);


        }


        //Edit Models code
        [HttpPost]
        public IActionResult CompanyEdit(Registration register)
        {
            try
            {

                string con = "Data Source=DESKTOP-U9BEN7N\\SQLEXPRESS;Initial Catalog=inventory;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    string query = "Update company_basic Set company_name=@company_name,email=@email,mobile_no=@mobile_no,pass_word=@pass_word,companyaddress=@companyaddress,country=@country,comapany_state=@comapany_state,city=@city,pincode=@pincode where id=@id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {

                        // 
                        //SqlDataReader reader = cmd.ExecuteReader();
                        cmd.Parameters.AddWithValue("@id", register.id);
                        cmd.Parameters.AddWithValue("@company_name", register.company_name);
                        cmd.Parameters.AddWithValue("@email", register.email);
                        cmd.Parameters.AddWithValue("@mobile_no", register.mobile_no);
                        cmd.Parameters.AddWithValue("@pass_word", register.pass_word);
                        cmd.Parameters.AddWithValue("@companyaddress", register.companyaddress);
                        cmd.Parameters.AddWithValue("@country", register.country);
                        cmd.Parameters.AddWithValue("@comapany_state", register.comapany_state);
                        cmd.Parameters.AddWithValue("@city", register.city);
                        cmd.Parameters.AddWithValue("@pincode", register.pincode);
                        cmd.ExecuteNonQuery();

                        HttpContext.Session.SetInt32("id", register.id);
                        HttpContext.Session.SetString("company_name", register.company_name);
                        HttpContext.Session.SetString("email", register.email);
                        HttpContext.Session.SetString("mobile_no", register.mobile_no);
                        HttpContext.Session.SetString("pass_word", register.pass_word);
                        HttpContext.Session.SetString("companyaddress", register.companyaddress);
                        HttpContext.Session.SetString("country", register.country);
                        HttpContext.Session.SetInt32("is_profile_complete", register.is_profile_complete);
                        HttpContext.Session.SetString("company_state", register.comapany_state);
                        HttpContext.Session.SetString("city", register.city);
                        HttpContext.Session.SetString("pincode", register.pincode);
                        return RedirectToAction("ProfileShow");

                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return View();
            }


            return View();
        }
        [HttpPost]
        public IActionResult RegisterEdit(Companydetail companydetail)
        {
            try
            {

                string con = "Data Source=DESKTOP-U9BEN7N\\SQLEXPRESS;Initial Catalog=inventory;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    string query = "Update Companydetails Set gstno=@gstno,panno=@panno,faxno=@faxno,offwebsite=@offwebsite,socialmedialink=@socialmedialink,other=@other where d_id=@d_id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {

                       // 
                        //SqlDataReader reader = cmd.ExecuteReader();
                        cmd.Parameters.AddWithValue("@d_id", companydetail.d_id);
                        cmd.Parameters.AddWithValue("@gstno", companydetail.gstno);
                        cmd.Parameters.AddWithValue("@panno", companydetail.panno);
                        cmd.Parameters.AddWithValue("@faxno", companydetail.faxno);
                        cmd.Parameters.AddWithValue("@offwebsite", companydetail.offwebsite);
                        cmd.Parameters.AddWithValue("@socialmedialink", companydetail.socialmedialink);
                       cmd.Parameters.AddWithValue("@other", companydetail.other);
                        cmd.ExecuteNonQuery();
                        return RedirectToAction("ProfileShow");

                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return View();
            }


            return View();
        }

      
        [HttpPost]
        public IActionResult BankEdit(Bankdetail bankdetail)
        {
            try
            {

                string con = "Data Source=DESKTOP-U9BEN7N\\SQLEXPRESS;Initial Catalog=inventory;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    string query = "Update bankdetails Set bankname=@bankname,customername=@customername,accountno=@accountno,ifsccode=@ifsccode,branchname=@branchname,branchaddress=@branchaddress where bank_id=@bank_id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@bank_id", bankdetail.bank_id);
                        cmd.Parameters.AddWithValue("@bankname", bankdetail.bankname);
                        cmd.Parameters.AddWithValue("@customername", bankdetail.customername);
                        cmd.Parameters.AddWithValue("@accountno", bankdetail.accountno);
                        cmd.Parameters.AddWithValue("@ifsccode", bankdetail.ifsccode);
                        cmd.Parameters.AddWithValue("@branchname", bankdetail.branchname);
                        cmd.Parameters.AddWithValue("@branchaddress", bankdetail.branchaddress);

                        cmd.ExecuteNonQuery();
                        return RedirectToAction("ProfileShow");

                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return View();
            }


            return View();
        }
        public IActionResult Companydetails()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Companydetails(Companydetail companydetail)
        {
            try
            {

                string con = "Data Source=DESKTOP-U9BEN7N\\SQLEXPRESS;Initial Catalog=inventory;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    string query = "INSERT INTO Companydetails (companyid,gstno,panno,faxno,offwebsite,socialmedialink,other,pro1_complete)VALUES" +
                    "('"+companydetail.companyid+"','" + companydetail.gstno + "','" + companydetail.panno + "','" + companydetail.faxno + "','" + companydetail.offwebsite + "','" + companydetail.socialmedialink + "','" + companydetail.other+ "','"+1+"')";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@companyid", companydetail.companyid);
                        cmd.Parameters.AddWithValue("@gstno", companydetail.gstno);
                        cmd.Parameters.AddWithValue("@panno", companydetail.panno);
                        cmd.Parameters.AddWithValue("@faxno", companydetail.faxno);
                        cmd.Parameters.AddWithValue("@offwebsite",companydetail.offwebsite);
                        cmd.Parameters.AddWithValue("@socialmedialink", companydetail.socialmedialink);
                        cmd.Parameters.AddWithValue("@other",companydetail.other);
                        cmd.Parameters.AddWithValue("@pro1_compelete", 1);


                        cmd.ExecuteNonQuery();
                        if (companydetail!= null)
                        {
                            return RedirectToAction("Bankdetails");
                        }
                        else
                        {
                            return RedirectToAction("Companydetails");
                        }




                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return View();
            }
         
        }
       
        public IActionResult Bankdetails()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Bankdetails(Bankdetail bankdetail)
        {
            try
            {

                string con = "Data Source=DESKTOP-U9BEN7N\\SQLEXPRESS;Initial Catalog=inventory;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    string query = "INSERT INTO bankdetails (companyid,bankname,customername,accountno,ifsccode,branchname,branchaddress,pro2_complete)VALUES" +
                    "('" + bankdetail.companyid + "','" + bankdetail.bankname + "','" + bankdetail.customername + "','" +bankdetail.accountno + "','" + bankdetail.ifsccode + "','" + bankdetail.branchname + "','"+bankdetail.branchaddress+"','"+ 1 +"')";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@companyid", bankdetail.companyid);
                        cmd.Parameters.AddWithValue("@bankname", bankdetail.bankname);
                        cmd.Parameters.AddWithValue("@customername", bankdetail.customername);
                        cmd.Parameters.AddWithValue("@accountno", bankdetail.accountno);
                        cmd.Parameters.AddWithValue("@ifsccode", bankdetail.ifsccode);
                        cmd.Parameters.AddWithValue("@branchname", bankdetail.branchname);
                        cmd.Parameters.AddWithValue("@branchaddress", bankdetail.branchaddress);
                        cmd.Parameters.AddWithValue("@pro2_complete", 1);



                        cmd.ExecuteNonQuery();
                        TempData["Message"] = "register suceessfully done";


                        if (bankdetail != null)
                        {
                            string query2 = "Update company_basic Set is_profile_complete= '"+1+"'";
                            using (SqlCommand cmd1 = new SqlCommand(query2, connection))
                            {
                                cmd1.Parameters.AddWithValue("@is_profile_complete", 1);
                                cmd1.ExecuteNonQuery();

                            }
                            return RedirectToAction("Dashboard");
                        }
                        else
                        {
                            return RedirectToAction("Bankdetails");
                        }



                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return View();
            }
            //Response.Redirect("Login");

            return RedirectToAction("Login");
        }
      
    }
}
