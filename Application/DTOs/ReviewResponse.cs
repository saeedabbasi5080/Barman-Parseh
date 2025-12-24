namespace ProductsReviewsApi.Application.DTOs;

public class ReviewResponse
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
}
