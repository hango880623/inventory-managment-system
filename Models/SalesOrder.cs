using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class SalesOrder
    {
        [Key]
        public int SalesOrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string SalesOrderNo { get; set; } = string.Empty;

        [Required]
        public int CustomerId { get; set; }

        public DateTime? OrderDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Subtotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Tax { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Total { get; set; }

        // Navigation properties
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; } = new List<SalesOrderDetail>();
    }
}
