using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos.RequestModel;

public class LoginRequest
{
    [Required(ErrorMessage = "User Name is required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}