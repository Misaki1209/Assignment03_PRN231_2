using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using DataAccess.Dtos.RequestModel;
using DataAccess.Dtos.ResponseModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace WebMvc.Controllers;

public class LoginController : Controller
{
    private readonly HttpClient client = null;

    private string apiUrl = "";

    public LoginController()
    {
        client = new HttpClient();
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);
        apiUrl = "http://localhost:5000/api/Authenticate/login";
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View("Index");
    }
    [HttpPost]
    public async Task<ActionResult> Index(string username, string password)
    {
        var jwt = await LoginAsync(username, password);
        if (!string.IsNullOrEmpty(jwt))
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddHours(1)
            };
            Response.Cookies.Append("jwt", jwt, cookieOptions);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt) as JwtSecurityToken;
            if (jsonToken != null)
            {
                var claims = jsonToken.Claims;
                //var roles = claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
                var claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
            }
            return RedirectToAction("Index", "Home");
        }
        
        else
        {
            ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
            return View("Index");
        }
    }
    
    public async Task<string> LoginAsync(string username, string password)
    {
        var loginRequest = new LoginRequest()
        {
            Username = username,
            Password = password
        };
        var jsonContent = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(apiUrl,jsonContent);
        var strData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        var jwt = JsonSerializer.Deserialize<LoginResponse>(strData, options);
        return jwt.Token;
    }
}