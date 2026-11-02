namespace MiniMRP.Web.Models;

using MiniMRP.Core.Calculation;
using MiniMRP.Core.Entities;

public class OrderDetailsViewModel
{
    public Order Order { get; set; } = null!;

    public List<MaterialRequirement> Requirements { get; set; } = new();
}

