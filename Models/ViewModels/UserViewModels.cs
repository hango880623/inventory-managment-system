using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class UserListViewModel
    {
        public IEnumerable<User> Users { get; set; } = new List<User>();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }

    public class UserCreateViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int RoleId { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();
    }

    public class UserEditViewModel
    {
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int RoleId { get; set; }

        [Required]
        public string AccountStatus { get; set; } = string.Empty;

        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
