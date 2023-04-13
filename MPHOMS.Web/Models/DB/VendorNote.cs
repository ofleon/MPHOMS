using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models.DB
{
    public partial class VendorNote
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public int VendorId { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
