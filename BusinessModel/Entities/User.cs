using System.Security.Claims;
using Microsoft.AspNetCore.Identity;


namespace BusinessModel.Entities;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public virtual List<Order> Orders { get; set; }

    /*public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
    {
        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        return userIdentity;
    }*/
}