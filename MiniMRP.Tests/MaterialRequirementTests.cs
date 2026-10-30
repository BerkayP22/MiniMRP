using MiniMRP.Core.Calculation;
using MiniMRP.Core.Entities;
namespace MiniMRP.Tests
{
    public class MaterialRequirementTests
    {
        [Fact]
        public void Shortage_StockNotEnough_ReturnsMissingQuantity()
        {
            var req = new MaterialRequirement { RequiredQuantity = 15, StockQuantity = 10 };

            var result = req.Shortage;

            Assert.Equal(5, result);
        }

        [Fact]
        public void Shortage_StockEnough_ReturnsZero()
        {
            var req = new MaterialRequirement { RequiredQuantity = 15, StockQuantity = 100 };

            var result = req.Shortage;

            Assert.Equal(0, result);
        }


    }
}
