using MiniMRP.Core.Calculation;
using MiniMRP.Core.Entities;

namespace MiniMRP.Tests
{
    public class MrpCalculatorTests
    {

        [Fact]
        public void Calculate_StockNotEnough_ReturnsShortagePerMaterial()
        {
            var tabla = new Material { Id = 1, Code = "TBL", FullName = "Tabla", StockQuantity = 10 };
            var ayaklik = new Material { Id = 2, Code = "AYK", FullName = "Ayaklık", StockQuantity = 50 };
            var vida = new Material { Id = 3, Code = "VDA", FullName = "Vida", StockQuantity = 100 };

            var masa = new Product
            {
                Id = 1,
                Code = "MASA",
                Name = "Masa",
            };

            masa.RecipeItems.Add(new RecipeItem { Material = tabla, Quantity = 1 });
            masa.RecipeItems.Add(new RecipeItem { Material = ayaklik, Quantity = 4 });
            masa.RecipeItems.Add(new RecipeItem { Material = vida, Quantity = 10 });

            var order = new Order { Product = masa, Quantity = 15 };

            var results = MrpCalculator.Calculate(order);
            Assert.Equal(3, results.Count);

            var tbl = results.Single(r => r.MaterialCode == "TBL");
            var ayk = results.Single(r => r.MaterialCode == "AYK");
            var vid = results.Single(r => r.MaterialCode == "VDA");

            Assert.Equal(5, tbl.Shortage);
            Assert.Equal(10, ayk.Shortage);
            Assert.Equal(50, vid.Shortage);
        }

        [Fact]
        public void Calculate_StockEnough_ReturnsZeroShortage()
        {
            var tabla = new Material { Id = 1, Code = "TBL", FullName = "Tabla", StockQuantity = 100 };
            var ayaklik = new Material { Id = 2, Code = "AYK", FullName = "Ayaklık", StockQuantity = 100 };
            var vida = new Material { Id = 3, Code = "VDA", FullName = "Vida", StockQuantity = 1000 };
            var masa = new Product
            {
                Id = 1,
                Code = "MASA",
                Name = "Masa",
            };
           
            masa.RecipeItems.Add(new RecipeItem { Material = tabla, Quantity = 1 });
            masa.RecipeItems.Add(new RecipeItem { Material = ayaklik, Quantity = 4 });
            masa.RecipeItems.Add(new RecipeItem { Material = vida, Quantity = 10 });
           
            var order = new Order { Product = masa, Quantity = 15 };
            var results = MrpCalculator.Calculate(order);
           
            Assert.Equal(3, results.Count);

            var tbl = results.Single(r => r.MaterialCode == "TBL");
            var ayk = results.Single(r => r.MaterialCode == "AYK");
            var vid = results.Single(r => r.MaterialCode == "VDA");

            Assert.Equal(0, tbl.Shortage);
            Assert.Equal(0, ayk.Shortage);
            Assert.Equal(0, vid.Shortage);
        }
    }
}
