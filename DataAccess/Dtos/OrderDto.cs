namespace DataAccess.Dtos;

public class OrderDto
{
    public int OrderId { get; set; }
    public int MemberId { get; set; }
    public string MemberFirstname { get; set; }
    public string MemberLastname { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RequiredDate { get; set; }
    public DateTime ShippedDate { get; set; }
    public string Freight { get; set; }
    
    public virtual List<OrderDetailDto> OrderDetails { get; set; }
}