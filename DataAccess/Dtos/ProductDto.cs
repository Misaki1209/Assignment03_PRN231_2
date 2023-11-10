namespace DataAccess.Dtos;

public class ProductDto
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string ProductName { get; set; }
    public decimal Weight { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
}