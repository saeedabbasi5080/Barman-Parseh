using ProductsReviewsApi.Application.DTOs;

namespace ProductsReviewsApi.Application.Interfaces;

public interface IProductService
{
    Task<ProductResponse> CreateProductAsync(CreateProductRequest request);
    Task<ProductResponse?> GetProductByIdAsync(Guid id);
}
