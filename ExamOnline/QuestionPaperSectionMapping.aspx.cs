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
    public partial class QuestionPaperSectionMapping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetQuePprSectionMapping();
                frmQuestionPaperSectionMapping.Style.Add("display", "none");
            }
        }

        protected void btnQuestionPaperSectionMapping_Click(object sender, EventArgs e)
        {
            BindSectionDDL();
            BindQuestionDDL();
            frmQuestionPaperSectionMapping.Style.Add("display", "flex");
            tblQuestionPaperSectionMapping.Style.Add("display", "none");
        }

        protected void rptQuestionPaperSectionMapping_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label lbNumberOfQuestion = e.Item.FindControl("lbNumberOfQuestion") as Label;
            Label lblstatus = e.Item.FindControl("lblStatus") as Label;
            HiddenField hdn = e.Item.FindControl("hdnId") as HiddenField;
            HiddenField hdnQuestionId = e.Item.FindControl("hdnQuestionId") as HiddenField;
            HiddenField hdnSectionId = e.Item.FindControl("hdnSectionId") as HiddenField;
            hdQuePprSectionMappingId.Value = hdn.Value;
            if (!string.IsNullOrEmpty(hdQuePprSectionMappingId.Value))
            {
                if (e.CommandName == "CatEdit")
                {
                    BindSectionDDL();
                    BindQuestionDDL();
                    frmQuestionPaperSectionMapping.Style.Add("display", "flex");
                    tblQuestionPaperSectionMapping.Style.Add("display", "none");
                    chkStatus.Checked = (lblstatus.Text.ToUpper() == "ACTIVE");
                }
                else if (e.CommandName == "CatDelete")
                {
                    DeleteQuePprSectionMapping(Convert.ToInt32(hdnQuestionId.Value));
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdminDL objAdminCls = new AdminDL();
            QuePprSectionMapping objQuePprSectionMapping = new QuePprSectionMapping();
            if (Convert.ToInt32(hdQuePprSectionMappingId.Value) > 0)
            {
                hdMessage.Value = "Question Paper Section Mapping Update |";
                objQuePprSectionMapping.QuePprSectionMappingId = Convert.ToInt32(hdQuePprSectionMappingId.Value);
            }
            else
            {
                hdMessage.Value = "Question Paper Section Mapping Insert |";
                objQuePprSectionMapping.QuePprSectionMappingId = 0;
            }
            objQuePprSectionMapping.NumberOfQuestion = Convert.ToInt32(txtNumberOfQuestion.Text.Trim());
            objQuePprSectionMapping.QuestionPaperid = Convert.ToInt32(ddlQuestion.SelectedItem.Value);
            objQuePprSectionMapping.Sectionid = Convert.ToInt32(ddlSection.SelectedItem.Value);
            objQuePprSectionMapping.bActive = chkStatus.Checked;
            MessageResponse Response = objAdminCls.SetQuePprSectionMapping(objQuePprSectionMapping);
            if (Response.nError == 0)
            {
                GetQuePprSectionMapping();
                hdMessage.Value += "Data saved successfully";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Successmsg()", true);
                frmQuestionPaperSectionMapping.Style.Add("display", "none");
                tblQuestionPaperSectionMapping.Style.Add("display", "block");
            }
            else if (Response.nError == -1)
            {
                hdMessage.Value += "Data not saved. Because Question Paper Section Mapping already exists.";
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
            frmQuestionPaperSectionMapping.Style.Add("display", "none");
            tblQuestionPaperSectionMapping.Style.Add("display", "flex");
        }
        private void GetQuePprSectionMapping()
        {
            AdminDL objAdminCls = new AdminDL();
            hdMessage.Value = "Question Paper Section Mapping |";
            DataSet ds = objAdminCls.GetAllQuePprSectionMapping();
            rptQuestionPaperSectionMapping.DataSource = ds.Tables[0];
            rptQuestionPaperSectionMapping.DataBind();
            ResetControl();
        }
        private void ResetControl()
        {
            ddlSection.SelectedIndex = 0;
            ddlQuestion.SelectedIndex = 0;
            txtNumberOfQuestion.Text = "";
            chkStatus.Checked = true;
            hdQuePprSectionMappingId.Value = "0";
            frmQuestionPaperSectionMapping.Style.Add("display", "none");
            tblQuestionPaperSectionMapping.Style.Add("display", "flex");
        }
        private void BindSectionDDL()
        {
            List<EntityLayer.ListItem> lstItem = null;
            AdminDL objAdminCls = new AdminDL();
            DataSet ds = objAdminCls.GetAllSectionMaster();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lstItem = new List<EntityLayer.ListItem>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstItem.Add(new EntityLayer.ListItem
                    {
                        ID = Convert.ToString(ds.Tables[0].Rows[i]["SectionMasterId"]),
                        Name = Convert.ToString(ds.Tables[0].Rows[i]["SectionName"])
                    });
                }
                ddlSection.DataSource = lstItem;
                ddlSection.DataTextField = "Name";
                ddlSection.DataValueField = "ID";
                ddlSection.DataBind();
                ddlSection.SelectedIndex = 0;
            }
        }
        private void BindQuestionDDL()
        {
            List<EntityLayer.ListItem> lstItem = null;
            AdminDL objAdminCls = new AdminDL();
            DataSet ds = objAdminCls.GetAllQuestionPaperMaster();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lstItem = new List<EntityLayer.ListItem>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstItem.Add(new EntityLayer.ListItem
                    {
                        ID = Convert.ToString(ds.Tables[0].Rows[i]["QuestionPaperMasterId"]),
                        Name = Convert.ToString(ds.Tables[0].Rows[i]["sName"])
                    });
                }
                ddlQuestion.DataSource = lstItem;
                ddlQuestion.DataTextField = "Name";
                ddlQuestion.DataValueField = "ID";
                ddlQuestion.DataBind();
                ddlQuestion.SelectedIndex = 0;
            }
        }
        private void DeleteQuePprSectionMapping(int idQuePprSectionMapping)
        {
            AdminDL objAdminCls = new AdminDL();
            hdMessage.Value = "Question Paper Section Mapping Delete |";
            MessageResponse Response = objAdminCls.DeleteQuePprSectionMapping(idQuePprSectionMapping);
            if (Response.nError == 0)
            {
                GetQuePprSectionMapping();
                hdMessage.Value += "Question Paper Section Mapping delete successfully";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Successmsg()", true);
                frmQuestionPaperSectionMapping.Style.Add("display", "none");
                tblQuestionPaperSectionMapping.Style.Add("display", "block");
            }
            else if (Response.nError == -1)
            {
                hdMessage.Value += "Question Paper Section Mapping does not exists.";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Errormsg()", true);
            }
        }

    }
}