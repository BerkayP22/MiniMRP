using MiniMRP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMRP.Core.Calculation
{
    public class MaterialRequirement
    {
        public long RequiredQuantity {  get; set; }
        public long StockQuantity { get; set; }
        public long MaterialId { get; set; }
        public string MaterialCode { get; set; } = "";
        public string MaterialName { get; set; } = "";

        public long Shortage => Math.Max(0, RequiredQuantity - StockQuantity);
    }
}
