﻿using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models
{
    public partial class DiscountAppliedToManufacturers
    {
        public int DiscountId { get; set; }
        public int ManufacturerId { get; set; }

        public virtual Discount Discount { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
