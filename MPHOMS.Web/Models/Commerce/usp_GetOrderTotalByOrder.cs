using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPHOMS.Web.Models.Commerce
{
    public class usp_GetOrderTotalByOrder
    {
        [Key]
        public int Id { get; set; }
        public decimal OrderSubtotalExclTax { get; set; }
        public decimal OrderShippingExclTax { get; set; }
        public decimal OrderTax { get; set; }
        public decimal OrderDiscount { get; set; }
        public decimal OrderTotal { get; set; }
    }
}
