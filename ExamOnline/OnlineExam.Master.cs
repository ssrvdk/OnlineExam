using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnline
{
    public partial class OnlineExam : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
           // Response.Cookies.Clear();
           // Response.Cookies["OnlineExamStudentInfo"].Expires = DateTime.Now.AddDays(-1);
            Response.Redirect("Index.aspx");
        }
    }
}