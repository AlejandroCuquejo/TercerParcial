using Microsoft.AspNetCore.Mvc;
using Servicios.ContactosService;


namespace Optativo_Api.Controllers;
[ApiController]
[Route("[controller]")]

public class ClienteController : Controller
{
    private ClienteService clienteServicio;
    
    public ClienteController()
    {
        clienteServicio = new ClienteService("Server=localhost;Port=5432;User Id=postgres;Password=6408;Database=TercerParcial;");
    }
    
    //Mostra porsona por ID
    //
    [HttpGet("obtenerTodosLosClientes")]
    public IActionResult obtenerTodosLosClientes()
    {
        return Ok(clienteServicio.obtenerTodosLosClientes());
    }
    
    //    //Mostra porsona por ID
    //
    [HttpGet("obtenerClientePorId{id}")]
    public IActionResult obtenerClientePorId(int id)
    {
        return Ok(clienteServicio.obtenerClientePorId(id));
    }
    //
    
    //Insertar ciudad - Basico
    //
    [HttpPost("RegistrarCliente")]
    public IActionResult RegistrarCliente([FromBody] Models.ClienteModels modelo)
    {
        clienteServicio.RegistrarCliente(
            new Infraestructura.Modelos.ClienteModel()
            {
                fecha_ingreso = modelo.fecha_ingreso,
                calificacion = modelo.calificacion,
                estado = modelo.estado,
                persona  = new Infraestructura.Modelos.PersonaModel()
                {
                    id_persona = modelo.id_persona
                }
            });
        return Ok("Los datos de persona fueron insertados correctamente");
    }
    //
    
    //Modificar cliente
    //
    [HttpPut("modificarClientePorId")]
    public IActionResult modificarClientePorId([FromBody] Infraestructura.Modelos.ClienteModel modelo) {
        try {
            clienteServicio.modificarClientePorId(modelo);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
            throw;
        }
        return Ok("se actualizo con exito");
    }
    //
    
    //Eliminar cliente
    //
    [HttpDelete("EliminarClientePorId{id}")]
    public IActionResult EliminarClientePorId(int id)
    {
        return Ok(clienteServicio.EliminarClientePorId(id));
    }
    //
}