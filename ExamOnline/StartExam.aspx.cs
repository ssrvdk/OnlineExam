using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnline
{
    public partial class StartExam : System.Web.UI.Page
    {
        List<EntityLayer.QuestionMaster> lstQuestion = null;
        StudentDL objStudentDL = new StudentDL();
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                QuestionsTable = PopulateQuestions();
                QuestionIndex = 0;
                EnableDisableNextPrevious();
                GetCurrentQuestion(QuestionIndex, QuestionsTable);
                btnPrevious.Enabled = false;

                Button ddl1 = (System.Web.UI.WebControls.Button)rptQuestionNo.Items[QuestionIndex].FindControl("btnQuestionNumber");
                ddl1.BackColor = Color.Red;
                btnVisited.Text = "1";
                btnNotVisited.Text = (QuestionsTable.Rows.Count - 1).ToString();

                Session["timeout"] = DateTime.Now.AddMinutes(360);
                string mm = ((Int32)DateTime.Parse(Session["timeout"].ToString()).Subtract(DateTime.Now).TotalMinutes).ToString();
                string ss = ((Int32)DateTime.Parse(Session["timeout"].ToString()).Subtract(DateTime.Now).Seconds).ToString();
                lblTime.Text = string.Format("Time Left: 00:{0}:{1}", mm.Length > 1 ? mm : ("0" + mm), ss.Length > 1 ? ss : ("0" + ss));
            }
        }

        //private List<EntityLayer.QuestionMaster> GetQuestions()
        //{
        //    AdminDL datalayer = new AdminDL();
        //    List<EntityLayer.QuestionMaster> lstQuestion = null;
        //    DataSet ds = datalayer.GetQuestions();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        lstQuestion = new List<EntityLayer.QuestionMaster>();
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            lstQuestion.Add(new EntityLayer.QuestionMaster
        //            {
        //                QuestionMasterId = Convert.ToInt32(ds.Tables[0].Rows[i]["QuestionMasterId"]),
        //                Question = Convert.ToString(ds.Tables[0].Rows[i]["Question"]),
        //                SectionId = Convert.ToInt32(ds.Tables[0].Rows[i]["SectionId"]),
        //                Option1 = Convert.ToString(ds.Tables[0].Rows[i]["Option1"]),
        //                Option2 = Convert.ToString(ds.Tables[0].Rows[i]["Option2"]),
        //                Option3 = Convert.ToString(ds.Tables[0].Rows[i]["Option3"]),
        //                Option4 = Convert.ToString(ds.Tables[0].Rows[i]["Option4"]),
        //                bActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["bActive"])
        //            });
        //        }
        //    }
        //    return lstQuestion;
        //}

        protected void GetTime(object sender, EventArgs e)
        {
            if (0 > DateTime.Compare(DateTime.Now, DateTime.Parse(Session["timeout"].ToString())))
            {
                string mm = ((Int32)DateTime.Parse(Session["timeout"].ToString()).Subtract(DateTime.Now).TotalMinutes).ToString();
                string ss = ((Int32)DateTime.Parse(Session["timeout"].ToString()).Subtract(DateTime.Now).Seconds).ToString();
                lblTime.Text = string.Format("Time Left: 00:{0}:{1}", mm.Length > 1 ? mm : ("0" + mm), ss.Length > 1 ? ss : ("0" + ss));
            }
            else
            {
                //Timer1.Enabled = true;
                Response.Redirect("Logout.aspx");
            }
        }

        private DataTable QuestionsTable
        {
            get { return (DataTable)ViewState["Question"]; }
            set { ViewState["Question"] = value; }
        }

        private DataTable AnsweredTable
        {
            get
            {
                if (ViewState["Answer"] == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("QuestionID", typeof(int));
                    dt.Columns.Add("Question", typeof(string));
                    dt.Columns.Add("Type", typeof(string));
                    dt.Columns.Add("Answered", typeof(int));
                    ViewState["Answer"] = dt;
                    return (DataTable)ViewState["Answer"];
                }
                else
                {
                    return (DataTable)ViewState["Answer"];
                }
            }
            set { ViewState["Answer"] = value; }
        }

        private int QuestionIndex
        {
            get { return (int)ViewState["QuestionIndex"]; }
            set { ViewState["QuestionIndex"] = value; }
        }


        protected void OnNext(object sender, EventArgs e)
        {
            SetSelectedAnswer();
            QuestionIndex++;
            EnableDisableNextPrevious();
            GetCurrentQuestion(QuestionIndex, QuestionsTable);
            SetSelectedOption();
            CalculateAnswer();
        }

        protected void OnPrevious(object sender, EventArgs e)
        {
            SetSelectedAnswer();
            QuestionIndex--;
            EnableDisableNextPrevious();
            GetCurrentQuestion(QuestionIndex, QuestionsTable);
            SetSelectedOption();
            CalculateAnswer();
        }



        private void SetSelectedAnswer()
        {
            DataTable dt = AnsweredTable;
            DataRow[] drs = dt.Select("Question='" + lblQuestion.Text.Trim() + "'");
            if (drs.Length > 0)
            {
                drs[0]["Answered"] = rbtnOptions.SelectedItem != null ? int.Parse(rbtnOptions.SelectedItem.Value) : (object)DBNull.Value;
                Button ddl11 = (System.Web.UI.WebControls.Button)rptQuestionNo.Items[QuestionIndex].FindControl("btnQuestionNumber");
                if (rbtnOptions.SelectedItem != null)
                {
                    drs[0]["Type"] = (int)EntityLayer.Type.Answered;
                    ddl11.BackColor = ColorTranslator.FromHtml("#49be25");
                }
                else
                {
                    drs[0]["Type"] = (int)EntityLayer.Type.Visited;
                    ddl11.BackColor = Color.Red;
                }
            }
            else
            {
                DataRow dr = dt.NewRow();
                dr["QuestionID"] = hdnQuestionId.Value;
                dr["Question"] = lblQuestion.Text.Trim();
                Button ddl1 = (System.Web.UI.WebControls.Button)rptQuestionNo.Items[QuestionIndex].FindControl("btnQuestionNumber");
                if (rbtnOptions.SelectedItem != null)
                {
                    dr["Type"] = (int)EntityLayer.Type.Answered;
                    ddl1.BackColor = ColorTranslator.FromHtml("#49be25");
                }
                else
                {
                    dr["Type"] = (int)EntityLayer.Type.Visited;
                    ddl1.BackColor = Color.Red;
                }
                dr["Answered"] = rbtnOptions.SelectedItem != null ? int.Parse(rbtnOptions.SelectedItem.Value) : (object)DBNull.Value;
                dt.Rows.Add(dr);
            }
            AnsweredTable = dt;
        }

        private void SetSelectedOption()
        {
            DataTable dt = AnsweredTable;
            DataRow[] dr = AnsweredTable.Select("Question='" + lblQuestion.Text.Trim() + "'");
            if (dr.Length > 0)
            {
                if (rbtnOptions.Items.FindByValue(dr[0]["Answered"].ToString()) != null)
                {
                    rbtnOptions.Items.FindByValue(dr[0]["Answered"].ToString()).Selected = true;
                }
            }
            else
            {
                DataRow dr1 = dt.NewRow();
                dr1["QuestionID"] = hdnQuestionId.Value;
                dr1["Question"] = lblQuestion.Text.Trim();
                Button dd = (System.Web.UI.WebControls.Button)rptQuestionNo.Items[QuestionIndex].FindControl("btnQuestionNumber");
                if (rbtnOptions.SelectedItem != null)
                {
                    dr1["Type"] = (int)EntityLayer.Type.Answered;
                    dd.BackColor = ColorTranslator.FromHtml("#49be25");
                }
                else
                {
                    dr1["Type"] = (int)EntityLayer.Type.Visited;
                    dd.BackColor = Color.Red;
                }
                dr1["Answered"] = rbtnOptions.SelectedItem != null ? int.Parse(rbtnOptions.SelectedItem.Value) : (object)DBNull.Value;
                dt.Rows.Add(dr1);
            }
            AnsweredTable = dt;
        }

        private void EnableDisableNextPrevious()
        {
            btnPrevious.Enabled = QuestionIndex == 0 ? false : true;
            btnNext.Enabled = QuestionIndex == QuestionsTable.Rows.Count - 1 ? false : true;
        }

        private DataTable PopulateQuestions(int sectionId = 0)
        {
            DataSet ds = objStudentDL.GetAllQuestionDetails();
            DataTable dt = ds.Tables[0];  
            rptSection.DataSource = dt.AsEnumerable().GroupBy(r => new { Col1 = r["SectionId"] }).Select(g => g.OrderBy(r => r["SectionId"]).First()).CopyToDataTable();
            rptSection.DataBind();
            if (sectionId == 0)
            {
                HiddenField hdn = (System.Web.UI.WebControls.HiddenField)rptSection.Items[0].FindControl("hdnSectionId");
                rptQuestionNo.DataSource = dt.AsEnumerable().Where(x => x.Field<int>("SectionId") == Convert.ToInt32(hdn.Value)).CopyToDataTable();
                rptQuestionNo.DataBind();
                return dt.AsEnumerable().Where(x => x.Field<int>("SectionId") == Convert.ToInt32(hdn.Value)).CopyToDataTable();
            }
            else
            {
                rptQuestionNo.DataSource = dt.AsEnumerable().Where(x => x.Field<int>("SectionId") == sectionId).CopyToDataTable();
                rptQuestionNo.DataBind();
                return dt.AsEnumerable().Where(x => x.Field<int>("SectionId") == sectionId).CopyToDataTable();
            }
        }

        private void GetCurrentQuestion(int index, DataTable dtQuestions)
        {
            lblQuestNo.Text = (index + 1).ToString();
            DataRow row = dtQuestions.Rows[index];
            lblQuestion.Text = row["Question"].ToString();
            hdnQuestionId.Value = row["QuestionMasterId"].ToString();
            List<System.Web.UI.WebControls.ListItem> options = new List<System.Web.UI.WebControls.ListItem>();
            options.AddRange(new System.Web.UI.WebControls.ListItem[4] {
            new System.Web.UI.WebControls.ListItem(row["Option1"].ToString(), "1"),
            new System.Web.UI.WebControls.ListItem(row["Option2"].ToString(), "2"),
            new System.Web.UI.WebControls.ListItem(row["Option3"].ToString(), "3"),
            new System.Web.UI.WebControls.ListItem(row["Option4"].ToString(), "4")
        });
            rbtnOptions.Items.Clear();
            rbtnOptions.Items.AddRange(options.ToArray());
            rbtnOptions.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SetSelectedAnswer();
            List<EntityLayer.StudentAnswer> objStudentAnswer = new List<EntityLayer.StudentAnswer>() ;
            EntityLayer.StudentAnswer objStudent = null;
            //ViewState["Answer"]
            for (int i = 0; i < AnsweredTable.Rows.Count; i++)
            {
                objStudent = null;
                objStudent = new EntityLayer.StudentAnswer();
                objStudent.Questionid = Convert.ToInt32(AnsweredTable.Rows[i]["QuestionID"]);
                objStudent.Answer = Convert.ToString(AnsweredTable.Rows[i]["Answered"]);
                objStudentAnswer.Add(objStudent);
            }
            int Response = objStudentDL.InsertStudentAnswer(objStudentAnswer);
        }

        protected void rptQuestionNo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Button hdnQuestionId = e.Item.FindControl("btnQuestionNumber") as Button;
            if (true)
            {
                SetSelectedAnswer();
                QuestionIndex = Convert.ToInt32(hdnQuestionId.Text) - 1;
                EnableDisableNextPrevious();
                GetCurrentQuestion(QuestionIndex, QuestionsTable);
                SetSelectedOption();
                CalculateAnswer();
            }
        }

        private void CalculateAnswer()
        {
            DataTable dt = AnsweredTable;
            int answered = dt.AsEnumerable().Where(x => x.Field<string>("Type") == ((int)EntityLayer.Type.Answered).ToString()).Count();
            int Visited = dt.AsEnumerable().Where(x => x.Field<string>("Type") == ((int)EntityLayer.Type.Visited).ToString()).Count();
            btnVisited.Text = (Visited + answered).ToString();
            btnAnswered.Text = answered.ToString();
            btnNotVisited.Text = (QuestionsTable.Rows.Count - Visited - answered).ToString();
        }
        
        protected void rptSection_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField hdnQuestionId = e.Item.FindControl("hdnSectionId") as HiddenField;
            if (hdnQuestionId.Value != "")
            {
                QuestionsTable = PopulateQuestions(Convert.ToInt32(hdnQuestionId.Value));
                QuestionIndex = 0;
                EnableDisableNextPrevious();
                GetCurrentQuestion(QuestionIndex, QuestionsTable);
                btnPrevious.Enabled = false;
                Button ddl1 = (System.Web.UI.WebControls.Button)rptQuestionNo.Items[QuestionIndex].FindControl("btnQuestionNumber");
                ddl1.BackColor = Color.Red;
                btnVisited.Text = "1";
                btnNotVisited.Text = (QuestionsTable.Rows.Count - 1).ToString();
            }
        }




    }
}