using ProductsReviewsApi.Application.DTOs;
using ProductsReviewsApi.Application.Interfaces;
using ProductsReviewsApi.Domain.Entities;

namespace ProductsReviewsApi.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponse> CreateProductAsync(CreateProductRequest request)
    {
        if (!await _productRepository.IsSkuUniqueAsync(request.Sku))
        {
            throw new InvalidOperationException("SKU already exists");
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Sku = request.Sku,
            Price = request.Price,
            CreatedAtUtc = DateTime.UtcNow
        };

        if (request.Reviews != null)
        {
            foreach (var reviewDto in request.Reviews)
            {
                product.Reviews.Add(new ProductReview
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Rating = reviewDto.Rating,
                    Title = reviewDto.Title,
                    Comment = reviewDto.Comment,
                    CreatedAtUtc = DateTime.UtcNow
                });
            }
        }

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        var reviewsList = product.Reviews.OrderByDescending(r => r.CreatedAtUtc).ToList();

        double? averageRating = null;
        if (reviewsList.Count > 0)
        {
            averageRating = reviewsList.Average(r => r.Rating);
        }

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Sku = product.Sku,
            Price = product.Price,
            CreatedAtUtc = product.CreatedAtUtc,
            ReviewCount = reviewsList.Count,
            AverageRating = averageRating,
            Reviews = reviewsList.Select(r => new ReviewResponse
            {
                Id = r.Id,
                Rating = r.Rating,
                Title = r.Title,
                Comment = r.Comment,
                CreatedAtUtc = r.CreatedAtUtc
            }).ToList()
        };
    }

    public async Task<ProductResponse?> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdWithReviewsAsync(id);

        if (product == null)
        {
            return null;
        }

        var reviewsList = product.Reviews.OrderByDescending(r => r.CreatedAtUtc).ToList();

        double? averageRating = null;
        if (reviewsList.Count > 0)
        {
            averageRating = reviewsList.Average(r => r.Rating);
        }

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Sku = product.Sku,
            Price = product.Price,
            CreatedAtUtc = product.CreatedAtUtc,
            ReviewCount = reviewsList.Count,
            AverageRating = averageRating,
            Reviews = reviewsList.Select(r => new ReviewResponse
            {
                Id = r.Id,
                Rating = r.Rating,
                Title = r.Title,
                Comment = r.Comment,
                CreatedAtUtc = r.CreatedAtUtc
            }).ToList()
        };
    }
}
