using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MySql.Data.MySqlClient;

namespace Attendance.Controllers
{
    public class HomeController : Controller
    {
        string myConnectionString = "server=localhost;uid=root;" + "database=employee_attendance";
        [HttpGet]
        public ActionResult Login()
        {                      
            ViewBag.Message = "Login.";
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.loginDetail obj)
        {
            string newRole = "";
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select Role from employees o where o.username=@userName and password=@password", conn);
                    cmd.Parameters.AddWithValue("@userName", obj.userName);
                    cmd.Parameters.AddWithValue("@password", obj.password);
                    try
                    {
                        conn.Open();
                        newRole = Convert.ToString(cmd.ExecuteScalar());
                        if (newRole == "")
                        {
                            return View("Login");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                newRole = "";
            }
            Session["Role"] = newRole;
            Session["uid"] = obj.userName;
            Session["pwd"] = obj.password;
            return View("Index");
        }

        [HttpGet]
        public ActionResult UserRegistration()
        {
            ViewBag.Message = "User Registration.";
            return View();
        }

        [HttpPost]
        public ActionResult UserRegistration(Models.RegistrationDetails obj)
        {
            int count = 0;
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    MySql.Data.MySqlClient.MySqlCommand cmd1 = new MySql.Data.MySqlClient.MySqlCommand("select Count(*) from employees o where o.username=@username", conn);
                    cmd1.Parameters.AddWithValue("@username", obj.Name);
                    //cmd1.Parameters.AddWithValue("@password", obj.UPassword);
                    try
                    {
                        conn.Open();
                        count = Convert.ToInt32(cmd1.ExecuteScalar());
                        if (count > 0)
                        {
                            //ViewBag["MSG"]= "User Allready Exist.";
                            ViewBag.Message = "User Name Allready Exist.";
                            return View("UserRegistration");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("insert into employees(username,fname,lname,phone,designation,email,password,Role) values(@username,@fname,@lname,@phone,@designation,@email,@password,@Role)", conn);
                    cmd.Parameters.AddWithValue("@userName", obj.Name);
                    cmd.Parameters.AddWithValue("@fname", obj.FirstName);
                    cmd.Parameters.AddWithValue("@lname", obj.LastName);
                    cmd.Parameters.AddWithValue("@phone", obj.Phone);
                    cmd.Parameters.AddWithValue("@designation", obj.Designation);
                    cmd.Parameters.AddWithValue("@email", obj.Email);
                    cmd.Parameters.AddWithValue("@password", obj.UPassword);
                    cmd.Parameters.AddWithValue("@Role", obj.Role);
                    try
                    {
                        //conn.Open();
                        count = cmd.ExecuteNonQuery();
                        if (count == 0)
                        {
                            return View("UserRegistration");
                        }
                    }
                    catch (Exception ex)
                    {
                        return View("UserRegistration");
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                count = 0;
            }
            return View("Login");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Attendance()
        {
            if ((Session["Role"]).ToString() == "Admin")
            {
                return View("Index");
            }
            return View();
        }

        [HttpPost]
        public JsonResult SaveAttendance()
        {
            string Data = "0";
            string Msg = "";
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select count(*) from attendances o where o.username = @userName and o.attendances_date = current_date", conn);
                    cmd.Parameters.AddWithValue("@userName", Convert.ToString(Session["uid"]));
                    try
                    {
                        conn.Open();
                        Data = Convert.ToString(cmd.ExecuteScalar());
                        if (Convert.ToInt32(Data) == 0)
                        {
                            MySql.Data.MySqlClient.MySqlCommand cmd1 = new MySql.Data.MySqlClient.MySqlCommand("insert into attendances(username) values(@userName)", conn);
                            cmd1.Parameters.AddWithValue("@userName", Convert.ToString(Session["uid"]));
                            try
                            {
                                Data = Convert.ToString(cmd1.ExecuteNonQuery());
                                if (Convert.ToInt32(Data) <= 0)
                                {
                                    Msg = "Error.";
                                }
                                else
                                {
                                    Msg = "Attendance Mark Successfully.";
                                }
                            }
                            catch (Exception ex)
                            {
                                Msg = ex.Message;
                            }
                        }
                        else
                        {
                            Msg = "Already Attendance Done.";
                        }
                    }
                    catch (Exception ex)
                    {
                        Msg = ex.Message;
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Msg = ex.Message;
            }
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session["uid"] = null;
            Session["pwd"] = null;
            return RedirectToAction("Login");
        }
        public ActionResult TimeSheet()
        {
            return View();
        }

        public JsonResult Proceed(Models.TimesheetDetails obj)
        {
            List<Models.TimesheetDet> liTimesheet = new List<Models.TimesheetDet>();
            DataTable dtable;
            //var jsonstring;
            //List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>(); 
            //Dictionary<string, object> rowelement; 
            try
            {
                using (MySqlConnection con = new MySqlConnection(myConnectionString))
                {
                    string Query = "";
                    if ((Session["Role"]).ToString() == "Admin")
                    {
                        Query = "select o.username,o.attendances_datetime,Date(o.attendances_date) attendances_date,'Present' as status,Remark FROM attendances o where o.attendances_date between @FromDate and @ToDate Order By o.attendances_date";
                    }
                    else
                    {
                        Query = "select o.username,o.attendances_datetime,Date(o.attendances_date) attendances_date,'Present' as status,Remark FROM attendances o where o.attendances_date between @FromDate and @ToDate And o.username = '" + Session["uid"].ToString() + "' Order By o.attendances_date";
                    }
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(Query, con);
                    //MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select o.username,o.attendances_datetime,Date(o.attendances_date) attendances_date,'Present' as status FROM attendances o where o.attendances_date between @FromDate and @ToDate", con);
                    cmd.Parameters.AddWithValue("@FromDate", (obj.FromDate).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@ToDate", (obj.ToDate).ToString("yyyy-MM-dd"));
                    con.Open();
                    //using (MySqlCommand cmd = new MySqlCommand("select * from attendances"))
                    //{
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            dtable = new DataTable();
                            sda.Fill(dt);
                            dtable = dt;
                        }
                    //}
                    }


                    if (dtable.Rows.Count > 0) //if data is there in dt(dataTable)  
                    {
                        foreach (DataRow dr in dtable.Rows)
                        {
                            Models.TimesheetDet timeSheet = new Models.TimesheetDet();
                            timeSheet.username = Convert.ToString(dr["username"]);
                            timeSheet.attendancesDatetime = Convert.ToString(dr["attendances_datetime"]);
                            timeSheet.attendancesDate = Convert.ToDateTime(dr["attendances_date"]).ToString("yyyy-MM-dd");
                            timeSheet.Status = Convert.ToString(dr["status"]);
                            timeSheet.Remark = Convert.ToString(dr["Remark"]);
                            //rowelement = new Dictionary<string, object>();
                            //foreach (DataColumn col in dtable.Columns)
                            //{
                            //    rowelement.Add(col.ColumnName, dr[col]); //adding columnn  
                            //}
                            liTimesheet.Add(timeSheet);
                        }
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //MessageBox.Show(ex.Message);
            }
            System.Web.Script.Serialization.JavaScriptSerializer Serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            /*var jsonstring =*/ /*Serializer.Serialize(liTimesheet); */
            return Json(Serializer.Serialize(liTimesheet));
           
        }

        public ActionResult RegularizeAttendance()
        {
            if ((Session["Role"]).ToString() == "Admin")
            {
                return View("Index");
            }
            return View();
        }

        [HttpPost]
        public JsonResult RegularizeAttendance(Models.RegularizeDetails obj)
        {
            string Data = "0";
            string Msg = "";
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select count(*) from attendances o where o.username = @userName and o.attendances_date ='"+ obj.txtRegulDate.ToString("yyyy-MM-dd") + "'", conn);
                    cmd.Parameters.AddWithValue("@userName", Convert.ToString(Session["uid"]));
                    try
                    {
                        conn.Open();
                        Data = Convert.ToString(cmd.ExecuteScalar());
                        if (Convert.ToInt32(Data) == 0)
                        {
                            MySql.Data.MySqlClient.MySqlCommand cmd1 = new MySql.Data.MySqlClient.MySqlCommand("insert into attendances(username,attendances_date,Remark) values(@userName,@RegularizeDate,@Remark)", conn);                           
                            cmd1.Parameters.AddWithValue("@userName", Convert.ToString(Session["uid"]));
                            cmd1.Parameters.AddWithValue("@RegularizeDate", obj.txtRegulDate.ToString("yyyy-MM-dd"));
                            cmd1.Parameters.AddWithValue("@Remark", "Regularize");
                            try
                            {
                                Data = Convert.ToString(cmd1.ExecuteNonQuery());
                                if (Convert.ToInt32(Data) <= 0)
                                {
                                    Msg = "Error.";
                                }
                                else
                                {
                                    Msg = "Attendance Regulerize Successfully.";
                                }
                            }
                            catch (Exception ex)
                            {
                                Msg = ex.Message;
                            }
                        }
                        else
                        {
                            Msg = "Already Attendance Done.";
                        }
                    }
                    catch (Exception ex)
                    {
                        Msg = ex.Message;
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Msg = ex.Message;
            }
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

    }
}