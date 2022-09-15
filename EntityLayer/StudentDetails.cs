using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
   public class StudentDetails
    {
        public int Id { get; set; }
        public string sName { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public bool bActive { get; set; }
        public string Address { get; set; }
        public DateTime LastLogin { get; set; }
        public string EmailAddress { get; set; }
        public bool bVerified { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
