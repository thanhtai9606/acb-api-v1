using System;
using System.Collections.Generic;

namespace acb_app.Models
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
