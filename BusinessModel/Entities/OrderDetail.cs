using System.ComponentModel.DataAnnotations;

namespace BusinessModel.Entities;

public class OrderDetail
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    [Required]
    public decimal UnitPrice { get; set; }
    [Required]
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
    
    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
}