using System.ComponentModel.DataAnnotations;

namespace BusinessModel.Entities;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    [Required]
    public string ProductName { get; set; }
    [Required]
    public decimal Weight { get; set; }
    [Required]
    public decimal UnitPrice { get; set; }
    [Required]
    public int UnitsInStock { get; set; }
    
    public virtual Category Category { get; set; }
    public virtual List<OrderDetail> OrderDetails { get; set; }
}