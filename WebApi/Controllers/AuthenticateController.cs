using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure;
using BusinessModel.Entities;
using DataAccess.Dtos.RequestModel;
using DataAccess.Dtos.ResponseModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Common;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthenticateController(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);

            return Ok(new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            });
        }

        return Unauthorized();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var userExists = await _userManager.FindByNameAsync(request.Username);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase { Status = "Error", Message = "User already exists!" });

        User user = new()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase { Status = "Error", Message = "User creation failed! Please check user details and try again." });
        
        if (!await _roleManager.RoleExistsAsync(UserRoles.Customer))
            await _roleManager.CreateAsync(new IdentityRole<int>(UserRoles.Customer));
        
        if (await _roleManager.RoleExistsAsync(UserRoles.Customer))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.Customer);
        }

        return Ok(new ResponseBase { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest request)
    {
        var userExists = await _userManager.FindByNameAsync(request.Username);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase { Status = "Error", Message = "User already exists!" });

        User user = new()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _roleManager.CreateAsync(new IdentityRole<int>(UserRoles.Admin));
        if (!await _roleManager.RoleExistsAsync(UserRoles.Customer))
            await _roleManager.CreateAsync(new IdentityRole<int>(UserRoles.Customer));

        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
        }

        if (await _roleManager.RoleExistsAsync(UserRoles.Customer))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.Customer);
        }

        return Ok(new ResponseBase { Status = "Success", Message = "User created successfully!" });
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
}