using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDO
{
    public class ItemDO
    {
        public int Id { get; set; }             // Item ID
        public int UserId { get; set; }         // User ID
        public string SessionId { get; set; }   // Session ID
        public decimal Amount { get; set; }     // Amount (price)
        public string Name { get; set; }        // Item Name
        public decimal Price { get; set; }      // Price of the item
        public decimal OriginalPrice { get; set; } // Original Price before discount
        public int Discount { get; set; }       // Discount percentage
        public int Quantity { get; set; }       // Quantity available
        public int MaxQuantity { get; set; }    // Max allowed quantity
        public string Description { get; set; } // Description of the item
        public string ImageUrl { get; set; }    // Image URL
        public DateTime Date { get; set; }      // Date of checkout

        // Writable Src property with a backing field
        private string _src;
        public string Src
        {
            get => _src ?? ImageUrl; // Default to ImageUrl if _src is null
            set => _src = value;
        }

        public string Title { get; set; }       // Title of the item
    }



}
