using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniMRP.Core.Calculation;
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
            var viewModel = new OrderDetailsViewModel
            {
                Order = order,
                Requirements = requirements
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Products = new SelectList(await _db.Products.ToListAsync(), "Id", "Name");
            return View(new OrderCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateViewModel model)
        {
            bool exists = await _db.Orders.AnyAsync(o =>
            o.No == model.No &&
            o.PositionNo == model.PositionNo &&
            o.ProductId == model.ProductId);

            if (exists)
            {
                ModelState.AddModelError(nameof(model.ProductId), "Bu ürün bu sipariş pozisyonunda zaten kayıtlı.");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Products = new SelectList(await _db.Products.ToListAsync(), "Id", "Name");
                return View(model);
            }
            var order = new Order
            {
                No = model.No,
                PositionNo = model.PositionNo,
                ProductId = model.ProductId,
                Quantity = model.Quantity
            };
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Requirements()
        {
            var orders = await _db.Orders.Include(o => o.Product)
                .ThenInclude(p => p.RecipeItems)
                .ThenInclude(ri => ri.Material)
                .ToListAsync();

            var requirements = MrpCalculator.CalculateTotal(orders);
            return View(requirements);
        }
    }
}
