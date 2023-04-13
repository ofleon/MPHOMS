using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models
{
    public partial class VNewOrderTotal
    {
        public int Id { get; set; }
        public decimal OrderSubtotalExclTax { get; set; }
        public decimal OrderShippingExclTax { get; set; }
        public decimal OrderTax { get; set; }
        public decimal OrderDiscount { get; set; }
        public decimal OrderTotal { get; set; }
    }
}
