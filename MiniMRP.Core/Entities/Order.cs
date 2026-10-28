using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMRP.Core.Entities
{
    public class Order
    {
        public long Id { get; set; }
        public string No { get; set; } = "";
        public string PositionNo { get; set; } = "";
        public long ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public long Quantity { get; set; }
    }
}
