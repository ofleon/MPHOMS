using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models
{
    public partial class CustomerAddresses
    {
        public int AddressId { get; set; }
        public int CustomerId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
