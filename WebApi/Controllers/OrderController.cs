using DataAccess.Dtos.RequestModel;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet("GetOrders")]
    public IActionResult GetOrders()
    {
        return Ok(_orderRepository.GetOrders());
    }

    [HttpGet("GetOrder/{id:int}")]
    public IActionResult GetOrder(int id)
    {
        return Ok(_orderRepository.GetOrderById(id));
    }

    [HttpPost("AddOrder")]
    public IActionResult AddOrder(AddOrderRequest request)
    {
        _orderRepository.AddOrder(request);
        return Ok();
    }
    
    [HttpPost("AddOrderDetails")]
    public IActionResult AddOrderDetail(List<AddOrderDetailRequest> request)
    {
        _orderRepository.AddOrderDetails(request);
        return Ok();
    }
}