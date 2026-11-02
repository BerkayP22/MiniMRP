using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMRP.Core.Entities;
using MiniMRP.Web.Data;
using MiniMRP.Web.Models;

namespace MiniMRP.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly MrpDbContext _db;

        public OrdersController(MrpDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var allOrders = await _db.Orders.Include(o => o.Product).ToListAsync();
            return View(allOrders);
        }

        public async Task<IActionResult> Details(long id)
        {
            var order = await _db.Orders.Include(o => o.Product)
                .ThenInclude(p => p.RecipeItems)
                .ThenInclude(ri => ri.Material)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            var requirements = Core.Calculation.MrpCalculator.Calculate(order);
            var viewModel= new OrderDetailsViewModel
            {
                Order = order,
                Requirements = requirements
            };
            return View(viewModel); 
        }
    }
}
