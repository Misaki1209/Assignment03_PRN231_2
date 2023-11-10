namespace DataAccess.Dtos.RequestModel;

public class AddProductRequest
{
    public int CategoryId { get; set; }
    public string ProductName { get; set; }
    public decimal Weight { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
}