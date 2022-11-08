using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eye_Save_New.Entities
{
    public partial class Agent
    {
        private string _logo;
        
        public Agent()
        {
            AgentPriorityHistories = new HashSet<AgentPriorityHistory>();
            ProductSales = new HashSet<ProductSale>();
            Shops = new HashSet<Shop>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int AgentTypeId { get; set; }
        public string? Address { get; set; }
        public string Inn { get; set; } = null!;
        public string? Kpp { get; set; }
        public string? DirectorName { get; set; }
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string? Logo 
        {
            get => (_logo == null || _logo == String.Empty)
               ?  $"\\Resources\\picture.png"
               :  $"\\Resources{_logo}"; 
            set => _logo = value; 
        }
        public int Priority { get; set; }

        public virtual AgentType AgentType { get; set; } = null!;
        public virtual ICollection<AgentPriorityHistory> AgentPriorityHistories { get; set; }
        public virtual ICollection<ProductSale> ProductSales { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return AgentType.Title + " | " + Title;
            }
        }
        //вычисление скидки
        [NotMapped]
        public int Discount
        {
            get
            {
                decimal sum = 0;
                foreach (var item in ProductSales)
                {
                    sum += item.ProductCount * item.Product.MinCostForAgent;
                }
                if (sum <= 10000)
                {
                    return 0;
                }
                else if (sum <= 50000) return 5;
                else if (sum <= 150000) return 10;
                else if (sum <= 500000) return 20;
                else return 25;
            }
        }
        //продажи в год
        [NotMapped]
        public int SellsForYear
        {
            get
            {   
                int lastYear = 0;
                foreach (var item in ProductSales)
                {
                    if (lastYear < item.SaleDate.Year)
                    {
                        lastYear = item.SaleDate.Year;
                    }
                }
                int sum = 0;
                foreach (var item in ProductSales)
                {
                    if (lastYear == item.SaleDate.Year)
                    {
                        sum += item.ProductCount;
                    }
                }
                return sum;
            }
        }

    }
}
