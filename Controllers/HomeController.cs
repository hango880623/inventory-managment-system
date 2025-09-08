using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get dashboard statistics
            var dashboardStats = new DashboardViewModel
            {
                TotalUsers = await _context.Users.CountAsync(),
                TotalProducts = await _context.Products.CountAsync(),
                TotalCustomers = await _context.Customers.CountAsync(),
                TotalSuppliers = await _context.Suppliers.CountAsync(),
                TotalPurchaseOrders = await _context.PurchaseOrders.CountAsync(),
                TotalSalesOrders = await _context.SalesOrders.CountAsync()
            };

            return View(dashboardStats);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
