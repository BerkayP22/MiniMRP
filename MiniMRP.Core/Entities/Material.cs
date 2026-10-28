using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMRP.Core.Entities
{
    public class Material
    {
        public long Id { get; set; }
        public string Code { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Unit { get; set; } = "";
        public long StockQuantity { get; set; }

        public List<RecipeItem> RecipeItems { get; set; } = new();

    }
}
