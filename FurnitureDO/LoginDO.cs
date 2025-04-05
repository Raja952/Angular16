using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDO
{
    public class LoginDO
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }



    }

    public class Student
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Department { get; set; }
        public int Salary { get; set; }
        public List<Student> ListOfStudent { get; set; } 

    }

    public class LoginDetailDO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public string OTP { get; set; }
        public string Email { get; set; }
        public bool IsLogin { get; set; }
        public bool IsOTP { get; set; }
        public bool ISMailSend { get; set; }



    }

  
    public class GoogleSignIn
    {
        public string Email { get; set; }
        public int ID { get; set; }
        public bool verified_email { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Picture { get; set; }
        public string Password { get; set; }

    }

}
