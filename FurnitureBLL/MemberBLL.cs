using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurnitureDAL;
using FurnitureDO;

namespace FurnitureBLL
{
    public class MemberBLL
    {
        public int GetMemberDetails(MemberDO objMemberDetails) 
        {
            string str = string.Empty;

            new MemberDAL().Getmemberdetails(objMemberDetails);
            //new LoginBLL().SendMail()

            return 1;
        }


        public int InsertMemberDetails(MemberDO objMemberDetails)
        {
            try
            {
                // Call DAL for database interaction
                new MemberDAL().InsertMemberDetails(objMemberDetails);
                return 1; // Return 1 for success
            }
            catch
            {
                return 0; // Return 0 for failure
            }
        }

        public List<MemberDetails> GetMemberDetails(int MemberID)
        {
            List<MemberDetails> objMemberDetails = new List<MemberDetails>();

            objMemberDetails = new MemberDAL().Getmemberdetails(MemberID);
            return objMemberDetails;
        }

    }
}
