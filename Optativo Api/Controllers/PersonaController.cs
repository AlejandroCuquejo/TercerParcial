using Microsoft.AspNetCore.Mvc;
using Servicios.ContactosService;

namespace Optativo_Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PersonaController : Controller {
    
    private PersonaService personaServicio;

    public PersonaController()
    {
        personaServicio = new PersonaService("Server=localhost;Port=5432;User Id=postgres;Password=6408;Database=TercerParcial;");
    }
    
    //Mostra todas las porsona
    //
    [HttpGet("obtenerTodasLasPersona")]
    public IActionResult obtenerTodasLasPersona()
    {
        return Ok(personaServicio.obtenerTodasLasPersona());
    }
    //
    
    
    //Mostra porsona por documento
    //
    [HttpGet("obtenerPersonaPorId{id}")]
    public IActionResult obtenerPersonaPorId(int id)
    {
        return Ok(personaServicio.obtenerPersonaPorId(id));
    }
    //
    
    
    //Insertar persona - Basico
    //
    [HttpPost("RegistrarPersona")]
    public IActionResult RegistrarPersona([FromBody] Models.PersonaModels modelo)
    {
        personaServicio.RegistrarPersona(
            new Infraestructura.Modelos.PersonaModel
            {
                nombre = modelo.nombre,
                apellido = modelo.apellido,
                nro_documento = modelo.nro_documento,
                direccion = modelo.direccion,
                email = modelo.email,
                celular = modelo.celular,
                estado = modelo.estado,
                ciudad  = new Infraestructura.Modelos.CiudadModel
                {
                    id_ciudad = modelo.id_ciudad
                }
            });
        return Ok("Los datos de persona fueron insertados correctamente");
    } 
    //
    
    //Modificar PERSONA
    //
    [HttpPut("modificarPersonaPorId")]
    public IActionResult modificarPersonaPorId([FromBody] Infraestructura.Modelos.PersonaModel modelo) {
        try {
            personaServicio.modificarPersonaPorId(modelo);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
            throw;
        }
        return Ok("se actualizo con exito");
    }
    //
    
    
    //Eliminar Persona
    //
    [HttpDelete("EliminarPersonaPorId{id}")]
    public IActionResult EliminarPersonaPorId(int id)
    {
        return Ok(personaServicio.EliminarPersonaPorId(id));
    }
    //
}