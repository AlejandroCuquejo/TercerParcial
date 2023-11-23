using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Servicios.ContactosService;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[Controller]
public class AuthController : Controller
{
    private const string connectionString = ("Server=localhost;Port=5432;UserId=postgres;Password=6408;Database=TercerParcial;");
    
    private  readonly IConfiguration _configuracion;
    private UsuariosService _usuariosService;
    
    public AuthController(IConfiguration configuration)
    {
        _configuracion = configuration;
        _usuariosService = new UsuariosService(connectionString);

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
        var usuario = _usuariosService.obtenerNombreUsuario(login.UserName);
        if (usuario.contrasena == login.Password)
        {
            return true;
        }
        return false;
    }

    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}