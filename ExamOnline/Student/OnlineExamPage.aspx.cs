using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamOnline.Student
{
    public partial class OnlineExamPage : System.Web.UI.Page
    {
        List<EntityLayer.QuestionMaster> lstQuestion = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                lstQuestion = GetQuestions();
            }
        }

        private List<EntityLayer.QuestionMaster> GetQuestions()
        {
            AdminDL datalayer = new AdminDL();
            List<EntityLayer.QuestionMaster> lstQuestion = null;
            DataSet ds = datalayer.GetQuestions();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rptExamPage.DataSource = ds.Tables[0];
                rptExamPage.DataBind();
                lstQuestion = new List<EntityLayer.QuestionMaster>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstQuestion.Add(new EntityLayer.QuestionMaster
                    {
                        QuestionMasterId = Convert.ToInt32(ds.Tables[0].Rows[i]["QuestionMasterId"]),
                        Question = Convert.ToString(ds.Tables[0].Rows[i]["Question"]),
                        SectionId = Convert.ToInt32(ds.Tables[0].Rows[i]["SectionId"]),
                        Option1 = Convert.ToString(ds.Tables[0].Rows[i]["Option1"]),
                        Option2 = Convert.ToString(ds.Tables[0].Rows[i]["Option2"]),
                        Option3 = Convert.ToString(ds.Tables[0].Rows[i]["Option3"]),
                        Option4 = Convert.ToString(ds.Tables[0].Rows[i]["Option4"]),
                        bActive = Convert.ToBoolean(ds.Tables[0].Rows[i]["bActive"])
                    });
                }
            }
            return lstQuestion;
        }


    }
}