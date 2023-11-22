using Infraestructura.Datos;
using Infraestructura.Modelos;

namespace Servicios.ContactosService;

public class CuentasService {
    
    CuentasDatos cuentasDatos;
    
    public CuentasService(string cadenaConexion) {
        cuentasDatos = new CuentasDatos(cadenaConexion);
    }
    
    public void RegistrarCuentas(CuentasModel cuentas) {
        validarDatos(cuentas);
        cuentasDatos.RegistrarCuentas(cuentas);
    }
    
    public List<CuentasModel> obtenerTodasLasCuentas()
    {
        return cuentasDatos.obtenerTodasLasCuentas();
    }
    
    public CuentasModel obtenerCuentasPorId(int id) {
        Console.WriteLine("Datos obtenidos correctamente");
        return cuentasDatos.obtenerCuentasPorId(id);
    }
    
    public void modificarCuentasPorId(CuentasModel cuentas) {
        cuentasDatos.modificarCuentasPorId(cuentas);
    } 
    
    public CuentasModel EliminarCuentaPorId(int id) {
        return cuentasDatos.EliminarCuentaPorId(id);
    }
    
    private void validarDatos(CuentasModel cuentas)
    {
        if(cuentas.nro_cuenta.Trim().Length < 2 )
        {
            throw new Exception("El campo nro_cuenta no puede ser nulo");
        }   
        if(cuentas.tipo_cuenta .Trim().Length < 2 )
        {
            throw new Exception("El campo tipo_cuenta no puede ser nulo");
        }
        if(cuentas.estado .Trim().Length < 2 )
        {
            throw new Exception("El campo estado no puede ser nulo");
        }
        if(cuentas.saldo  != null  )
        {
            throw new Exception("El campo saldo no puede ser nulo");
        }
        if(cuentas.nro_contrato .Trim().Length < 2 )
        {
            throw new Exception("El campo nro_contrato no puede ser nulo");
        }
        if(cuentas.costo_mantenimiento != null )
        {
            throw new Exception("El campo costo_mantenimiento no puede ser nulo");
        }
        if(cuentas.promedio_acreditacion .Trim().Length < 2 )
        {
            throw new Exception("El campo promedio_acreditacion no puede ser nulo");
        }
        if(cuentas.moneda != null )
        {
            throw new Exception("El campo moneda no puede ser nulo");
        }
    }
    
   
}
