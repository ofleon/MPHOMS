﻿using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models.DB
{
    public partial class VendorAttributeValue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int VendorAttributeId { get; set; }
        public bool IsPreSelected { get; set; }
        public int DisplayOrder { get; set; }

        public virtual VendorAttribute VendorAttribute { get; set; }
    }
}
