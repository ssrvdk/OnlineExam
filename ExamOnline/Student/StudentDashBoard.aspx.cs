using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnline.Student
{
    public partial class StudentDashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isSessionUpdate = false;
            if (Session["StudentId"] == null)
            {
                HttpCookie reqCookies = Request.Cookies["OnlineExamStudentInfo"];
                if (reqCookies != null)
                {
                    string rdata = reqCookies["Studata"].ToString();
                    string dData = CommanClasses.Decrypt(rdata);
                    string[] words = dData.Split('~');
                    if (words.Count() == 2)
                    {
                        int StudentId = Convert.ToInt32(words[0]);
                        string StudentName = words[1];
                        
                        Session["StudentId"] = StudentId;
                        Session["StudentName"] = StudentName;
                        isSessionUpdate = true;
                        //Response.Redirect(Request.RawUrl);
                    }
                }
                else
                {
                    Response.Redirect("StudentLogin.aspx");
                }
            }
            else
            {
               // hotalName1.InnerText = (string)(Session["UserName"]);
            }
            if (!IsPostBack)
            {



            }
        }
    }
}