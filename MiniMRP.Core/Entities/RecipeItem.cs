using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMRP.Core.Entities
{
    public class RecipeItem
    {
        public long Id { get; set; }
        public Product Product { get; set; } = null!;
        public long ProductId { get; set; }
        public Material Material { get; set; } = null!;
        public long MaterialId { get; set; }
        public long Quantity { get; set; }
    }
}
