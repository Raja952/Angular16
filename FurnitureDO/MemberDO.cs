using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDO
{
    public class MemberDO : RegisterDO
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }
    }
    public class MemberDetails
    {
        public int MemberId { get; set; }
        public string OrderName { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Productname { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public Decimal Amount { get; set; }
    }



}
