namespace ProductsReviewsApi.Application.DTOs;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<CreateReviewDto>? Reviews { get; set; }
}
