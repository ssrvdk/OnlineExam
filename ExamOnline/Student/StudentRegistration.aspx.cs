using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer;
using DataLayer;
using System.Configuration;

namespace ExamOnline.Student
{
    public partial class StudentRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            EntityLayer.StudentDetails studentDetails = new EntityLayer.StudentDetails();
            studentDetails.sName = txtName.Text;
            studentDetails.EmailAddress = txtEmail.Text;
            studentDetails.Password = CommanClasses.Encrypt(txtPassword.Text.Trim());
            StudentDL studentDL = new StudentDL();
            int id = studentDL.RegisterStudent(studentDetails);
            if (id == 0)
            {
                lbError.Text = "Student Already registor.";
            }
            else
            {
                string sUrl = ConfigurationManager.AppSettings["VerificationStudentURL"].ToString();
                sUrl += "?id=" + CommanClasses.Encrypt(id.ToString());
                string sMessage = "Hi " + txtName.Text + ",<br>";
                sMessage += "<a href='" + sUrl + "'>" + sUrl + "</a>";
                CommanClasses.SendEmail(txtEmail.Text, "Verification Message", sMessage );
                Response.Redirect("StudentDashBoard.aspx");
            }
            //DL_HotalManagment objHotalManagment = new DL_HotalManagment();
            //AdminCls admin = new AdminCls();
            //admin.Username = txtUsername.Text.Trim();
            //admin.Password = CommanClasses.Encrypt(txtpassword.Text.Trim());
            //DataSet ds = objHotalManagment.AdminLogin(admin);
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{

            //    if (ds.Tables[0].Rows[0]["PlanId"].ToString() != "0")
            //    {
            //        Session["UserId"] = ds.Tables[0].Rows[0]["ID"];
            //        Session["UserName"] = ds.Tables[0].Rows[0]["Username"];
            //        Session["Type"] = ds.Tables[0].Rows[0]["Type"];
            //        string typ = Convert.ToString(ds.Tables[0].Rows[0]["Type"]);
            //        if (typ.Trim() != "1")
            //        {
            //            DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndDate"]);
            //            int remainDays = (dt - CommanClasses.CurrentDateTime()).Days;
            //            DataSet dsData = objHotalManagment.GetAppSettingById(1);
            //            Session["Message"] = "";
            //            if (dsData != null)
            //            {
            //                if (remainDays <= Convert.ToInt32(dsData.Tables[0].Rows[0]["AppValue"]))
            //                {
            //                    Session["Message"] = "Your plan is going to expire in " + Convert.ToString(remainDays) + " days. Please contact to admin.";
            //                }
            //            }
            //        }

            //        DataColumnCollection columns = ds.Tables[0].Columns;
            //        if (columns.Contains("Hotelname") && columns.Contains("Logo"))
            //        {
            //            Session["Hotelname"] = ds.Tables[0].Rows[0]["Hotelname"];
            //            Session["Logo"] = ds.Tables[0].Rows[0]["Logo"];
            //            Session["Address"] = ds.Tables[0].Rows[0]["Address"];
            //            Session["PropertyName"] = ds.Tables[0].Rows[0]["PropertyName"];

            //        }
            //        HttpCookie userInfo = new HttpCookie("travinitiesUserInfo");
            //        string str = Convert.ToString(Session["UserId"]) + "~" +
            //                Convert.ToString(Session["UserName"]) + "~" +
            //                Convert.ToString(Session["Type"]) + "~" +
            //                Convert.ToString(Session["Message"]) + "~" +
            //                Convert.ToString(Session["Hotelname"]) + "~" +
            //                Convert.ToString(Session["Logo"]) + "~" +
            //                Convert.ToString(Session["Address"]) + "~" +
            //                Convert.ToString(ds.Tables[0].Rows[0]["Phoneno"]) + "~" +
            //                Convert.ToString(ds.Tables[0].Rows[0]["Mobileno"]) + "~" +
            //                Convert.ToString(ds.Tables[0].Rows[0]["LocationLink"]);

            //        userInfo["Rdata"] = CommanClasses.Encrypt(str);
            //        userInfo.Expires.Add(new TimeSpan(20, 0, 0));
            //        Response.Cookies.Add(userInfo);

            //        Response.Redirect("MainDashBoard.aspx");
            //    }
            //    else
            //    {
            //        lbError.Text = "No Active Plan Available. Please Contact Administrator.";
            //    }
            //}
            //else
            //{
            //    lbError.Text = "Invalid Credentials.Enter a valid credentials";
            //}
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

        }
    }
}