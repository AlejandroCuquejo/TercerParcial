namespace Infraestructura.Modelos;

public class CuentasModel {
    
    public int id_cuentas { get; set; }
    public ClienteModel cliente { get; set; }
    public string nro_cuenta { get; set; }
    public DateTime fecha_alta { get; set; }
    public string tipo_cuenta { get; set; }
    public string estado { get; set; }
    public int saldo { get; set; }
    public string nro_contrato { get; set; }
    public int costo_mantenimiento { get; set; }
    public string promedio_acreditacion { get; set; }
    public string moneda { get; set; }


}