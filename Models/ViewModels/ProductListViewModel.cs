namespace InventoryManagementSystem.Models
{
	public class ProductListViewModel
	{
		public IEnumerable<Product> Products { get; set; } = new List<Product>();
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public int TotalPages { get; set; }
	}
}


