using System;
using System.Collections.Generic;

namespace ProjectPrn211.Models
{
    public partial class Account
    {
        public Account()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public int AccountId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
