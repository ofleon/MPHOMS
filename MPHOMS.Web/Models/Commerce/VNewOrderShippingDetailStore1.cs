using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models
{
    public partial class VNewOrderShippingDetailStore1
    {
        public int OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int? StateProvinceId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string ZipPostalCode { get; set; }
        public string ShippingMethod { get; set; }
        public int ShippingStatusId { get; set; }
        public string ShippingStatus { get; set; }
    }
}
