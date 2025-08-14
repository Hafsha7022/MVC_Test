using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Mvc_Test.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Test.Controllers;

namespace Mvc_Test.Controllers
{
    public class HomeController : Controller
    {

        //private SqlConnection con;
        //private void connection()
        //{
        //    string constr = ConfigurationManager.ConnectionStrings["CustomerEntities"].ToString();
        //    con = new SqlConnection(constr);
        //}
        //GET: Home
        
        public ActionResult Create(int? id)
        {
          if (id != null)
            {
                
                    var constr = ConfigurationManager.ConnectionStrings["CustomerEntities"].ToString();
                    SqlConnection con = new SqlConnection(constr);
                    DataTable dt = new DataTable();
                    using (con)
                    {
                        con.Open();
                        string query = "Select * from dbo.Customer_Master a join dbo.Customer_Details b on a.CustomerId = b.Cust_ID where a.CustomerId = @CustmerId and b.IsDeleted = 1 /*and b.Id = @CustmerCode*/";
                        SqlDataAdapter da = new SqlDataAdapter(query, con);
                        da.SelectCommand.Parameters.AddWithValue("@CustmerId", id);
                        //da.SelectCommand.Parameters.AddWithValue("@CustmerCode", code);
                        da.Fill(dt);

                    }
                    if (dt.Rows.Count != 0)
                    {
                    CustomerMaster_Model custm = new CustomerMaster_Model();

                    custm.CustomerId = Convert.ToInt32(dt.Rows[0][0]);
                    custm.Name = Convert.ToString(dt.Rows[0][1].ToString().Trim());
                    custm.CustomerCode = Convert.ToInt32(dt.Rows[0][2]);
                    custm.TotalQty = Convert.ToInt32(dt.Rows[0][3]);
                    custm.GrossAmt = Convert.ToInt32(dt.Rows[0][4]);
                    custm.DiscPer = Convert.ToInt32(dt.Rows[0][5]);
                    custm.DiscAmt = Convert.ToInt32(dt.Rows[0][6]);
                    custm.NetAmt = Convert.ToInt32(dt.Rows[0][7]);
                    custm.SatrtDate = Convert.ToDateTime(dt.Rows[0][8]).Date;
                    custm.Days = Convert.ToInt32(dt.Rows[0][9]);
                    custm.EndDate = Convert.ToDateTime(dt.Rows[0][10]).Date;
                    custm.NewSatrtDate = Convert.ToString(dt.Rows[0][11].ToString().Trim());
                    custm.NewDays = Convert.ToInt32(dt.Rows[0][12]);
                    custm.NewEndDate = Convert.ToString(dt.Rows[0][13].ToString().Trim());
                    custm.CustomerDetails = getDetailEdit((int)id);
                    custm.CustomerItems = getItemEdit((int)id);
                    return View(custm);
                    }             

                



            }
            else
            {
                
                CustomerMaster_Model cm = new CustomerMaster_Model();
                cm.CustomerDetails = getDetails();
                cm.CustomerItems = getItem();
                return View(cm);
            }
            return View(new CustomerMaster_Model());

        }



       
        public List <CustomerDetails_Model> getDetailEdit(int id)
        {
            var DetailEdit = new List<CustomerDetails_Model>();
            var constr = ConfigurationManager.ConnectionStrings["CustomerEntities"].ToString();
            SqlConnection con = new SqlConnection(constr);
            DataTable dt = new DataTable();
            using (con)
            {
                con.Open();
                string query = "Select * from dbo.Customer_Master a join dbo.Customer_Details b on a.CustomerId = b.Cust_ID where a.CustomerId = @CustmerId and b.IsDeleted = 1 /*and b.Id = @CustmerCode*/";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@CustmerId", id);
                //da.SelectCommand.Parameters.AddWithValue("@CustmerCode", code);
                da.Fill(dt);

            }
            if (dt.Rows.Count != 0)
            {
                DetailEdit = new List<CustomerDetails_Model>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                   
                    DetailEdit.Add(new CustomerDetails_Model()
                    {
                        CustomerId = Convert.ToInt32(dt.Rows[i][14]),
                        AddressLine1 = Convert.ToString(dt.Rows[i][15].ToString().Trim()),
                        AddressLine2 = Convert.ToString(dt.Rows[i][16].ToString().Trim()),
                        City = Convert.ToString(dt.Rows[i][17].ToString().Trim()),
                        State = Convert.ToString(dt.Rows[i][18].ToString().Trim()),
                        PinCode = Convert.ToString(dt.Rows[i][19].ToString().Trim()),
                        PhoneNumber = Convert.ToInt32(dt.Rows[i][20])
                    });
                }
               
            }
            return DetailEdit;
        }


