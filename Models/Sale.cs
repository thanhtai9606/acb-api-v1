using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace acb_app.Models
{
    public partial class Sale
    {
        public int SoId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime WarrantyStart { get; set; }
        public DateTime WarrantyEnd { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreateBy { get; set; }

        [NotMapped]
        public virtual ICollection<Product> Products { set; get; }
        [NotMapped]
        public virtual ICollection<Customer> Customers { set; get; }

    }
}
