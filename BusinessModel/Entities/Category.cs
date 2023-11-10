using System.ComponentModel.DataAnnotations;

namespace BusinessModel.Entities;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    public string CategoryName { get; set; }
    
    public virtual List<Product> Products { get; set; }
}