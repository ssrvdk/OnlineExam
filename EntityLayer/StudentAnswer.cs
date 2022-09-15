using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class StudentAnswer
    {
        public int id { get; set; }
        public int QuestionPaperid { get; set; }
        public int Questionid { get; set; }
        public string Answer { get; set; }
        public bool bCorrect { get; set; }

    }
}
