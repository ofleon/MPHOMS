using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using MPHOMS.Web.Models.Delivery;
using MPHOMS.Web.Repository;

namespace MPHOMS.Web.Hubs
{
    public class SignalOrder : Hub
    {
        private readonly DeliveryDbContext _context;
        public SignalOrder(DeliveryDbContext context)
        {
            _context = context;
        }

    }
}
