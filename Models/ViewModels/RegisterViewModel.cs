using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
	public class RegisterViewModel
	{
		[Required]
		[StringLength(100)]
		public string Name { get; set; } = string.Empty;

		[Required]
		[EmailAddress]
		[StringLength(100)]
		public string Email { get; set; } = string.Empty;

		[Required]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;

		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords do not match.")]
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}


