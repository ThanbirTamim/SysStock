using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysStock.Utility.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public int QuantityInStock { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
