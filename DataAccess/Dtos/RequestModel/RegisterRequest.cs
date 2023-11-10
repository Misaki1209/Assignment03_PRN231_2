using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos.RequestModel;

public class RegisterRequest
{
    [Required(ErrorMessage = "User Name is required")]
    public string? Username { get; set; }
    
    [Required(ErrorMessage = "Firstname is required")]
    public string? FirstName { get; set; }
    
    [Required(ErrorMessage = "Lastname is required")]
    public string? LastName { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}
