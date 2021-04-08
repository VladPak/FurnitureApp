using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public double TotalAmount { get; set; }
        public int Qty { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }

    }
}
