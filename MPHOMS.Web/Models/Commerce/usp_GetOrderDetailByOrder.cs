using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPHOMS.Web.Models.Commerce
{
    public class usp_GetOrderDetailByOrder
    {
        [Key]
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
