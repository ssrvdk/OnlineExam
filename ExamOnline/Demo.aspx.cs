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
    public partial class Demo : System.Web.UI.Page
    {
        StudentDL obj = new StudentDL();
        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}
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
                    dt.Columns.Add("Question", typeof(string));
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                QuestionsTable = PopulateQuestions();
                QuestionIndex = 0;
                EnableDisableNextPrevious();
                GetCurrentQuestion(QuestionIndex, QuestionsTable);
                btnPrevious.Enabled = false;
            }
        }

        protected void OnNext(object sender, EventArgs e)
        {
            SetSelectedAnswer();
            QuestionIndex++;
            EnableDisableNextPrevious();
            GetCurrentQuestion(QuestionIndex, QuestionsTable);
            SetSelectedOption();
        }

        protected void OnPrevious(object sender, EventArgs e)
        {
            SetSelectedAnswer();
            QuestionIndex--;
            EnableDisableNextPrevious();
            GetCurrentQuestion(QuestionIndex, QuestionsTable);
            SetSelectedOption();
        }

        private void SetSelectedAnswer()
        {
            DataTable dt = AnsweredTable;
            DataRow[] drs = dt.Select("Question='" + lblQuestion.Text.Trim() + "'");
            if (drs.Length > 0)
            {
                drs[0]["Answered"] = rbtnOptions.SelectedItem != null ? int.Parse(rbtnOptions.SelectedItem.Value) : (object)DBNull.Value;
            }
            else
            {
                DataRow dr = dt.NewRow();
                dr["Question"] = lblQuestion.Text.Trim();
                dr["Answered"] = rbtnOptions.SelectedItem != null ? int.Parse(rbtnOptions.SelectedItem.Value) : (object)DBNull.Value;
                dt.Rows.Add(dr);
            }

            AnsweredTable = dt;
        }

        private void SetSelectedOption()
        {
            DataRow[] dr = AnsweredTable.Select("Question='" + lblQuestion.Text.Trim() + "'");
            if (dr.Length > 0)
            {
                if (rbtnOptions.Items.FindByValue(dr[0]["Answered"].ToString()) != null)
                {
                    rbtnOptions.Items.FindByValue(dr[0]["Answered"].ToString()).Selected = true;
                }
            }
        }

        private void EnableDisableNextPrevious()
        {
            btnPrevious.Enabled = QuestionIndex == 0 ? false : true;
            btnNext.Enabled = QuestionIndex == QuestionsTable.Rows.Count - 1 ? false : true;
        }

        private DataTable PopulateQuestions()
        {
            DataSet ds = obj.GetAllQuestionDetails();
            DataTable dt = ds.Tables[0];

            return dt;
        }

        private void GetCurrentQuestion(int index, DataTable dtQuestions)
        {
            DataRow row = dtQuestions.Rows[index];
            lblQuestion.Text = row["Question"].ToString();
            List<ListItem> options = new List<ListItem>();
            options.AddRange(new ListItem[4] {
            new ListItem(row["Option1"].ToString(), "1"),
            new ListItem(row["Option2"].ToString(), "2"),
            new ListItem(row["Option3"].ToString(), "3"),
            new ListItem(row["Option4"].ToString(), "4")
        });
            rbtnOptions.Items.Clear();
            rbtnOptions.Items.AddRange(options.ToArray());
            rbtnOptions.DataBind();
        }
    }
}