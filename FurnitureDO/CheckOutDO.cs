using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDO
{
    public class CheckOutDO
    {
        public int Id { get; set; }             // Item ID
        public string ImageUrl { get; set; }    // Image URL
        public string Amount { get; set; }      // Amount (price)
        public string Description { get; set; } // Description (e.g., Sofa type)
        public string SessionId { get; set; }   // Identity (session or user identifier)
        public string Date { get; set; }        // Date of checkout
        public int UserId { get; set; }         // User ID who is checking out
        public string Name { get; set; }        // Sofa Name (e.g., "Luxury Sofa")
        public decimal Price { get; set; }      // Sofa Price
        public decimal OriginalPrice { get; set; } // Original Price before discount
        public int Discount { get; set; }       // Discount percentage
        public int Quantity { get; set; }       // Quantity available
        public int MaxQuantity { get; set; }    // Max allowed quantity
    }
}
