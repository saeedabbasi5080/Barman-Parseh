using Microsoft.EntityFrameworkCore;
using ProductsReviewsApi.Domain.Entities;

namespace ProductsReviewsApi.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductReview> ProductReviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(p => p.Sku).IsUnique();

            entity.HasMany(p => p.Reviews)
                  .WithOne(r => r.Product)
                  .HasForeignKey(r => r.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProductReview>(entity =>
        {
            entity.HasIndex(r => r.ProductId);
            entity.HasIndex(r => r.CreatedAtUtc);
        });
    }
}
