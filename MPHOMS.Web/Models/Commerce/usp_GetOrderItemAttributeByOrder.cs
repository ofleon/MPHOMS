using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPHOMS.Web.Models.Commerce
{
    public class usp_GetOrderItemAttributeByOrder
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string Attributedescription { get; set; }
    }
}
