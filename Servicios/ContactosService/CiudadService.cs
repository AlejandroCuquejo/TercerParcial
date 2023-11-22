using Infraestructura.Datos;
using Infraestructura.Modelos;

namespace Servicios.ContactosService;

public class CiudadService {
    
    CiudadDatos ciudadDatos;
    
    public CiudadService(string cadenaConexion) {
        ciudadDatos = new CiudadDatos(cadenaConexion);
    }

    public void RegistrarCiudad(CiudadModel ciudad) {
        validarDatos(ciudad);
        ciudadDatos.RegistrarCiudad(ciudad);
    }
    
    public List<CiudadModel> obtenerTodasLasCiudad()
    {
        return ciudadDatos.obtenerTodasLasCiudad();
    }

    
    public CiudadModel obtenerCiudadPorId(int id)
    {
        return ciudadDatos.obtenerCiudadPorId(id);
    }

    public void modificarCiudadPorID(CiudadModel ciudad)
    {
        validarDatos(ciudad);
        ciudadDatos.modificarCiudadPorID(ciudad);
    }  
    
    public CiudadModel EliminarCiudadPorId(int id) {
        return ciudadDatos.EliminarCiudadPorId(id);
    }
    
    private void validarDatos(CiudadModel ciudad)
    {
        if(ciudad.ciudad.Trim().Length < 2 )
        {
            throw new Exception("El campo ciudad no puede ser nulo");
        }   
        if(ciudad.departamento.Trim().Length < 2 )
        {
            throw new Exception("El campo departamento no puede ser nulo");
        }   

    }
}