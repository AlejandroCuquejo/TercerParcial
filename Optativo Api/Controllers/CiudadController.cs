using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servicios.ContactosService;

namespace Optativo_Api.Controllers;

[ApiController]
[Route("[controller]")]

public class CiudadController : ControllerBase {
    
    private const string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=6408;Database=TercerParcial;";
    private CiudadService servicio;

    public CiudadController() {
        servicio = new CiudadService(connectionString);
    }
    
    
    //Mostra todas las ciudades
    //
    [HttpGet("obtenerTodasLasCiudad")]
    public IActionResult obtenerTodasLasCiudad()
    {
        return Ok(servicio.obtenerTodasLasCiudad());
    }
    //
    

    //Mostrar ciudad por ID
    //
    [HttpGet("obtenerCiudadPorId")]
    public IActionResult obtenerCiudadPorId([FromQuery] int id) {
        var ciudad = servicio.obtenerCiudadPorId(id);
        return Ok(ciudad);
    }
    //
    
    
    //Agregar ciudad
    //
    [HttpPost("RegistrarCiudad")]
    public IActionResult RegistrarCiudad([FromBody] Infraestructura.Modelos.CiudadModel ciudad) {
        servicio.RegistrarCiudad(ciudad);
        return Created("Se creo con exito", ciudad);
    }
    //
    
    
    //Modificar ciudad
    //
    [HttpPut("modificarCiudadPorID")]
    public IActionResult modificarCiudadPorID([FromBody] Infraestructura.Modelos.CiudadModel ciudad) {
        try {
            servicio.modificarCiudadPorID(ciudad);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
            throw;
        }
        return Ok("se actualizo con exito");
    }
    //
    
    
    //Eliminar ciudad
    //
    [HttpDelete("EliminarCiudadPorId{id}")]
    public IActionResult EliminarCiudadPorId(int id)
    {
        return Ok(servicio.EliminarCiudadPorId(id));
    }
    //
    
}