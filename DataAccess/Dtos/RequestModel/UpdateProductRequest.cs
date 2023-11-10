namespace DataAccess.Dtos.RequestModel;

public class UpdateProductRequest
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string ProductName { get; set; }
    public decimal Weight { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
}