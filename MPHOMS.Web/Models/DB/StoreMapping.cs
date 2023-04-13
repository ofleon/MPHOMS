using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models.DB
{
    public partial class StoreMapping
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public int StoreId { get; set; }
        public int EntityId { get; set; }

        public virtual Store Store { get; set; }
    }
}
