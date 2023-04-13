using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models.Delivery
{
    public partial class OrderNotificationStore1
    {
        public int NotificationId { get; set; }
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int OrderStatusId { get; set; }
        public string Status { get; set; }
        public int PaymentStatusId { get; set; }
        public string PaymentStatus { get; set; }
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public int StoreId { get; set; }
        public string Store { get; set; }
        public DateTime DateModified { get; set; }
    }
}
