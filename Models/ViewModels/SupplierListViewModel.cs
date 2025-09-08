namespace InventoryManagementSystem.Models
{
	public class SupplierListViewModel
	{
		public IEnumerable<Supplier> Suppliers { get; set; } = new List<Supplier>();
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public int TotalPages { get; set; }
	}
}


