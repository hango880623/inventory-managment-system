namespace InventoryManagementSystem.Models
{
	public class CustomerListViewModel
	{
		public IEnumerable<Customer> Customers { get; set; } = new List<Customer>();
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public int TotalPages { get; set; }
	}
}


