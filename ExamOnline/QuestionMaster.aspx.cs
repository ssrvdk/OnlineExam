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
    public partial class QuestionMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetQuestionMaster();
                frmQuestionMaster.Style.Add("display", "none");
            }
        }

        private void GetQuestionMaster()
        {
            AdminDL objAdminCls = new AdminDL();
            hdMessage.Value = "Question |";
            DataSet ds = objAdminCls.GetAllQuestionMaster();
            rptQuestionMaster.DataSource = ds.Tables[0];
            rptQuestionMaster.DataBind();
            resetControl();
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

        private void resetControl()
        {
            ddlSection.SelectedIndex = 0;
            txtQuestion.Text = "";
            txtOption1.Text = "";
            txtOption2.Text = "";
            txtOption3.Text = "";
            txtOption4.Text = "";
            chkStatus.Checked = true;
            hdQuestionMasterId.Value = "0";
            frmQuestionMaster.Style.Add("display", "none");
            tblQuestionMaster.Style.Add("display", "flex");
        }

        protected void rptQuestionMaster_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label lblQuestion = e.Item.FindControl("lblQuestion") as Label;
            Label lblOpt1 = e.Item.FindControl("lblOpt1") as Label;
            Label lblOpt2 = e.Item.FindControl("lblOpt2") as Label;
            Label lblOpt3 = e.Item.FindControl("lblOpt3") as Label;
            Label lblOpt4 = e.Item.FindControl("lblOpt4") as Label;
            Label lblAns = e.Item.FindControl("lblAnswer") as Label;
            //HiddenField hdnAnswerId = e.Item.FindControl("hdnAnswerId") as HiddenField;
            HiddenField hdnSectionId = e.Item.FindControl("hdnSectionId") as HiddenField;
            Label lblstatus = e.Item.FindControl("lblStatus") as Label;
            HiddenField hdn = e.Item.FindControl("hdnId") as HiddenField;
            hdQuestionMasterId.Value = hdn.Value;
            if (!string.IsNullOrEmpty(hdQuestionMasterId.Value))
            {
                if (e.CommandName == "CatEdit")
                {
                    BindSectionDDL();
                    frmQuestionMaster.Style.Add("display", "flex");
                    tblQuestionMaster.Style.Add("display", "none");
                    txtQuestion.Text = lblQuestion.Text;
                    txtOption1.Text = lblOpt1.Text;
                    txtOption2.Text = lblOpt2.Text;
                    txtOption3.Text = lblOpt3.Text;
                    txtOption4.Text = lblOpt4.Text;
                    ddlSection.SelectedValue = hdnSectionId.Value;
                    if (!string.IsNullOrEmpty(lblAns.Text))
                    {
                        string[] ans = (lblAns.Text).Split(',');
                        for (int i = 0; i < ans.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(ans[i]))
                            {
                                switch (Convert.ToInt32(ans[i]))
                                {
                                    case 1:
                                        chkOption1.Checked = true;
                                        break;
                                    case 2:
                                        chkOption2.Checked = true;
                                        break;
                                    case 3:
                                        chkOption3.Checked = true;
                                        break;
                                    case 4:
                                        chkOption4.Checked = true;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                    chkStatus.Checked = (lblstatus.Text.ToUpper() == "ACTIVE");
                }
                else if (e.CommandName == "CatDelete")
                {
                    DeleteQuestion(Convert.ToInt32(hdQuestionMasterId.Value));
                }
            }
        }

        private void DeleteQuestion(int idQuestionMaster)
        {
            AdminDL objAdminCls = new AdminDL();
            hdMessage.Value = "Question Delete |";
            MessageResponse Response = objAdminCls.DeleteQuestionMaster(idQuestionMaster);
            if (Response.nError == 0)
            {
                GetQuestionMaster();
                hdMessage.Value += "Question delete successfully";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Successmsg()", true);
                frmQuestionMaster.Style.Add("display", "none");
                tblQuestionMaster.Style.Add("display", "block");
            }
            else if (Response.nError == -1)
            {
                hdMessage.Value += "Question deos not exists.";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Errormsg()", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdminDL objAdminCls = new AdminDL();
            EntityLayer.QuestionMaster objQuestionMaster = new EntityLayer.QuestionMaster();
            if (Convert.ToInt32(hdQuestionMasterId.Value) > 0)
            {
                hdMessage.Value = "Question Update |";
                objQuestionMaster.QuestionMasterId = Convert.ToInt32(hdQuestionMasterId.Value);
            }
            else
            {
                hdMessage.Value = "Question Insert |";
                objQuestionMaster.QuestionMasterId = 0;
            }
            objQuestionMaster.Question = txtQuestion.Text.Trim();
            objQuestionMaster.Option1 = txtOption1.Text.Trim();
            objQuestionMaster.Option2 = txtOption2.Text.Trim();
            objQuestionMaster.Option3 = txtOption3.Text.Trim();
            objQuestionMaster.Option4 = txtOption4.Text.Trim();
            objQuestionMaster.SectionId = Convert.ToInt32(ddlSection.SelectedItem.Value);
            objQuestionMaster.bActive = chkStatus.Checked;
            string Answer = "";
            if (chkOption1.Checked)
            {
                Answer += "1,";
            }
            if (chkOption2.Checked)
            {
                Answer += "2,";
            }
            if (chkOption3.Checked)
            {
                Answer += "3,";
            }
            if (chkOption4.Checked)
            {
                Answer += "4";
            }
            objQuestionMaster.Answer = Answer;
            //= chkOption1.Text + "," + chkOption2.Text;
            MessageResponse Response = objAdminCls.SetQuestionMaster(objQuestionMaster);
            if (Response.nError == 0)
            {
                GetQuestionMaster();
                hdMessage.Value += "Data saved successfully";
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "Successmsg()", true);
                frmQuestionMaster.Style.Add("display", "none");
                tblQuestionMaster.Style.Add("display", "block");
            }
            else if (Response.nError == -1)
            {
                hdMessage.Value += "Data not saved. Because Question already exists.";
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
            frmQuestionMaster.Style.Add("display", "none");
            tblQuestionMaster.Style.Add("display", "flex");
            resetControl();
        }
        protected void btnQuestionMaster_Click(object sender, EventArgs e)
        {
            BindSectionDDL();
            frmQuestionMaster.Style.Add("display", "flex");
            tblQuestionMaster.Style.Add("display", "none");
        }
    }
}