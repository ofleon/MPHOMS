using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using System.ComponentModel.DataAnnotations;
using MPHOMS.Web.Models.DB;
using MPHOMS.Web.Models.Commerce;

namespace MPHOMS.Web.Models
{
    public class OrderViewModel
    {
        #region Properties

        public IEnumerable<Order> Orders { get; set; }

        /// <summary>  
        /// Gets or sets to product header by order Id property.  
        /// </summary>  
        [Display(Name = "Order Header")]
        public usp_GetOrderHeaderReceipt GetOrderHeader { get; set; }

        /// <summary>  
        /// Gets or sets to product detail by order Id property.  
        /// </summary>  
        [Display(Name = "Order Detail")]
        public List<usp_GetOrderDetailByOrder> GetOrderDetail { get; set; }

        /// <summary>  
        /// Gets or sets to product shipping detail by order Id property..  
        /// </summary>  
        [Display(Name = "Order Shipping Detail")]
        public usp_GetOrderShippingDetailByOrder GetOrderShippingDetail { get; set; }

        // <summary>  
        /// Gets or sets to product total by order Id property.  
        /// </summary>  
        [Display(Name = "Order Total")]
        public usp_GetOrderTotalByOrder GetOrderTotal { get; set; }

        /// <summary>  
        /// Gets or sets to order attribute list by order Id property.  
        /// </summary>  
        [Display(Name = "Order Attributes")]
        public List<usp_GetOrderItemAttributeByOrder> GetOrderItemAttributes { get; set; }

        #endregion
    }
}
