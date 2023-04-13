using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models
{
    public partial class VNewOrderListStore1
    {
        public int OrderId { get; set; }
        public int OrderStatusId { get; set; }
        public string Status { get; set; }
        public int PaymentStatusId { get; set; }
        public string PaymentStatus { get; set; }
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public int StoreId { get; set; }
        public string Store { get; set; }
    }
}
