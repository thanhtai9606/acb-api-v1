using System;
using System.Collections.Generic;

namespace acb_app.Models
{
    public partial class SaleHeader
    {
        public SaleHeader()
        {
            SaleDetails = new HashSet<SaleDetail>();
        }

        public int SoId { get; set; }
        public int CustomerId { get; set; }
        public int TotalLine { get; set; }
        public int SubTotal { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
