using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Controllers
{
	[Authorize]
	public class ProductController : Controller
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
		{
			if (page < 1) page = 1;
			if (pageSize < 1) pageSize = 10;

			var items = await _productService.GetProductsWithPaginationAsync(page, pageSize);
			var total = await _productService.GetTotalProductsCountAsync();

			var vm = new Models.ProductListViewModel
			{
				Products = items,
				CurrentPage = page,
				PageSize = pageSize,
				TotalCount = total,
				TotalPages = (int)Math.Ceiling(total / (double)pageSize)
			};

			return View(vm);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Product model, IFormFile? photo)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (photo != null && photo.Length > 0)
			{
				using var ms = new MemoryStream();
				await photo.CopyToAsync(ms);
				model.Photo = ms.ToArray();
			}

			await _productService.CreateProductAsync(model);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Product model, IFormFile? photo)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var existing = await _productService.GetProductByIdAsync(model.ProductId);
			if (existing == null)
			{
				return NotFound();
			}

			if (photo != null && photo.Length > 0)
			{
				using var ms = new MemoryStream();
				await photo.CopyToAsync(ms);
				existing.Photo = ms.ToArray();
			}

			// Update fields
			existing.ProductName = model.ProductName;
			existing.Description = model.Description;
			existing.PurchasePrice = model.PurchasePrice;
			existing.SalesPrice = model.SalesPrice;
			existing.InStock = model.InStock;
			existing.ValueHand = model.ValueHand;

			await _productService.UpdateProductAsync(existing);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			await _productService.DeleteProductAsync(id);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Image(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);
			if (product == null || product.Photo == null || product.Photo.Length == 0)
			{
				return NotFound();
			}

			// Default to JPEG content type; adjust if you store content type separately
			return File(product.Photo, "image/jpeg");
		}
	}
}


