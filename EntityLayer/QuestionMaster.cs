using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class QuestionMaster
    {
        public int QuestionMasterId { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public string Question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Answer { get; set; }
        public int AnswerId { get; set; }
        public bool bActive { get; set; }

        //public QuestionMaster()
        //{

        //}
        //public QuestionMaster(DataRow dr)
        //{
        //    if (dr.Table.Columns.Contains("QuestionMasterId") && !Convert.IsDBNull(dr["QuestionMasterId"]))
        //        this.QuestionMasterId = Convert.ToInt32(dr["QuestionMasterId"]);
        //    if (dr.Table.Columns.Contains("SectionId") && !Convert.IsDBNull(dr["SectionId"]))
        //        this.SectionId = Convert.ToInt32(dr["SectionId"]);
        //    if (dr.Table.Columns.Contains("SectionName") && !Convert.IsDBNull(dr["SectionName"]))
        //        this.SectionName = Convert.ToString(dr["SectionName"]);
        //    if (dr.Table.Columns.Contains("Question") && !Convert.IsDBNull(dr["Question"]))
        //        this.Question = Convert.ToString(dr["Question"]);
        //    if (dr.Table.Columns.Contains("Option1") && !Convert.IsDBNull(dr["Option1"]))
        //        this.Option1 = Convert.ToString(dr["Option1"]);
        //    if (dr.Table.Columns.Contains("Option2") && !Convert.IsDBNull(dr["Option2"]))
        //        this.Option2 = Convert.ToString(dr["Option2"]);
        //    if (dr.Table.Columns.Contains("Option3") && !Convert.IsDBNull(dr["Option3"]))
        //        this.Option3 = Convert.ToString(dr["Option3"]);
        //    if (dr.Table.Columns.Contains("Option4") && !Convert.IsDBNull(dr["Option4"]))
        //        this.Option4 = Convert.ToString(dr["Option4"]);
        //    if (dr.Table.Columns.Contains("Answer") && !Convert.IsDBNull(dr["Answer"]))
        //        this.Answer = Convert.ToString(dr["Answer"]);
        //    if (dr.Table.Columns.Contains("bActive") && !Convert.IsDBNull(dr["bActive"]))
        //        this.bActive = Convert.ToBoolean(dr["bActive"]);
        //}
    }
}
