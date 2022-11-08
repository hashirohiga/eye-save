using System;
using System.Collections.Generic;

namespace Eye_Save_New.Entities
{
    public partial class ProductSale
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int ProductId { get; set; }
        public DateTime SaleDate { get; set; }
        public int ProductCount { get; set; }

        public virtual Agent Agent { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
