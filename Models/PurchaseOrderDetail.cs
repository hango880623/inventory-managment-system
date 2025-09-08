using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class PurchaseOrderDetail
    {
        [Key]
        public int PurchaseOrderDetailsId { get; set; }

        [Required]
        public int PurchaseOrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public int? Qty { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SalesPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Total { get; set; }

        // Navigation properties
        [ForeignKey("PurchaseOrderId")]
        public virtual PurchaseOrder PurchaseOrder { get; set; } = null!;
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } = null!;
    }
}
