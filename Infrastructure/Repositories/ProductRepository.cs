using Microsoft.EntityFrameworkCore;
using ProductsReviewsApi.Application.Interfaces;
using ProductsReviewsApi.Domain.Entities;
using ProductsReviewsApi.Infrastructure.Persistence;

namespace ProductsReviewsApi.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdWithReviewsAsync(Guid id)
    {
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.Reviews)
            .AsSplitQuery()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> IsSkuUniqueAsync(string sku)
    {
        return !await _context.Products
            .AsNoTracking()
            .AnyAsync(p => p.Sku == sku);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
