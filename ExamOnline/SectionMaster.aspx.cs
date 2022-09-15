using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer;

namespace ExamOnline
{
    public partial class SectionMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.CheckBox.DisabledCssClass = null;
            if (!IsPostBack)
            {
                GetSectionMaster();
                frmSectionMaster.Style.Add("display", "none");
            }
        }

        private void GetSectionMaster()
        {
            AdminDL objAdminCls = new AdminDL();
            hdMessage.Value = "Section Master |";
            DataSet ds = objAdminCls.GetAllSectionMaster();
            lstSectionMaster.DataSource = ds.Tables[0];
            lstSectionMaster.DataBind();
            resetControl();
        }

        private void resetControl()
        {
            txtSectionName.Text = "";
            chkStatus.Checked = true;
            hdSectionMasterId.Value = "0";
            frmSectionMaster.Style.Add("display", "none");
            tblSectionMaster.Style.Add("display", "flex");
        }

        protected void lstSectionMaster_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label lblsName = e.Item.FindControl("lblsName") as Label;
            Label lblstatus = e.Item.FindControl("lblStatus") as Label;
            HiddenField hdn = e.Item.FindControl("hdnId") as HiddenField;
            hdSectionMasterId.Value = hdn.Value;
            if (!string.IsNullOrEmpty(lblsName.Text))
            {
                if (e.CommandName == "CatEdit")
                {
                    frmSectionMaster.Style.Add("display", "flex");
                    tblSectionMaster.Style.Add("display", "none");
                    txtSectionName.Text = lblsName.Text;
                    chkStatus.Checked = (lblstatus.Text.ToUpper() == "ACTIVE");
                }
                else if (e.CommandName == "CatDelete")
                {
                    DeleteCategory(Convert.ToInt32(hdSectionMasterId.Value));
                }
            }
        }

        private void DeleteCategory(int idSectionMaster)
        {
            AdminDL objAdminCls = new AdminDL();
            hdMessage.Value = "Section Master Delete |";
            int Response = objAdminCls.DeleteSectionMaster(idSectionMaster);
            if (Response > 0)
            {
                GetSectionMaster();
                hdMessage.Value += "Section Master Delete successfully";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Successmsg()", true);
                frmSectionMaster.Style.Add("display", "none");
                tblSectionMaster.Style.Add("display", "block");
            }
            else if (Response == 0)
            {
                hdMessage.Value += "Section Master not exists.";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Errormsg()", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdminDL objAdminCls = new AdminDL();
            EntityLayer.SectionMaster objSectionMaster = new EntityLayer.SectionMaster();
            if (Convert.ToInt32(hdSectionMasterId.Value) > 0)
            {
                hdMessage.Value = "Section Master Update |";
                objSectionMaster.IdSectionMaster = Convert.ToInt32(hdSectionMasterId.Value);
            }
            else
            {
                hdMessage.Value = "Section Master Insert |";
                objSectionMaster.IdSectionMaster = 0;
            }
            objSectionMaster.SectionName = txtSectionName.Text.Trim();
            objSectionMaster.bActive = chkStatus.Checked;
            int Response = objAdminCls.SetSectionMaster(objSectionMaster);
            if (Response > 0)
            {
                GetSectionMaster();
                //ClearControl();
                hdMessage.Value += "Data saved successfully";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Successmsg()", true);
                frmSectionMaster.Style.Add("display", "none");
                tblSectionMaster.Style.Add("display", "block");
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
            frmSectionMaster.Style.Add("display", "none");
            tblSectionMaster.Style.Add("display", "flex");
            resetControl();
        }

        protected void btnSectionMaster_Click(object sender, EventArgs e)
        {
            frmSectionMaster.Style.Add("display", "flex");
            tblSectionMaster.Style.Add("display", "none");
        }
    }
}