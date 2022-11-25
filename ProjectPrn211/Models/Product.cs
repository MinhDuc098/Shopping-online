using System;
using System.Collections.Generic;

namespace ProjectPrn211.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public double? UnitPrice { get; set; }
        public int? UnitInStoke { get; set; }
        public string? Image { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
