using System.Collections.Generic;

using MPHOMS.Web.Models.Delivery;

namespace MPHOMS.Web.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<OrderNotificationStore1> OrderList { get; }
    }
}
