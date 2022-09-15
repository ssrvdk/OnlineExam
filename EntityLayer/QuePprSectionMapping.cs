using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class QuePprSectionMapping
    {
        public int QuePprSectionMappingId { get; set; }
        public string sName { get; set; }
        public int QuestionPaperid { get; set; }
        public string sSectionName { get; set; }
        public int Sectionid { get; set; }
        public int NumberOfQuestion { get; set; }
        public bool bActive { get; set; }
    }
}