        public List<CustomerItem_Model> getItemEdit(int id)
        {
            var ItemEdit = new List<CustomerItem_Model>();
            var constr = ConfigurationManager.ConnectionStrings["CustomerEntities"].ToString();
            SqlConnection con = new SqlConnection(constr);
            DataTable dt = new DataTable();
            using (con)
            {
                con.Open();
                string query = "Select * from dbo.Customer_Master a join dbo.Customer_Item b on a.CustomerId = b.Cust_ID where a.CustomerId = @CustmerId and b.IsDeleted = 1 /*and b.Id = @CustmerCode*/";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@CustmerId", id);
                //da.SelectCommand.Parameters.AddWithValue("@CustmerCode", code);
                da.Fill(dt);

            }
            if (dt.Rows.Count != 0)
            {
                ItemEdit = new List<CustomerItem_Model>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ItemEdit.Add(new CustomerItem_Model()
                    {
                        ItemId = Convert.ToInt32(dt.Rows[i][14]),
                        Item = Convert.ToString(dt.Rows[i][15].ToString().Trim()),
                        Qty = Convert.ToInt32(dt.Rows[i][16]),
                        Rate = Convert.ToInt32(dt.Rows[i][17]),
                        Amount = Convert.ToInt32(dt.Rows[i][18]),
                       
                    });
                }

            }
            return ItemEdit;
        }

        public List<CustomerDetails_Model> getDetails()
        {
            var LDetail = new List<CustomerDetails_Model>();
            LDetail.Add(new CustomerDetails_Model());
            return LDetail;
        }
        public List<CustomerItem_Model> getItem()
        {
            var LItem = new List<CustomerItem_Model>();
            LItem.Add(new CustomerItem_Model());
            return LItem;
        }

        public ActionResult AddNewLine(CustomerDetails_Model cd)
        {
            //CustomerDetails_Model CustomerDetail = new CustomerDetails_Model();
            return PartialView("_CreateD", cd);
        }
        public ActionResult AddNewItem(CustomerItem_Model ci)
        {
            //CustomerDetails_Model CustomerDetail = new CustomerDetails_Model();
            return PartialView("_ItemD", ci);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RemoveLine(int DelCode)
        {
            int Delid = DeleteDetail(DelCode);
            //int id = cd.CustomerId; 
            //DeleteDetail(Convert.ToInt32(cd.CustomerId));
            //CustomerDetails_Model CustomerDetail = new CustomerDetails_Model();
            return RedirectToAction("Create", new { id = Delid });
        }

        public int DeleteDetail(int id)
        {
            var DetailEdit = new List<CustomerDetails_Model>();
            var constr = ConfigurationManager.ConnectionStrings["CustomerEntities"].ToString();
            SqlConnection con = new SqlConnection(constr);
            DataTable dt = new DataTable();
            using (con)
            {
                con.Open();
                
                string query = "Update dbo.Customer_Details set IsDeleted = 0 where Id = @CustmerId Select Cust_ID from  dbo.Customer_Details b where b.Id = @CustmerId ";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@CustmerId", id);
                //da.SelectCommand.Parameters.AddWithValue("@CustmerCode", code);
                da.Fill(dt);

            }
            
            return Convert.ToInt32(dt.Rows[0][0]);
        }


        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RemoveItem(int DelCode)
        {
            int Delid = DeleteItem(DelCode);
            //int id = cd.CustomerId; 
            //DeleteDetail(Convert.ToInt32(cd.CustomerId));
            //CustomerDetails_Model CustomerDetail = new CustomerDetails_Model();
            return RedirectToAction("Create", new { id = Delid });
        }

        public int DeleteItem(int id)
        {
            var DetailEdit = new List<CustomerDetails_Model>();
            var constr = ConfigurationManager.ConnectionStrings["CustomerEntities"].ToString();
            SqlConnection con = new SqlConnection(constr);
            DataTable dt = new DataTable();
            using (con)
            {
                con.Open();

                string query = "Update dbo.Customer_Item set IsDeleted = 0 where Item_Id = @CustmerId Select Cust_ID from  dbo.Customer_Item b where b.Item_Id = @CustmerId ";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@CustmerId", id);
                //da.SelectCommand.Parameters.AddWithValue("@CustmerCode", code);
                da.Fill(dt);

            }

            return Convert.ToInt32(dt.Rows[0][0]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Data(CustomerMaster_Model cm, int? id, int? code/*, int? CustomerId, int? CustomerCode, String CustomerName, String AddressLine1, String AddressLine2, String City, String State, String PinCode, int PhoneNumber*/)
                    {
            string msg = string.Empty;
            

            if (id != 0 && code != 0)
            {
                if (ModelState.IsValid)
                {

                    try
                    {
                        var constr = ConfigurationManager.ConnectionStrings["CustomerEntities"].ToString();

                        SqlConnection con = new SqlConnection(constr);
                        using (con)
                        {
                            SqlCommand cmd3 = new SqlCommand("SP_UpdateNew", con);
                            cmd3.CommandType = CommandType.StoredProcedure;
                            cmd3.Parameters.AddWithValue("@CustomerName", cm.Name);
                            cmd3.Parameters.AddWithValue("@Code", cm.CustomerCode);
                            cmd3.Parameters.AddWithValue("@id", id);
                            cmd3.Parameters.AddWithValue("@TotalQty", cm.TotalQty);
                            cmd3.Parameters.AddWithValue("@GrossAmt", cm.GrossAmt);
                            cmd3.Parameters.AddWithValue("@DiscPer", cm.DiscPer);
                            cmd3.Parameters.AddWithValue("@DiscAmt", cm.DiscAmt);
                            cmd3.Parameters.AddWithValue("@NetAmt", cm.NetAmt);
                            cmd3.Parameters.AddWithValue("@SatrtDate", cm.SatrtDate);
                            cmd3.Parameters.AddWithValue("@Days", cm.Days);
                            cmd3.Parameters.AddWithValue("@EndDate", cm.EndDate);
                            cmd3.Parameters.AddWithValue("@NewSatrtDate", cm.NewSatrtDate);
                            cmd3.Parameters.AddWithValue("@NewDays", cm.NewDays);
                            cmd3.Parameters.AddWithValue("@NewEndDate", cm.NewEndDate);
                            con.Open();
                            cmd3.ExecuteNonQuery();
                            con.Close();
                            //cmd3.Parameters.AddWithValue("@CountNum", cm.CustomerDetails.Count);
                            foreach (var item in cm.CustomerDetails)
                            {
                                if (item.CustomerId != null)
                                {
                                    SqlCommand cmd4 = new SqlCommand("SP_UpdateDetail", con);
                                    cmd4.CommandType = CommandType.StoredProcedure;
                                    //cmd4.Parameters.AddWithValue("@CustomerName", cm.Name);
                                    //cmd3.Parameters.AddWithValue("@Code", cm.CustomerCode);
                                    //cmd3.Parameters.AddWithValue("@Count", cm.CustomerDetails.Count);
                                    cmd4.Parameters.AddWithValue("@AddressLine1", item.AddressLine1);
                                    cmd4.Parameters.AddWithValue("@AddressLine2", item.AddressLine2);
                                    cmd4.Parameters.AddWithValue("@City", item.City);
                                    cmd4.Parameters.AddWithValue("@State", item.State);
                                    cmd4.Parameters.AddWithValue("@PinCode", item.PinCode);
                                    cmd4.Parameters.AddWithValue("@PhoneNumber", item.PhoneNumber);
                                    cmd4.Parameters.AddWithValue("@id", id);
                                    cmd4.Parameters.AddWithValue("@DetailId", item.CustomerId);

                                    con.Open();
                                    cmd4.ExecuteNonQuery();
                                    con.Close();
                                }
                                else
                                {
                                    
                                    using (con)
                                    {
                                        //SqlCommand cmd5 = new SqlCommand("SP_InsertNew", con);
                                        //cmd5.CommandType = CommandType.StoredProcedure;
                                        //cmd5.Parameters.AddWithValue("@CustomerName", cm.Name);
                                        //cmd5.Parameters.AddWithValue("@Code", cm.CustomerCode);
                                        //con.Open();
                                        //cmd5.ExecuteNonQuery();
                                        //con.Close();
                                        //cmd3.Parameters.AddWithValue("@CountNum", cm.CustomerDetails.Count);
                                        
                                            SqlCommand cmd6 = new SqlCommand("SP_InsertDetail", con);
                                            cmd6.CommandType = CommandType.StoredProcedure;
                                            //cmd4.Parameters.AddWithValue("@CustomerName", cm.Name);
                                            //cmd3.Parameters.AddWithValue("@Code", cm.CustomerCode);
                                            //cmd3.Parameters.AddWithValue("@Count", cm.CustomerDetails.Count);
                                            cmd6.Parameters.AddWithValue("@AddressLine1", item.AddressLine1);
                                            cmd6.Parameters.AddWithValue("@AddressLine2", item.AddressLine2);
                                            cmd6.Parameters.AddWithValue("@City", item.City);
                                            cmd6.Parameters.AddWithValue("@State", item.State);
                                            cmd6.Parameters.AddWithValue("@PinCode", item.PinCode);
                                            cmd6.Parameters.AddWithValue("@PhoneNumber", item.PhoneNumber);

                                            con.Open();
                                            cmd6.ExecuteNonQuery();
                                            con.Close();
                                        

                                    }
                                }
                            }

                            foreach (var item in cm.CustomerItems)
                            {
                                if (item.ItemId != null)
                                {
                                    SqlCommand cmd4 = new SqlCommand("SP_UpdateItem", con);
                                    cmd4.CommandType = CommandType.StoredProcedure;
                                    cmd4.Parameters.AddWithValue("@Name", item.Item);
                                    cmd4.Parameters.AddWithValue("@Qty", item.Qty);
                                    cmd4.Parameters.AddWithValue("@Rate", item.Rate);
                                    cmd4.Parameters.AddWithValue("@Amt", item.Amount);
                                    cmd4.Parameters.AddWithValue("@id", id);
                                    cmd4.Parameters.AddWithValue("@ItemId", item.ItemId);

                                    con.Open();
                                    cmd4.ExecuteNonQuery();
                                    con.Close();
                                }
                                else
                                {

                                    using (con)
                                    {
                                        SqlCommand cmd5 = new SqlCommand("SP_InsertItem", con);
                                        cmd5.CommandType = CommandType.StoredProcedure;
                                        cmd5.Parameters.AddWithValue("@Name", item.Item);
                                        cmd5.Parameters.AddWithValue("@Qty", item.Qty);
                                        cmd5.Parameters.AddWithValue("@Rate", item.Rate);
                                        cmd5.Parameters.AddWithValue("@Amt", item.Amount);


                                        con.Open();
                                        cmd5.ExecuteNonQuery();
                                        con.Close();


                                    }
                                }
                            }



                            ModelState.Clear();
                            msg = "Data Updated Successfully!";
                            ViewBag.Message = msg;
                            return View("Create", cm);

                        }



                    }
                    catch (Exception)
                    {

                        msg = "An Error Occured... Try Again!";
                        ViewBag.Message = msg;
                        return View("Create", cm);
                    }
                }

                return View("Create", cm);




            }
            else
            {

                //code = Convert.ToInt32(TempData["ID"]);

                if (ModelState.IsValid)
                {

                    try
                    {
                        var constr = ConfigurationManager.ConnectionStrings["CustomerEntities"].ToString();
                        
                        SqlConnection con = new SqlConnection(constr);
                        using (con)
                        {
                            SqlCommand cmd3 = new SqlCommand("SP_InsertNew", con);
                            cmd3.CommandType = CommandType.StoredProcedure;
                            cmd3.Parameters.AddWithValue("@CustomerName", cm.Name);
                            cmd3.Parameters.AddWithValue("@Code", cm.CustomerCode);
                            cmd3.Parameters.AddWithValue("@TotalQty", cm.TotalQty);
                            cmd3.Parameters.AddWithValue("@GrossAmt", cm.GrossAmt);
                            cmd3.Parameters.AddWithValue("@DiscPer", cm.DiscPer);
                            cmd3.Parameters.AddWithValue("@DiscAmt", cm.DiscAmt);
                            cmd3.Parameters.AddWithValue("@NetAmt", cm.NetAmt);
                            cmd3.Parameters.AddWithValue("@SatrtDate", cm.SatrtDate);
                            cmd3.Parameters.AddWithValue("@Days", cm.Days);
                            cmd3.Parameters.AddWithValue("@EndDate", cm.EndDate);
                            cmd3.Parameters.AddWithValue("@NewSatrtDate", cm.NewSatrtDate);
                            cmd3.Parameters.AddWithValue("@NewDays", cm.NewDays);
                            cmd3.Parameters.AddWithValue("@NewEndDate", cm.NewEndDate);

                            con.Open();
                            cmd3.ExecuteNonQuery();
                            con.Close();
                            //cmd3.Parameters.AddWithValue("@CountNum", cm.CustomerDetails.Count);
                            foreach (var item in cm.CustomerDetails)
                                {
                                    SqlCommand cmd4 = new SqlCommand("SP_InsertDetail", con);
                                    cmd4.CommandType = CommandType.StoredProcedure;
                                    //cmd4.Parameters.AddWithValue("@CustomerName", cm.Name);
                                    //cmd3.Parameters.AddWithValue("@Code", cm.CustomerCode);
                                    //cmd3.Parameters.AddWithValue("@Count", cm.CustomerDetails.Count);
                                    cmd4.Parameters.AddWithValue("@AddressLine1", item.AddressLine1);
                                    cmd4.Parameters.AddWithValue("@AddressLine2", item.AddressLine2);
                                    cmd4.Parameters.AddWithValue("@City", item.City);
                                    cmd4.Parameters.AddWithValue("@State", item.State);
                                    cmd4.Parameters.AddWithValue("@PinCode", item.PinCode);
                                    cmd4.Parameters.AddWithValue("@PhoneNumber", item.PhoneNumber);
                                    
                                    con.Open();
                                    cmd4.ExecuteNonQuery();
                                    con.Close();
                                }
                            foreach (var item in cm.CustomerItems)
                            {
                                SqlCommand cmd5 = new SqlCommand("SP_InsertItem", con);
                                cmd5.CommandType = CommandType.StoredProcedure;
                                cmd5.Parameters.AddWithValue("@Name", item.Item);
                                cmd5.Parameters.AddWithValue("@Qty", item.Qty);
                                cmd5.Parameters.AddWithValue("@Rate", item.Rate);
                                cmd5.Parameters.AddWithValue("@Amt", item.Amount);
                               

                                con.Open();
                                cmd5.ExecuteNonQuery();
                                con.Close();
                            }



                            ModelState.Clear();
                            msg = "Data Inserted Successfully!";
                            ViewBag.Message = msg;
                            return View("Create", cm);

                        }



                    }
                    catch (Exception)
                    {

                        msg = "An Error Occured... Try Again!";
                        ViewBag.Message = msg;
                        return View("Create", cm);
                    }
                }
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                return View("Create", cm);



            }
                   
           
            
        }
       
        public ActionResult GetAllCustDetails(string Msg)
        {
            //string msg = "An Error Occured... Try Again!";
            //ViewBag.Message = msg;
            if (Msg != null)
            {
                ViewBag.Message = Msg;
            }

            var constr = ConfigurationManager.ConnectionStrings["CustomerEntities"].ToString();
            SqlConnection con = new SqlConnection(constr);
            DataTable dt = new DataTable();
            
            using (con)
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select  a.CustomerId, a.Name, COUNT(a.CustomerId), a.NetAmt from dbo.Customer_Master a join dbo.Customer_Details b on a.CustomerId = b.Cust_ID where b.IsDeleted = 1 group by a.CustomerId, a.Name, a.NetAmt", con);
                da.Fill(dt);
                

            }
            
            return View(dt);
        }

        public ActionResult Delete(int id)
        {
            string msg = string.Empty;
            try
            {
                var constr = ConfigurationManager.ConnectionStrings["CustomerEntities"].ToString();

                SqlConnection con = new SqlConnection(constr);
                using (con)
                {
                    SqlCommand cmd3 = new SqlCommand("SP_DeleteCust", con);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.AddWithValue("@id", id);
                    

                    con.Open();
                    cmd3.ExecuteNonQuery();
                    con.Close();
                    msg = "Data Deleted Successfully!";
                    //ViewBag.Message = msg;
                    return RedirectToAction("GetAllCustDetails", new {Msg = msg });
                }


            }
            catch (Exception)
            {

                msg = "An Error Occured... Try Again!";
                ViewBag.Message = msg;
                return View("GetAllCustDetails");
            }

            //return RedirectToAction("GetAllCustDetails");
        }

        public ActionResult GetEmployee()
        {
            var constr = ConfigurationManager.ConnectionStrings["CustomerEntities"].ToString();
            SqlConnection con = new SqlConnection(constr);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();



            using (con)
            {
                con.Open();

                SqlDataAdapter da1 = new SqlDataAdapter("select a.EmployeeId, a.EmployeeName, COUNT(b.EmployeeId) from dbo.EmployeeMaster a left join dbo.TransactionTable b on a.EmployeeId = b.EmployeeId GROUP BY a.EmployeeId, b.EmployeeId, a.EmployeeName ", con);
               
                da1.Fill(dt1);

            }

            return View(dt1);
            
        }

        //public ActionResult AddTransaction(EmployeeMaster em)
        //{
        //    string msg = string.Empty;
        //    try
        //    {

        //        var constr = ConfigurationManager.ConnectionStrings["CustomerEntities"].ToString();

        //        SqlConnection con = new SqlConnection(constr);
        //        using (con)
        //        {

        //            SqlCommand cmd3 = new SqlCommand("SP_InsertTransaction", con);
        //            cmd3.CommandType = CommandType.StoredProcedure;
        //            cmd3.Parameters.AddWithValue("@EmployeeId", em.EmployeeId);
        //            cmd3.Parameters.AddWithValue("@EmployeeName", em.EmployeeName);
        //            cmd3.Parameters.AddWithValue("@TransactionDate", em.Transaction.TransactionDate);
        //            cmd3.Parameters.AddWithValue("@Amount", em.Transaction.Amount);
                   
        //            con.Open();
        //            cmd3.ExecuteNonQuery();
        //            con.Close();
        //            msg = "Data Inserted Successfully!";
        //            ViewBag.Message = msg;
        //            return View("Create", em);

        //        }



        //    }
        //    catch (Exception)
        //    {

        //        msg = "An Error Occured... Try Again!";
        //        ViewBag.Message = msg;
        //        return View("Create", em);
        //    }
            
        //}
    }
}

