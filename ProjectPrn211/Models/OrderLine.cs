using System;
using System.Collections.Generic;

namespace ProjectPrn211.Models
{
    public partial class OrderLine
    {
        public int OrderLineId { get; set; }
        public int? AccountId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Product? Product { get; set; }
    }
}
