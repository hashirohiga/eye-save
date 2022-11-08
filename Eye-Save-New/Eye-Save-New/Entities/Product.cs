using System;
using System.Collections.Generic;

namespace Eye_Save_New.Entities
{
    public partial class Product
    {
        public Product()
        {
            ProductCostHistories = new HashSet<ProductCostHistory>();
            ProductMaterials = new HashSet<ProductMaterial>();
            ProductSales = new HashSet<ProductSale>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int? ProductTypeId { get; set; }
        public string ArticleNumber { get; set; } = null!;
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int? ProductionPersonCount { get; set; }
        public int? ProductionWorkshopNumber { get; set; }
        public decimal MinCostForAgent { get; set; }

        public virtual ProductType? ProductType { get; set; }
        public virtual ICollection<ProductCostHistory> ProductCostHistories { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
        public virtual ICollection<ProductSale> ProductSales { get; set; }
    }
}
