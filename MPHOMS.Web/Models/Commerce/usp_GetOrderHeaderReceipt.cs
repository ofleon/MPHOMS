using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPHOMS.Web.Models.Commerce
{
    public class usp_GetOrderHeaderReceipt
    {
        [Key]
        public int Id { get; set; }
        public string CustomOrderNumber { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int CustomerId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
