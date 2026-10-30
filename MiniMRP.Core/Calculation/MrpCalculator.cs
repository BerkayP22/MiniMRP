using MiniMRP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMRP.Core.Calculation
{
    public static class MrpCalculator
    {
        public static List<MaterialRequirement> Calculate(Order order)
        {
            List<MaterialRequirement> result = new List<MaterialRequirement>();

            foreach (var item in order.Product.RecipeItems)
            {
                var requiredQuantity = order.Quantity * item.Quantity;

                var materialRequirement = new MaterialRequirement
                {
                    MaterialId = item.Material.Id,
                    MaterialCode = item.Material.Code,
                    MaterialName = item.Material.FullName,
                    RequiredQuantity = requiredQuantity,
                    StockQuantity = item.Material.StockQuantity
                };

                result.Add(materialRequirement);
            }

            return result;

        }
    }
}
