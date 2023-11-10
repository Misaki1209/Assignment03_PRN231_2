using DataAccess.Dtos;
using DataAccess.Dtos.RequestModel;

namespace DataAccess.IRepository;

public interface IOrderRepository
{
    public List<OrderDto> GetOrders();
    public OrderDto? GetOrderById(int id);
    public void AddOrder(AddOrderRequest request);
    public void AddOrderDetails(List<AddOrderDetailRequest> requests);
}