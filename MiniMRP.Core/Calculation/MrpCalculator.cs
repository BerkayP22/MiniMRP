using MiniMRP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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

        public static List<MaterialRequirement> CalculateTotal(List<Order> orders)
        {
            var totals = new Dictionary<long, MaterialRequirement>();

            var materialRequirement = new MaterialRequirement();
            foreach (var order in orders)
            {
                foreach (var item in order.Product.RecipeItems)
                {
                    if (!totals.ContainsKey(item.Material.Id))
                    {
                        materialRequirement = new MaterialRequirement
                        {
                            MaterialId = item.Material.Id,
                            MaterialCode = item.Material.Code,
                            MaterialName = item.Material.FullName,
                            StockQuantity = item.Material.StockQuantity
                        };
                        totals[item.Material.Id] = materialRequirement;
                    }

                    materialRequirement = totals[item.Material.Id];
                    materialRequirement.RequiredQuantity += order.Quantity * item.Quantity;

                }
            }

            return totals.Values.ToList();
        }
    }
}
