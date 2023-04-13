using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models
{
    public partial class VNewOrderItemAttribute
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string Attributedescription { get; set; }
    }
}
