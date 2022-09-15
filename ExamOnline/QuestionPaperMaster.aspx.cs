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
    public partial class QuestionPaperMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.CheckBox.DisabledCssClass = null;
            if (!IsPostBack)
            {
                GetQuestionPaperMaster();
                frmQuestionPaperMaster.Style.Add("display", "none");
            }
        }

        protected void btnQuestionPaperMaster_Click(object sender, EventArgs e)
        {
            frmQuestionPaperMaster.Style.Add("display", "flex");
            tblQuestionPaperMaster.Style.Add("display", "none");
        }

        protected void lstQuestionPaperMaster_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label lblsName = e.Item.FindControl("lblsName") as Label;
            Label lblNoQues = e.Item.FindControl("lblNoofQues") as Label;
            Label lbltime = e.Item.FindControl("lblTime") as Label;
            Label lblstatus = e.Item.FindControl("lblStatus") as Label;
            HiddenField hdn = e.Item.FindControl("hdnId") as HiddenField;
            hdQuestionPaperMasterId.Value = hdn.Value;
            if (!string.IsNullOrEmpty(lblsName.Text))
            {
                if (e.CommandName == "CatEdit")
                {
                    frmQuestionPaperMaster.Style.Add("display", "flex");
                    tblQuestionPaperMaster.Style.Add("display", "none");
                    txtQuestionPaperName.Text = lblsName.Text;
                    txtNoofQues.Text = lblNoQues.Text;
                    txtTime.Text = lbltime.Text;
                    chkStatus.Checked = (lblstatus.Text.ToUpper() == "ACTIVE");
                }
                else if (e.CommandName == "CatDelete")
                {
                    DeleteCategory(Convert.ToInt32(hdQuestionPaperMasterId.Value));
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdminDL objAdminCls = new AdminDL();
            EntityLayer.QuestionPaperMaster objQuestionPaperMaster = new EntityLayer.QuestionPaperMaster();
            if (Convert.ToInt32(hdQuestionPaperMasterId.Value) > 0)
            {
                hdMessage.Value = "Question Paper Master Update |";
                objQuestionPaperMaster.QuestionPaperMasterId = Convert.ToInt32(hdQuestionPaperMasterId.Value);
            }
            else
            {
                hdMessage.Value = "Question Paper Master Insert |";
                objQuestionPaperMaster.QuestionPaperMasterId = 0;
            }
            objQuestionPaperMaster.sName = txtQuestionPaperName.Text.Trim();
            objQuestionPaperMaster.NumberofQuestions = Convert.ToInt32(txtNoofQues.Text.Trim());
            objQuestionPaperMaster.Time = Convert.ToInt32(txtTime.Text.Trim());
            objQuestionPaperMaster.bActive = chkStatus.Checked;
            int Response = objAdminCls.SetQuestionPaperMaster(objQuestionPaperMaster);
            if (Response > 0)
            {
                GetQuestionPaperMaster();
                //ClearControl();
                hdMessage.Value += "Data saved successfully";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Successmsg()", true);
                frmQuestionPaperMaster.Style.Add("display", "none");
                tblQuestionPaperMaster.Style.Add("display", "block");
            }
            else if (Response == 0)
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
            frmQuestionPaperMaster.Style.Add("display", "none");
            tblQuestionPaperMaster.Style.Add("display", "flex");
            resetControl();
        }

        private void GetQuestionPaperMaster()
        {
            AdminDL objAdminCls = new AdminDL();
            hdMessage.Value = "Question Paper Master |";
            DataSet ds = objAdminCls.GetAllQuestionPaperMaster();
            lstQuestionPaperMaster.DataSource = ds.Tables[0];
            lstQuestionPaperMaster.DataBind();
            resetControl();
        }

        private void resetControl()
        {
            txtQuestionPaperName.Text = "";
            txtNoofQues.Text = "";
            txtTime.Text = "";
            chkStatus.Checked = true;
            hdQuestionPaperMasterId.Value = "0";
            frmQuestionPaperMaster.Style.Add("display", "none");
            tblQuestionPaperMaster.Style.Add("display", "flex");
        }

        private void DeleteCategory(int idQuestionPaperMaster)
        {
            AdminDL objAdminCls = new AdminDL();
            hdMessage.Value = "Question Paper Master Delete |";
            int Response = objAdminCls.DeleteQuestionPaperMaster(idQuestionPaperMaster);
            if (Response > 0)
            {
                GetQuestionPaperMaster();
                hdMessage.Value += "Question Paper Master Delete successfully";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Successmsg()", true);
                frmQuestionPaperMaster.Style.Add("display", "none");
                tblQuestionPaperMaster.Style.Add("display", "block");
            }
            else if (Response == 0)
            {
                hdMessage.Value += "Section Master not exists.";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Errormsg()", true);
            }
        }
    }
}