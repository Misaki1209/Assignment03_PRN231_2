using DataAccess.Dtos.RequestModel;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;

namespace WebApi.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("GetProducts")]
    public IActionResult GetProducts()
    {
        return Ok(_productRepository.GetProducts());
    }

    [HttpGet("GetProduct/{id:int}")]
    public IActionResult GetProductById(int id)
    {
        return Ok(_productRepository.GetProductById(id));
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpPost("AddProduct")]
    public IActionResult AddProduct(AddProductRequest request)
    {
        _productRepository.AddProduct(request);
        return Ok();
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpPost("UpdateProduct")]
    public IActionResult UpdateProduct(UpdateProductRequest request)
    {
        _productRepository.UpdateProduct(request);
        return Ok();
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpDelete("DeleteProduct/{id:int}")]
    public IActionResult DeleteProduct(int id)
    {
        _productRepository.DeleteProduct(id);
        return Ok();
    }
}