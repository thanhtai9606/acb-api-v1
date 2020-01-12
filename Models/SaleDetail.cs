using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace acb_app.Models
{
    public partial class SaleDetail
    {
        public int Id { get; set; }
        public int SoId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price {set;get;}

        public int? TotalAmount { get; set; }
        public DateTime WarrantyStart { get; set; }
        public DateTime WarrantyEnd { get; set; }

        public virtual SaleHeader So { get; set; }

        [NotMapped]
        public virtual Product Product {set;get;}
    }
}
