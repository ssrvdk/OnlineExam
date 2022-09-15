using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnline
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Clear();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtpassword.Text))
            {
                AdminDL objAdminCls = new AdminDL();
                hdMessage.Value = "Login |";
                String pass = CommonControl.SHA256Encryption(txtpassword.Text);
                DataSet ds = objAdminCls.AdminLogin(txtUserName.Text, pass);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(ds.Tables[0].Rows[0]["isEmailVerified"]) == "")
                    {
                        string str = "Your email address is not Verified.";
                        Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "ShowMessageForm();", true);
                        lblMessage.Text = str;
                    }
                    else
                    {
                        string str = "LoginSuccessfully";
                        Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "ShowMessageForm();", true);
                        lblMessage.Text = str;
                        Session["UserId"] = ds.Tables[0].Rows[0]["UserId"];
                        Session["Name"] = ds.Tables[0].Rows[0]["Name"];
                        Session["UserName"] = ds.Tables[0].Rows[0]["UserName"];
                        Session["Password"] = ds.Tables[0].Rows[0]["Password"];
                        Response.Redirect("StudentDetails.aspx");
                    }
                }
                else
                {
                    hdMessage.Value += "No active user exist.";
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "ClosePopup", "Errormsg();", true);
                }
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblMessage.Text) && lblMessage.Text.Equals("Your email address is not Verified."))
            {
                Response.Redirect("index.aspx");
            }
        }
    }
}