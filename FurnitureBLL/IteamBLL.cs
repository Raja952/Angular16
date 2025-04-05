using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml;
using FurnitureDO;
namespace FurnitureBLL
{
    public class IteamBLL
    {

        public List<ItemDO> GetFurniture(string Iteam)
        {

            Iteam = Iteam.Split(' ').Count() > 1 ? Iteam.Replace(" ", "") : Iteam;
            List <ItemDO> iteamList = new List<ItemDO>();
            //string xmlFilePath = @"D:\AppData\Iteams\Sofa.xmlitem";
            //string xmlFilePath = @"D:\Angular\10072024\AngularWithMVC\AngularWithMVC\App_Data\Sofa.xmlitem";

            string Loadxml = HostingEnvironment.ApplicationPhysicalPath + "App_Data\\Iteams\\" + Iteam + ".xml";


            //string xmlFilePath = @"AppData\Iteams\Sofa.xmlitem"; // specify the path to your xmlitem file

            XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(Server.MapPath("~/App_Data/Setting.xmlitem"));
            xmlDoc.Load(Loadxml);

            XmlNodeList sofaNodes = xmlDoc.SelectNodes("/ImageList/"+ Iteam);

            foreach (XmlNode sofa in sofaNodes)
            {
                ItemDO xmlitem = new ItemDO();
                xmlitem.Id = Convert.ToInt32(sofa["id"].InnerText);
                xmlitem.Src = sofa["src"] != null ? sofa["src"].InnerText : string.Empty;
                xmlitem.Title = sofa["title"] != null ? sofa["title"].InnerText : string.Empty;
                xmlitem.Amount = sofa["Amount"] != null ? Convert.ToDecimal(sofa["Amount"].InnerText) : 0;

                // Reading additional properties
                xmlitem.Name = sofa["name"] != null ? sofa["name"].InnerText : string.Empty;
                xmlitem.Price = sofa["price"] != null ? Convert.ToDecimal(sofa["price"].InnerText) : 0;
                xmlitem.OriginalPrice = sofa["originalPrice"] != null ? Convert.ToDecimal(sofa["originalPrice"].InnerText) : 0;
                xmlitem.Discount = sofa["discount"] != null ? Convert.ToInt32(sofa["discount"].InnerText) : 0;
                xmlitem.Quantity = sofa["quantity"] != null ? Convert.ToInt32(sofa["quantity"].InnerText) : 0;
                xmlitem.MaxQuantity = sofa["maxQuantity"] != null ? Convert.ToInt32(sofa["maxQuantity"].InnerText) : 0;
                xmlitem.Description = sofa["description"] != null ? sofa["description"].InnerText : string.Empty;
                xmlitem.ImageUrl = sofa["imageUrl"] != null ? sofa["imageUrl"].InnerText : string.Empty;

                iteamList.Add(xmlitem);
            }


            return iteamList;
        }
    }
}
