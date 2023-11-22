using Microsoft.AspNetCore.Mvc;
using Servicios.ContactosService;

namespace Optativo_Api.Controllers;
[ApiController]
[Route("[controller]")]

public class CuentasController : Controller
{
    private CuentasService cuentasService;
    
    public CuentasController()
    {
        cuentasService = new CuentasService("Server=localhost;Port=5432;User Id=postgres;Password=6408;Database=TercerParcial;");
    }
    
    //Mostra todos las cuentas
    //
    [HttpGet("obtenerTodasLasCuentas")]
    public IActionResult obtenerTodasLasCuentas()
    {
        return Ok(cuentasService.obtenerTodasLasCuentas());
    }
    //
    
    
    //Mostra cuentas por ID
    //
    [HttpGet("obtenerCuentasPorId{id}")]
    public IActionResult obtenerCuentasPorId(int id)
    {
        return Ok(cuentasService.obtenerCuentasPorId(id));
    }
    //
    
    
    //Registrar Cuentas
    //
    [HttpPost("RegistrarCuentas")]
    public IActionResult RegistrarCuentas([FromBody] Models.CuentasModels modelo)
    {
        cuentasService.RegistrarCuentas(
            new Infraestructura.Modelos.CuentasModel()
            {
                nro_cuenta = modelo.nro_cuenta,
                fecha_alta = modelo.fecha_alta,
                tipo_cuenta = modelo.tipo_cuenta,
                estado = modelo.estado,
                saldo = modelo.saldo,
                nro_contrato = modelo.nro_contrato,
                costo_mantenimiento = modelo.costo_mantenimiento,
                promedio_acreditacion = modelo.promedio_acreditacion,
                moneda = modelo.moneda,
                cliente  = new Infraestructura.Modelos.ClienteModel()
                {
                    id_cliente = modelo.id_cliente
                }
            });
        return Ok("Los datos de persona fueron insertados correctamente");
    }
    //
    
    
    //modificar cuentas
    //
    [HttpPut("modificarCuentasPorId")]
    public IActionResult modificarCuentasPorId([FromBody]  Infraestructura.Modelos.CuentasModel modelo) {
        try {
            cuentasService.modificarCuentasPorId(modelo);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
            throw;
        }
        return Ok("se actualizo con exito");
    }
    //
    
    
    //Eliminar cuentas
    //
    [HttpDelete("EliminarCuentaPorId{id}")]
    public IActionResult EliminarCuentaPorId(int id)
    {
        return Ok(cuentasService.EliminarCuentaPorId(id));
    }
    //


}