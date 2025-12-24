using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsReviewsApi.Domain.Entities;

[Table("ProductReviews")]
public class ProductReview
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ProductId { get; set; }

    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    [Required]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string Comment { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAtUtc { get; set; }

    public Product Product { get; set; } = null!;
}
