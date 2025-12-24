using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsReviewsApi.Domain.Entities;

[Table("Products")]
public class Product
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(40)]
    public string Sku { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public DateTime CreatedAtUtc { get; set; }

    public ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();
}
