using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnline
{
    public partial class RegistrationAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AdminDL objUserDL = new AdminDL();
            AdminCls objAdminCls = new AdminCls();
            string pass = "";
            if (txtPassword.Text == txtConfirmPassword.Text)
            {
                pass = CommonControl.SHA256Encryption(txtPassword.Text);
            }            
            objAdminCls.sName = txtName.Text.Trim();
            objAdminCls.EmailAddress = txtEmail.Text.Trim();
            objAdminCls.Username = txtUsername.Text.Trim();
            objAdminCls.Password = pass;
            objAdminCls.VerificationCode = Convert.ToString(Guid.NewGuid());
            int ResponseData = objUserDL.RegisterAdmin(objAdminCls);
            if (ResponseData > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "ShowMessage", "showAlertMessage('success','You registered successfully.');", true);
                DataSet ds1 = objUserDL.GetAllClientMaster();
                string host = "", fromMail = "", password = "";
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    host = Convert.ToString(ds1.Tables[0].Rows[0]["host"]);
                    fromMail = Convert.ToString(ds1.Tables[0].Rows[0]["fromEmail"]);
                    password = Convert.ToString(ds1.Tables[0].Rows[0]["sPassword"]);
                }
                if (!string.IsNullOrEmpty(host) && !string.IsNullOrEmpty(fromMail) && !string.IsNullOrEmpty(password))
                {
                    string ResetPasswordURL = ConfigurationManager.AppSettings["VerificationURL"].ToString();
                    string Url = ResetPasswordURL + "?UserName=" + CommonControl.Encrypt(objAdminCls.VerificationCode);
                    string emailTo = txtEmail.Text.Trim();
                    string subject = "Verification";
                    string body = "<br>You are almost there!. To complete the process, please click on the link below to verfiy email address." + "<br>" + "<a href=" + Url + ">" + Url + "</a>";
                    string str = CommonControl.SendEmail(emailTo, subject, body, host, fromMail, password);
                }
                //if (Convert.ToInt16(hdnCheckbox.Value) == 1)
                //{
                //    this.Master.SubscribeEmail(txtREmail.Text.Trim());
                //}
                Response.Redirect("~/index.aspx");
            }
            else if (ResponseData == 0)
            {
                lblMessage.Text = "You are already registered. Please log in.";
            }
        }
    }
}