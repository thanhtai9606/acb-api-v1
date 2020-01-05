using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace acb_app.Models
{
    public partial class SaleHeader
    {
        public int SoId { get; set; }
        public int CustomerId { get; set; }      
        public DateTime ModifiedDate { get; set; }
        public string CreateBy { get; set; }

        public int TotalLine {set;get;}
        [NotMapped]
        public virtual Customer Customer { set; get; }

    }
}
