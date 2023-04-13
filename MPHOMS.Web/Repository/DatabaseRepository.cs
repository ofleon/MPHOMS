using MPHOMS.Web.Models.Delivery;
using System.Collections.Generic;
using System.Linq;

namespace MPHOMS.Web.Repository
{
    public class DatabaseRepository : IOrderRepository
    {
        private readonly DeliveryDbContext _context;

        public IEnumerable<OrderNotificationStore1> OrderList => GetOrders();

        public DatabaseRepository(DeliveryDbContext context)
        {
            _context = context;
        }

        private IEnumerable<OrderNotificationStore1> GetOrders()
        {
            return _context.OrderNotificationStore1.Where(m => m.OrderStatusId == 10).ToList();
        }
    }
}
