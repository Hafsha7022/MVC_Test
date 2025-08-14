using Mvc_Test.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace New_MVC.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Login()
        {
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie != null)
            {
                ViewBag.UserName = cookie["UserName"].ToString();
                ViewBag.Password = cookie["Password"].ToString();
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult LoginP(UserTable ut)
        {
            if (ModelState.IsValid == true)
            {
                
                var constr = ConfigurationManager.ConnectionStrings["CustomerEntities1"].ToString();

                SqlConnection con = new SqlConnection(constr);
                using (con)
                {
                    string sqlQuery = "select UserName, Password from dbo.UserTable where UserName = @User and Password = @Pass";
                    SqlCommand cmd5 = new SqlCommand(sqlQuery, con);
                    cmd5.Parameters.AddWithValue("@User", ut.UserName);
                    cmd5.Parameters.AddWithValue("@Pass", ut.Password);
                    con.Open();
                    //cmd5.ExecuteNonQuery();

                    using (SqlDataReader sdr = cmd5.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                HttpCookie cookie = new HttpCookie("User");
                                if (ut.RememberMe == true)
                                {

                                    cookie["UserName"] = ut.UserName;
                                    cookie["Password"] = ut.Password;
                                    cookie.Expires = DateTime.Now.AddDays(2);
                                    HttpContext.Response.Cookies.Add(cookie);
                                }
                                else
                                {
                                    cookie.Expires = DateTime.Now.AddDays(-1);
                                    HttpContext.Response.Cookies.Add(cookie);
                                }


                                Session["UserName"] = ut.UserName;
                                FormsAuthentication.SetAuthCookie(ut.UserName, false);
                                return RedirectToAction("GetAllRecord", "Home");
                            }
                            //else
                            //{
                            //    ViewBag.ErrorMessage = "UserName or Password is Incorrect.";
                            //    return View();
                            //}
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "UserName or Password is Incorrect.";
                            return View("Login");
                        }
                    }



                    //return RedirectToAction("GetAllCustDetails");
                }

               
            }

            if (Session["UserName"] != null)
            {
                return RedirectToAction("GetAllRecord", "Home");
            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("Login", "Default");
        }
    }
}