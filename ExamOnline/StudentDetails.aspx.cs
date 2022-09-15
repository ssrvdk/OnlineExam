using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnline
{
    public partial class StudentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isSessionUpdate = false;
            System.Web.UI.WebControls.CheckBox.DisabledCssClass = null;
            if (Session["UserId"] != null)
            {
                //HttpCookie reqCookies = Request.Cookies["OnlineExamStudentInfo"];
                //if (reqCookies != null)
                //{
                //    string rdata = reqCookies["Studata"].ToString();
                //    string dData = CommanClasses.Decrypt(rdata);
                //    string[] words = dData.Split('~');
                //    if (words.Count() == 2)
                //    {
                //        int StudentId = Convert.ToInt32(words[0]);
                //        string StudentName = words[1];

                //        Session["StudentId"] = StudentId;
                //        Session["StudentName"] = StudentName;
                //        isSessionUpdate = true;
                //        //Response.Redirect(Request.RawUrl);
                //    }
                //}
                //else
                //{
                //    Response.Redirect("StudentLogin.aspx");
                //}
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
            if (!IsPostBack)
            {
                GetStudentDetails();
                frmStudentDetails.Style.Add("display", "none");
            }
        }

        protected void btnStudentDetails_Click(object sender, EventArgs e)
        {
            frmStudentDetails.Style.Add("display", "flex");
            tblStudentDetails.Style.Add("display", "none");
        }

        protected void lstStudentDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label lblStudentName = e.Item.FindControl("lblstudentName") as Label;
            Label lblAddress = e.Item.FindControl("lblAddress") as Label;
            Label lblCity = e.Item.FindControl("lblCity") as Label;
            Label lblState = e.Item.FindControl("lblState") as Label;
            Label lblEmailAddress = e.Item.FindControl("lblEmailAddress") as Label;
            Label lblMobileNumber = e.Item.FindControl("lblMobileNumber") as Label;
            Label lblstatus = e.Item.FindControl("lblStatus") as Label;
            HiddenField hdn = e.Item.FindControl("hdnId") as HiddenField;
            hdStudentDetailsId.Value = hdn.Value;
            if (!string.IsNullOrEmpty(lblStudentName.Text))
            {
                if (e.CommandName == "CatEdit")
                {
                    frmStudentDetails.Style.Add("display", "flex");
                    tblStudentDetails.Style.Add("display", "none");
                    txtStudentName.Text = lblStudentName.Text;
                    txtAddress.Text = lblAddress.Text;
                    txtCity.Text = lblCity.Text;
                    txtEmailAddress.Text = lblEmailAddress.Text;
                    txtState.Text = lblState.Text;
                    txtMobileNumber.Text = lblMobileNumber.Text;
                    chkStatus.Checked = (lblstatus.Text.ToUpper() == "ACTIVE");
                }
                else if (e.CommandName == "CatDelete")
                {
                    DeleteCategory(Convert.ToInt32(hdStudentDetailsId.Value));
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdminDL objAdminCls = new AdminDL();
            EntityLayer.StudentDetails objStudentDetails = new EntityLayer.StudentDetails();
            if (Convert.ToInt32(hdStudentDetailsId.Value) > 0)
            {
                hdMessage.Value = "Student Details Update |";
                objStudentDetails.Id = Convert.ToInt32(hdStudentDetailsId.Value);
            }
            else
            {
                hdMessage.Value = "Student Details Insert |";
                objStudentDetails.Id = 0;
            }
            objStudentDetails.sName = txtStudentName.Text.Trim();
            objStudentDetails.Address =txtAddress.Text.Trim();
            objStudentDetails.EmailAddress = txtEmailAddress.Text.Trim();
            objStudentDetails.City = txtCity.Text.Trim();
            objStudentDetails.State = txtState.Text.Trim();
            objStudentDetails.MobileNumber = txtMobileNumber.Text.Trim();
            //objStudentDetails.bVerified = chkbVerified.Checked;
            objStudentDetails.bActive = chkStatus.Checked;
            MessageResponse Response = objAdminCls.SetStudentDetails(objStudentDetails);
            if (Response.nError == 0)
            {
                GetStudentDetails();
                //ClearControl();
                hdMessage.Value += "Data saved successfully";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Successmsg()", true);
                frmStudentDetails.Style.Add("display", "none");
                tblStudentDetails.Style.Add("display", "block");
            }
            else if (Response.nError == -1)
            {
                hdMessage.Value += "Data not saved. Because category already exists.";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Errormsg()", true);
            }
            else
            {

                hdMessage.Value += "Data not saved successfully please try again...";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Errormsg()", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            frmStudentDetails.Style.Add("display", "none");
            tblStudentDetails.Style.Add("display", "flex");
            resetControl();
        }

        private void GetStudentDetails()
        {
            AdminDL objAdminCls = new AdminDL();
            hdMessage.Value = "Student Details |";
            DataSet ds = objAdminCls.GetAllStudentDetails();
            lstStudentDetails.DataSource = ds.Tables[0];
            lstStudentDetails.DataBind();
            resetControl();
        }

        private void resetControl()
        {
            txtStudentName.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtEmailAddress.Text = "";
            txtState.Text = "";
            txtMobileNumber.Text = "";
            chkStatus.Checked = true;
            hdStudentDetailsId.Value = "0";
            frmStudentDetails.Style.Add("display", "none");
            tblStudentDetails.Style.Add("display", "flex");
        }

        private void DeleteCategory(int idQuestionPaperMaster)
        {
            AdminDL objAdminCls = new AdminDL();
            hdMessage.Value = "Student Details Delete |";
            int Response = objAdminCls.DeleteStudentDetails(idQuestionPaperMaster);
            if (Response > 0)
            {
                GetStudentDetails();
                hdMessage.Value += "Student Details Delete successfully";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Successmsg()", true);
                frmStudentDetails.Style.Add("display", "none");
                tblStudentDetails.Style.Add("display", "block");
            }
            else if (Response == 0)
            {
                hdMessage.Value += "Student Details not exists.";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Errormsg()", true);
            }
        }
    }
}