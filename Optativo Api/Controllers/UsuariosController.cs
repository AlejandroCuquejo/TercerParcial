using Microsoft.AspNetCore.Mvc;
using Servicios.ContactosService;

namespace Optativo_Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsuariosController : Controller {
    
    private UsuariosService usuariosService;

    public UsuariosController()
    {
        usuariosService = new UsuariosService("Server=localhost;Port=5432;User Id=postgres;Password=6408;Database=TercerParcial;");
    }
    
    //Mostra todos los usuarios
    //
    [HttpGet("obtenerTodasLosUsuarios")]
    public IActionResult obtenerTodasLosUsuarios()
    {
        return Ok(usuariosService.obtenerTodasLosUsuarios());
    }
    //
    
    
    //Mostra usuario por id
    //
    [HttpGet("obtenerUsuariosPorId{id}")]
    public IActionResult obtenerUsuariosPorId(int id)
    {
        return Ok(usuariosService.obtenerUsuariosPorId(id));
    }
    //
    
    
    //Insertar usuario - Basico
    //
    [HttpPost("RegistrarUsuarios")]
    public IActionResult RegistrarUsuarios([FromBody] Models.UsuariosModels modelo)
    {
        usuariosService.RegistrarUsuarios(
            new Infraestructura.Modelos.UsuariosModel()
            {
                nombre_usuario = modelo.nombre_usuario,
                contrasena = modelo.contrasena,
                nivel = modelo.nivel,
                estado = modelo.estado,
                persona  = new Infraestructura.Modelos.PersonaModel()
                {
                    id_persona = modelo.id_persona
                }
            });
        return Ok("Los datos del usuario fueron insertados correctamente");
    } 
    //
    
    //Modificar Usuarios
    //
    [HttpPut("modificarUsuariosPorId")]
    public IActionResult modificarUsuariosPorId([FromBody] Infraestructura.Modelos.UsuariosModel modelo) {
        try {
            usuariosService.modificarUsuariosPorId(modelo);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
            throw;
        }
        return Ok("se actualizo con exito");
    }
    //
    
    
    //Eliminar Usuarios
    //
    [HttpDelete("EliminarUsuariosPorId{id}")]
    public IActionResult EliminarUsuariosPorId(int id)
    {
        return Ok(usuariosService.EliminarUsuariosPorId(id));
    }
    //
}