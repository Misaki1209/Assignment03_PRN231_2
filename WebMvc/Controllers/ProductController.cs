using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;

namespace WebMvc.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class ProductController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}