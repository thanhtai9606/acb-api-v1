using System;
using System.Collections.Generic;

namespace acb_app.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int OrderPrice { get; set; }
        public int SalePrice { get; set; }
        public string Model { get; set; }
        public short Inventory { get; set; }
        public int Warranty { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
