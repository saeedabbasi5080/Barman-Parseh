namespace ProductsReviewsApi.Application.DTOs;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public int ReviewCount { get; set; }
    public double? AverageRating { get; set; }
    public List<ReviewResponse>? Reviews { get; set; }
}
