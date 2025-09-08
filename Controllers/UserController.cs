using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public UserController(IUserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var users = await _userService.GetUsersWithPaginationAsync(page, pageSize);
            var totalCount = await _userService.GetTotalUsersCountAsync();

            var viewModel = new UserListViewModel
            {
                Users = users,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await _context.Roles.ToListAsync();
            var viewModel = new UserCreateViewModel
            {
                Roles = roles
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = await _context.Roles.ToListAsync();
                return View(model);
            }

            // Check if email already exists
            var existingUser = await _userService.GetUserByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email already exists");
                model.Roles = await _context.Roles.ToListAsync();
                return View(model);
            }

            var user = new User
            {
                Email = model.Email,
                Password = model.Password,
                Name = model.Name,
                RoleId = model.RoleId,
                AccountStatus = "Active"
            };

            await _userService.CreateUserAsync(user);
            TempData["SuccessMessage"] = "User created successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles.ToListAsync();
            var viewModel = new UserEditViewModel
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                RoleId = user.RoleId,
                AccountStatus = user.AccountStatus,
                Roles = roles
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = await _context.Roles.ToListAsync();
                return View(model);
            }

            var user = await _userService.GetUserByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            // Check if email already exists for another user
            var existingUser = await _userService.GetUserByEmailAsync(model.Email);
            if (existingUser != null && existingUser.UserId != model.UserId)
            {
                ModelState.AddModelError("Email", "Email already exists");
                model.Roles = await _context.Roles.ToListAsync();
                return View(model);
            }

            user.Email = model.Email;
            user.Name = model.Name;
            user.RoleId = model.RoleId;
            user.AccountStatus = model.AccountStatus;

            if (!string.IsNullOrEmpty(model.Password))
            {
                user.Password = model.Password;
            }

            await _userService.UpdateUserAsync(user);
            TempData["SuccessMessage"] = "User updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "User deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete user!";
            }
            return RedirectToAction("Index");
        }
    }

}
