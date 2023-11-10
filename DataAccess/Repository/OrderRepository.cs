using AutoMapper;
using BusinessModel.Entities;
using DataAccess.Dtos;
using DataAccess.Dtos.RequestModel;
using DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class OrderRepository : IOrderRepository
{
    private Ass3DbContext _context;
    private IMapper _mapper;

    public OrderRepository(IMapper mapper, Ass3DbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public List<OrderDto> GetOrders()
    {
        return _mapper.Map<List<OrderDto>>(_context.Orders
            .Include(x => x.User)
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.Product).ToList());
    }

    public OrderDto? GetOrderById(int id)
    {
        return _mapper.Map<OrderDto>(_context.Orders
            .Include(x => x.User)
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.Product).FirstOrDefault(x => x.OrderId == id));
    }

    public void AddOrder(AddOrderRequest request)
    {
        try
        {
            var order = _mapper.Map<Order>(request);
            AddOrderDetails(request.OrderDetails);
            _context.Add(order);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddOrderDetails(List<AddOrderDetailRequest> requests)
    {
        try
        {
            var listOrderDetail = _mapper.Map<List<OrderDetail>>(requests);
            foreach (var orderDetail in listOrderDetail)
            {
                var existOrder = _context.OrderDetails.FirstOrDefault(x => x.OrderId == orderDetail.OrderId && x.ProductId == orderDetail.ProductId);
                if (existOrder == null)
                    _context.OrderDetails.Add(orderDetail);
                else
                {
                    existOrder.UnitPrice = orderDetail.UnitPrice;
                    existOrder.Quantity = orderDetail.Quantity;
                    existOrder.Discount = orderDetail.Discount;
                    _context.OrderDetails.Update(existOrder);
                }
            }
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}