using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[Controller]
public class AuthController : Controller
{
    private  readonly IConfiguration _configuracion;

    public AuthController(IConfiguration configuration)
    {
        _configuracion = configuration;
    }
    
    [HttpPost("login")]
    public IActionResult Post([FromBody] LoginModel login)
    {
        var userIsValid = validUser(login);

        if (!userIsValid)
        {
            return Unauthorized();
        }
        var token = GenerateJWT(login.UserName);
        return Ok(new {jwt = token });
    }

    private object GenerateJWT(string userName)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracion["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Name, "Alejandro"),
            new Claim(JwtRegisteredClaimNames. FamilyName, "Cuquejo"),
            new Claim(JwtRegisteredClaimNames.Email, "alejandro.cuquejo362@gmail.com"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        var token = new JwtSecurityToken(
            issuer: _configuracion["Jwt:Issuer"],
            audience:_configuracion["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddSeconds(320),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private bool validUser(LoginModel login)
    {
        return login.UserName == "admin" && login.Password == "12345";
    }

    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}