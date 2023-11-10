using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessModel.Entities;

public class Order
{
    [Key]
    public int OrderId { get; set; }
    public int MemberId { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    public DateTime RequiredDate { get; set; }
    public DateTime ShippedDate { get; set; }
    public string Freight { get; set; }
    
    public virtual User User { get; set; }
    public virtual List<OrderDetail> OrderDetails { get; set; }
}