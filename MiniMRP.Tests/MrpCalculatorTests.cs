using MiniMRP.Core.Calculation;
using MiniMRP.Core.Entities;

namespace MiniMRP.Tests
{
    public class MrpCalculatorTests
    {

        [Fact]
        public void Calculate_StockNotEnough_ReturnsShortagePerMaterial()
        {
            var masa = CreateMasa(tablaStock: 10, ayaklikStock: 50, vidaStock: 100);

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
            var masa = CreateMasa(tablaStock: 100, ayaklikStock: 100, vidaStock: 1000);

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

        [Fact]
        public void CalculateTotal_MaterialRequirementsFromMultipleOrders_ReturnsTotalShortagePerMaterial()
        {
            var masa = CreateMasa(tablaStock: 10, ayaklikStock: 50, vidaStock: 100);

            var order1 = new Order { Product = masa, Quantity = 15 };
            var order2 = new Order { Product = masa, Quantity = 5 };

            var orders = new List<Order> { order1, order2 };

            var results = MrpCalculator.CalculateTotal(orders);

            Assert.Equal(3, results.Count);

            var tbl = results.Single(r => r.MaterialCode == "TBL");
            var ayk = results.Single(r => r.MaterialCode == "AYK");
            var vid = results.Single(r => r.MaterialCode == "VDA");

            Assert.Equal(20, tbl.RequiredQuantity);
            Assert.Equal(10, tbl.Shortage);

            Assert.Equal(80, ayk.RequiredQuantity);
            Assert.Equal(30, ayk.Shortage);

            Assert.Equal(200, vid.RequiredQuantity);
            Assert.Equal(100, vid.Shortage);
        }

        [Fact]
        public void CalculateTotal_EachOrderFitsButTotalDoesNot_ReturnsShortage()
        {
            var masa = CreateMasa(tablaStock: 10, ayaklikStock: 50, vidaStock: 100);

            var order1 = new Order { Product = masa, Quantity = 8 };
            var order2 = new Order { Product = masa, Quantity = 5 };

            var orders = new List<Order> { order1, order2 };

            var results = MrpCalculator.CalculateTotal(orders);

            Assert.Equal(3, results.Count);

            var tbl = results.Single(r => r.MaterialCode == "TBL");
            var ayk = results.Single(r => r.MaterialCode == "AYK");
            var vid = results.Single(r => r.MaterialCode == "VDA");

            Assert.Equal(13, tbl.RequiredQuantity);
            Assert.Equal(3, tbl.Shortage);

            Assert.Equal(52, ayk.RequiredQuantity);
            Assert.Equal(2, ayk.Shortage);

            Assert.Equal(130, vid.RequiredQuantity);
            Assert.Equal(30, vid.Shortage);
        }

        private static Product CreateMasa(long tablaStock, long ayaklikStock, long vidaStock)
        {
            var tabla = new Material { Id = 1, Code = "TBL", FullName = "Tabla", StockQuantity = tablaStock };
            var ayaklik = new Material { Id = 2, Code = "AYK", FullName = "Ayaklık", StockQuantity = ayaklikStock };
            var vida = new Material { Id = 3, Code = "VDA", FullName = "Vida", StockQuantity = vidaStock };

            var masa = new Product
            {
                Id = 1,
                Code = "MASA",
                Name = "Masa"
            };

            masa.RecipeItems.Add(new RecipeItem { Material = tabla, Quantity = 1 });
            masa.RecipeItems.Add(new RecipeItem { Material = ayaklik, Quantity = 4 });
            masa.RecipeItems.Add(new RecipeItem { Material = vida, Quantity = 10 });

            return masa;
        }
    }
}
