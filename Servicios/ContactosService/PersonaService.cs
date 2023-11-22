using Infraestructura.Datos;
using Infraestructura.Modelos;

namespace Servicios.ContactosService;

public class PersonaService
{
    PersonaDatos personaDatos;

    public PersonaService(string cadenaConexion) {
        personaDatos = new PersonaDatos(cadenaConexion);
    }

    public List<PersonaModel> obtenerTodasLasPersona()
    {
        return personaDatos.obtenerTodasLasPersonas();
    }

    public PersonaModel obtenerPersonaPorId(int id)
    {
        return personaDatos.obtenerPersonaPorId(id);
    }

    public void RegistrarPersona(PersonaModel persona)
    {
        validarDatos(persona);
        personaDatos.RegistrarPersona(persona);
    }
    
    public void modificarPersonaPorId(PersonaModel persona)
    {
        validarDatos(persona);
        personaDatos.modificarPersonaPorId(persona);
    }  
    
    public CiudadModel EliminarPersonaPorId(int id) {
        return personaDatos.EliminarPersonaPorId(id);
    }
    
    private void validarDatos(PersonaModel persona)
    {
        if(persona.nombre.Trim().Length < 2 )
        {
            throw new Exception("El campo nombre no puede ser nulo");
        }
        if(persona.apellido.Trim().Length < 2 )
        {
            throw new Exception("El campo apellido no puede ser nulo");
        } 
        if(persona.nro_documento.Trim().Length < 2 )
        {
            throw new Exception("El campo nro_documento no puede ser nulo");
        }  if(persona.direccion.Trim().Length < 2 )
        {
            throw new Exception("El campo direccion no puede ser nulo");
        }  
        if(persona.email.Trim().Length < 2 )
        {
            throw new Exception("El campo email no puede ser nulo");
        }  
        if(persona.celular.Trim().Length < 2 )
        {
            throw new Exception("El campo celular no puede ser nulo");
        }  
        if(persona.estado.Trim().Length < 1 )
        {
            throw new Exception("El campo estado no puede ser nulo");
        }  
        if(persona.estado.Trim().Length < 0 )
        {
            throw new Exception("El campo estado no puede ser negativo");
        } 
        if(persona.ciudad.id_ciudad < 1 )
        {
            throw new Exception("El campo id_ciudad no puede ser nulo");
        } 
    }
}