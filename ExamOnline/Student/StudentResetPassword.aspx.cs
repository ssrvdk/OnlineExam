using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnline.Student
{
    public partial class StudentResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string UserName = Request.QueryString["UserName"];
            if (!string.IsNullOrEmpty(UserName))
            {
                string recData = CommonControl.Decrypt(UserName);
                StudentDL objAdminDL = new StudentDL();
                EntityLayer.StudentDetails admin = new EntityLayer.StudentDetails();
                admin.EmailAddress = recData;
                admin.Password = CommonControl.SHA256Encryption(txtpassword.Text.Trim());
                int response = objAdminDL.ResetPassword(recData, CommonControl.SHA256Encryption(txtpassword.Text.Trim()));
                if (response > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "ShowMessageForm();", true);
                    lblMessage.Text = "Password set successfully";
                }
                else
                {

                    Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "ShowMessageForm();", true);
                    lblMessage.Text = "Password not set,Please try again";
                }

            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

        }
    }
}