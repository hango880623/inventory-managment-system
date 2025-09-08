using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Controllers
{
	[Authorize]
	public class CustomerController : Controller
	{
		private readonly ICustomerService _customerService;

		public CustomerController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
		{
			if (page < 1) page = 1;
			if (pageSize < 1) pageSize = 10;

			var items = await _customerService.GetCustomersWithPaginationAsync(page, pageSize);
			var total = await _customerService.GetTotalCustomersCountAsync();

			var vm = new Models.CustomerListViewModel
			{
				Customers = items,
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
		public async Task<IActionResult> Create(Customer model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			await _customerService.CreateCustomerAsync(model);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var customer = await _customerService.GetCustomerByIdAsync(id);
			if (customer == null)
			{
				return NotFound();
			}
			return View(customer);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Customer model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var existing = await _customerService.GetCustomerByIdAsync(model.CustomerId);
			if (existing == null)
			{
				return NotFound();
			}

			// Update fields
			existing.CustomerName = model.CustomerName;
			existing.Address = model.Address;
			existing.City = model.City;
			existing.Province = model.Province;
			existing.ZipCode = model.ZipCode;
			existing.Phone = model.Phone;
			existing.Email = model.Email;

			await _customerService.UpdateCustomerAsync(existing);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			await _customerService.DeleteCustomerAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}


