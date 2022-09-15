using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class StudentResult
    {
        public int id { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public int QuestionPaperid { get; set; }
        public string NoofCorrectAnswers { get; set; }
        public DateTime TotalTime { get; set; }

    }
}
