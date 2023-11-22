namespace Optativo_Api.Models;

public class ClienteModels
{
    public int id_cliente { get; set; }
    
    public int id_persona { get; set; }
    public DateTime fecha_ingreso { get; set; }
    public string calificacion { get; set; }
    public string estado { get; set;  }
}