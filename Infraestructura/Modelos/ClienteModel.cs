using System.Runtime.InteropServices.JavaScript;

namespace Infraestructura.Modelos;

public class ClienteModel {
    public int id_cliente { get; set; }
    public PersonaModel persona { get; set; }
    public DateTime fecha_ingreso { get; set; }
    public string calificacion { get; set; }
    public string estado { get; set; }

}