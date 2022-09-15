using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using EntityLayer;

namespace ExamOnline.Student
{
    public partial class studentVerification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string nid = Request.QueryString["Id"];
            if (!string.IsNullOrEmpty(nid))
            {
                int id = CommanClasses.checkParse(CommanClasses.Decrypt(nid));
                StudentDL studentDL = new StudentDL();
                int isVerified = studentDL.VerifyStudent(id);
                if (isVerified == 1)
                {
                    lblMessage.Text = "Student Verified successfully";
                }
                else
                {
                    lblMessage.Text = "verification code not correct";
                }
            }
        }

    }
}