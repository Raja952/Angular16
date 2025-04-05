//using GoogleAuthentication.Services;
using GoogleAuthentication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurnitureDAL;
using FurnitureDO;

namespace FurnitureBLL
{
    public class CommonBLL
    {
        public string GetGoogleLink()
        {
            //string clientid,
            //clientid = "259493419980-fapmgqgohg2uc7d52sdhkk9a6gqcpm7j.apps.googleusercontent.com";
           // url = "http://localhost/Furniture/Home/GoogleLoginCallback";
            return GoogleAuth.GetAuthUrl("259493419980-fapmgqgohg2uc7d52sdhkk9a6gqcpm7j.apps.googleusercontent.com", "http://localhost/Furniture/Home/GoogleLoginCallback");
        }


        public string InsertCartData(CheckOutDO objCheckoutData)
        {
            objCheckoutData.UserId = SessionContext.Current.AISession.UserId;
            objCheckoutData.SessionId = SessionContext.Current.AISession.SessionId;

            // Call to the DAL method to insert or check the item
            var result = new CommonDAL().InsertCheckOutDetails(objCheckoutData);

            return result;  // Return the message from stored procedure
        }

        public string RemoveCartData(CheckOutDO objCheckoutData)
        {
            objCheckoutData.UserId = SessionContext.Current.AISession.UserId;
            objCheckoutData.SessionId = SessionContext.Current.AISession.SessionId;

            // Call to the DAL method to remove the item
            var result = new CommonDAL().RemoveCheckOutDetails(objCheckoutData);

            return result;  // Return the message from the stored procedure
        }


        //public void InsertCartData(CheckOutDO objCheckoutData)
        //{
        //    objCheckoutData.UserId = SessionContext.Current.AISession.UserId;
        //    objCheckoutData.SessionId = SessionContext.Current.AISession.SessionId;
        //    new CommonDAL().InsertCheckOutDetails(objCheckoutData);
        //}

        public List<ItemDO> GetCartDetails(int UserId)
        {
            List<ItemDO> objMemberDetails = new List<ItemDO>();

            objMemberDetails = new CommonDAL().GetCartDetails(UserId);
            return objMemberDetails;
        }

    }
}
