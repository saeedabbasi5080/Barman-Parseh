using ProductsReviewsApi.Domain.Entities;

namespace ProductsReviewsApi.Application.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdWithReviewsAsync(Guid id);
    Task<bool> IsSkuUniqueAsync(string sku);
    Task AddAsync(Product product);
    Task SaveChangesAsync();
}
