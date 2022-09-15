using DataLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnline.Student
{
    public partial class StudentForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            StudentDL objAdminCls = new StudentDL();
            if (objAdminCls.ValidateStudent(txtEmail.Text.Trim()))
            {
                DataSet ds = objAdminCls.GetAllClientMaster();
                string host = "", fromMail = "", password = "";
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    host = Convert.ToString(ds.Tables[0].Rows[0]["host"]);
                    fromMail = Convert.ToString(ds.Tables[0].Rows[0]["fromEmail"]);
                    password = Convert.ToString(ds.Tables[0].Rows[0]["sPassword"]);
                }
                if (!string.IsNullOrEmpty(host) && !string.IsNullOrEmpty(fromMail) && !string.IsNullOrEmpty(password))
                {
                    string ResetPasswordURL = ConfigurationManager.AppSettings["ResetPasswordStudentURL"].ToString();
                    string Url = ResetPasswordURL + "?UserName=" + CommonControl.Encrypt(txtEmail.Text.Trim());
                    string str = CommonControl.SendEmail(txtEmail.Text.Trim(), "Forgot Password", "<br>You are almost there!. To complete the process, please click on the link below to set password." + "<br>" + "<a href=" + Url + ">" + Url + "</a>", host, fromMail, password);
                    Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "ShowMessageForm();", true);
                    lblMessage.Text = str;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "ShowMessageForm();", true);
                    lblMessage.Text = "Host, From mail and Password not found.";
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "ShowMessageForm();", true);
                lblMessage.Text = "User not exists..";
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

        }
    }
}