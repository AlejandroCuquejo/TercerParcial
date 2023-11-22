namespace Optativo_Api.Models;

public class CuentasModels
{
    public int id_cuentas { get; set; }
    public int id_cliente { get; set; }
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