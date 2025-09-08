using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Controllers
{
	[Authorize]
	public class SupplierController : Controller
	{
		private readonly ISupplierService _supplierService;

		public SupplierController(ISupplierService supplierService)
		{
			_supplierService = supplierService;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
		{
			if (page < 1) page = 1;
			if (pageSize < 1) pageSize = 10;

			var items = await _supplierService.GetSuppliersWithPaginationAsync(page, pageSize);
			var total = await _supplierService.GetTotalSuppliersCountAsync();

			var vm = new Models.SupplierListViewModel
			{
				Suppliers = items,
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
		public async Task<IActionResult> Create(Supplier model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			await _supplierService.CreateSupplierAsync(model);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var supplier = await _supplierService.GetSupplierByIdAsync(id);
			if (supplier == null)
			{
				return NotFound();
			}
			return View(supplier);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Supplier model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var existing = await _supplierService.GetSupplierByIdAsync(model.SupplierId);
			if (existing == null)
			{
				return NotFound();
			}

			existing.SupplierName = model.SupplierName;
			existing.Address = model.Address;
			existing.City = model.City;
			existing.Province = model.Province;
			existing.ZipCode = model.ZipCode;
			existing.Phone = model.Phone;
			existing.Email = model.Email;

			await _supplierService.UpdateSupplierAsync(existing);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			await _supplierService.DeleteSupplierAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}


