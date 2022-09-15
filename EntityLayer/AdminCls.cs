using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class AdminCls
    {
        public int idAdmin { get; set; }
        public string sName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool bActive { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime LastLogin { get; set; }
        public string EmailAddress { get; set; }
        public string VerificationCode { get; set; }
        public string isEmailVerified { get; set; }
    }
}
