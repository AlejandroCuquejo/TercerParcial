namespace Infraestructura.Modelos;

public class PersonaModel {

    public int id_persona { get; set; }
    public string nombre { get; set; }
    public string apellido { get; set; }
    public string nro_documento { get; set; }
    public string direccion { get; set; }
    public string email { get; set; }
    public string celular { get; set; }
    public string estado { get; set; }
    public CiudadModel ciudad { get; set; }
    
}