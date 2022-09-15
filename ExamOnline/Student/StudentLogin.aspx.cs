using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnline.Student
{
    public partial class StudentLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Clear();
            }
        }
        //protected void btnForgetPassword_Click(object sender, EventArgs e)
        //{
        //}
            
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            EntityLayer.StudentDetails studentDetails = new EntityLayer.StudentDetails();
            studentDetails.EmailAddress = txtEmail.Text;
            studentDetails.Password = CommanClasses.SHA256Encryption(txtPassword.Text.Trim());
            StudentDL studentDL = new StudentDL();
            DataSet ds = studentDL.LoginStudent(studentDetails);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["nError"].ToString() != "0")
                {
                    lblMess.Text = "Please enter valid detail.";
                }
                else
                {
                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0 && Convert.ToString(ds.Tables[1].Rows[0]["bVerified"]) == "1")
                    {
                        Session["StudentId"] = ds.Tables[1].Rows[0]["StudentDetailsId"];
                        Session["StudentName"] = ds.Tables[1].Rows[0]["Name"];
                        Session["Password"] = ds.Tables[1].Rows[0]["Password"];
                        HttpCookie userInfo = new HttpCookie("OnlineExamStudentInfo");
                        string str = Convert.ToString(Session["StudentId"]) + "~" + Convert.ToString(Session["StudentName"]);
                        userInfo["Studata"] = CommanClasses.Encrypt(str);
                        userInfo.Expires.Add(new TimeSpan(20, 0, 0));
                        Response.Cookies.Add(userInfo);

                        Response.Redirect("StudentDashBoard.aspx");
                    }
                    else
                    {
                        lblMess.Text = "Please verified your email.";
                    }
                }
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

        }
    }
}