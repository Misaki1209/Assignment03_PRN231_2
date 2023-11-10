namespace DataAccess.Dtos;

public class OrderDetailDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
    
    public virtual ProductDto Product { get; set; }
}