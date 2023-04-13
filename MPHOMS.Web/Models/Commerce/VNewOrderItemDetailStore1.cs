using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models
{
    public partial class VNewOrderItemDetailStore1
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string CustomOrderNumber { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public string AttributeDescription { get; set; }
        public decimal UnitPriceExclTax { get; set; }
        public int Quantity { get; set; }
        public decimal PriceExclTax { get; set; }
    }
}
