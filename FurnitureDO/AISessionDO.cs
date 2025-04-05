using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDO
{
   public class AISessionDO
    {
        public string SessionId { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string OTP { get; set; }
        public bool IsOTPValid { get; set; }
        public bool OTPValue { get; set; }
        public bool SessionConnected { get; set; }
        public string GoogleURL { get; set; }
        public bool IsOTPVeryfied { get; set; }
        public int LoginAction { get; set; }


    }
}
