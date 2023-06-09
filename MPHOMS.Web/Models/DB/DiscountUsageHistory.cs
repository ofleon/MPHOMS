﻿using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models.DB
{
    public partial class DiscountUsageHistory
    {
        public int Id { get; set; }
        public int DiscountId { get; set; }
        public int OrderId { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public virtual Discount Discount { get; set; }
        public virtual Order Order { get; set; }
    }
}
