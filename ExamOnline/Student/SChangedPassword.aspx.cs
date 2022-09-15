using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnline.Student
{
    public partial class SChangedPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblName.Text = Session["UserName"].ToString();
            hdUserId.Value = Session["StudentId"].ToString();
            hdUserName.Value = Session["StudentName"].ToString();
            hdCurrentPassword.Value = Session["Password"].ToString();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                StudentDL objAdmin = new StudentDL();
                EntityLayer.StudentDetails objAdminCls = new EntityLayer.StudentDetails();
                hdMessage.Value = "Change Password |";
                lblMess.Text = hdMessage.Value;
                objAdminCls.sName = Convert.ToString(hdUserName.Value);
                objAdminCls.Id = Convert.ToInt32(hdUserId.Value);
                objAdminCls.Password = CommonControl.SHA256Encryption(txtCurrentPassword.Text);
                string CPassword = CommonControl.SHA256Encryption(txtCurrentPassword.Text);
                objAdminCls.ConfirmPassword = CommonControl.SHA256Encryption(txtConfirmPassword.Text);
                int Respon = 0;
                if (hdCurrentPassword.Value == CPassword)
                {
                    Respon = objAdmin.StudentChangePassword(objAdminCls);
                }
                else
                {
                    Respon = 0;
                }

                if (Respon > 0)
                {
                    string str = "Change Password Successfully";
                    lblMessage.Text = str;
                    Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "ShowMessageForm();", true);
                    Response.Redirect("StudentDashBoard.aspx");
                }
                else
                {
                    hdMessage.Value += "No active user exist.";
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "ClosePopup", "Errormsg();", true);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //frmChangedPassword.Style.Add("display", "none");
            Response.Redirect("Dashboard.aspx");
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

        }
    }
}