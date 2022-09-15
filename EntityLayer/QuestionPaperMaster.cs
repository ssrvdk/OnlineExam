using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class QuestionPaperMaster
    {
        public int QuestionPaperMasterId { get; set; }
        public string sName { get; set; }
        public int NumberofQuestions { get; set; }
        public decimal Time { get; set; }
        public bool bActive { get; set; }
    }
}
