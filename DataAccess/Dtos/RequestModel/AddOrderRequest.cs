using BusinessModel.Entities;

namespace DataAccess.Dtos.RequestModel;

public class AddOrderRequest
{
    public int MemberId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RequiredDate { get; set; }
    public DateTime ShippedDate { get; set; }
    public string Freight { get; set; }
    public virtual List<AddOrderDetailRequest> OrderDetails { get; set; }
}