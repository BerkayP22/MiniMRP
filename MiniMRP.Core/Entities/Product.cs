using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMRP.Core.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public string Code { get; set; } = "";

        public List<RecipeItem> RecipeItems { get; set; } = new();
    }
}
