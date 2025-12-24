using Microsoft.AspNetCore.Mvc;
using ProductsReviewsApi.Application.DTOs;
using ProductsReviewsApi.Application.Interfaces;

namespace ProductsReviewsApi.Presentation.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        try
        {
            var result = await _productService.CreateProductAsync(request);
            return CreatedAtAction(nameof(GetProduct), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("SKU"))
        {
            return Conflict(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var result = await _productService.GetProductByIdAsync(id);

        if (result == null)
        {
            return NotFound(new { error = "Product not found" });
        }

        return Ok(result);
    }
}
