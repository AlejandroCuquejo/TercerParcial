using Infraestructura.Conexiones;
using System.Data;
namespace Infraestructura.Datos;
using Infraestructura.Modelos;

public class ClienteDatos {
    
    private ConexionDB conexion;
    
    public ClienteDatos(string cadenaConexion) {
        conexion = new ConexionDB(cadenaConexion);
    }
    
    public void RegistrarCliente(ClienteModel cliente) {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand("INSERT INTO cliente( id_persona, fecha_ingreso, calificacion,estado)" +
                                               "VALUES(@id_persona, @fecha_ingreso, @calificacion, @estado)", conn);
        comando.Parameters.AddWithValue("id_persona", cliente.persona.id_persona);
        comando.Parameters.AddWithValue("fecha_ingreso", cliente.fecha_ingreso);
        comando.Parameters.AddWithValue("calificacion", cliente.calificacion);
        comando.Parameters.AddWithValue("estado", cliente.estado);

        comando.ExecuteNonQuery();
    }
    
    public List<ClienteModel> obtenerTodosLosClientes()
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"SELECT p.*, c.*, ci.* " +
                                               $"FROM persona p " +
                                               $"INNER JOIN cliente c ON p.id_persona = c.id_persona " +
                                               $"INNER JOIN ciudad ci ON p.id_ciudad = ci.id_ciudad ", conn);
        List<ClienteModel> personas = new List<ClienteModel>();

        using var reader = comando.ExecuteReader();
        while (reader.Read())
        {
            personas.Add(new ClienteModel()
            {
                id_cliente = reader.GetInt32("id_cliente"),
                fecha_ingreso = reader.GetDateTime("fecha_ingreso"),
                calificacion = reader.GetString("calificacion"),
                estado = reader.GetString("estado"),
                persona = new PersonaModel()
                {
                    id_persona = reader.GetInt32("id_persona"),
                    nombre = reader.GetString("nombre"),
                    apellido = reader.GetString("apellido"),
                    nro_documento = reader.GetString("nro_documento"),
                    direccion = reader.GetString("direccion"),
                    email = reader.GetString("email"),
                    celular = reader.GetString("celular"),
                    estado = reader.GetString("estado"),
                    ciudad = new CiudadModel()
                    {
                        id_ciudad = reader.GetInt32("id_ciudad"),
                        ciudad = reader.GetString("ciudad"),
                        departamento = reader.GetString("departamento"),
                        postal_code = reader.GetInt32("postal_code"),
                    }                        
                },
            });
        }
    
        return personas;
    }
    
    public ClienteModel obtenerClientePorId(int id) {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"SELECT p.*, c.*, ci.* " +
                                               $"FROM persona p " +
                                               $"INNER JOIN cliente c ON p.id_persona = c.id_persona " +
                                               $"INNER JOIN ciudad ci ON p.id_ciudad = ci.id_ciudad " +
                                               $"WHERE c.id_cliente = {id}", conn);
        using var reader = comando.ExecuteReader();
  if (reader.Read())
        {
                return new ClienteModel()
                {
                    id_cliente = reader.GetInt32("id_cliente"),
                    fecha_ingreso = reader.GetDateTime("fecha_ingreso"),
                    calificacion = reader.GetString("calificacion"),
                    estado = reader.GetString("estado"),
                    persona = new PersonaModel()
                    {
                        id_persona = reader.GetInt32("id_persona"),
                        nombre = reader.GetString("nombre"),
                        apellido = reader.GetString("apellido"),
                        nro_documento = reader.GetString("nro_documento"),
                        direccion = reader.GetString("direccion"),
                        email = reader.GetString("email"),
                        celular = reader.GetString("celular"),
                        estado = reader.GetString("estado"),
                            ciudad = new CiudadModel()
                            {
                                id_ciudad = reader.GetInt32("id_ciudad"),
                                ciudad = reader.GetString("ciudad"),
                                departamento = reader.GetString("departamento"),
                                postal_code = reader.GetInt32("postal_code"),
                            }                        
                    },
                };
        }
        return null;
    }
    
    public void modificarClientePorId(ClienteModel cliente) {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"UPDATE cliente SET id_persona = '{cliente.persona.id_persona}', " +
                                               $"fecha_ingreso = '{cliente.fecha_ingreso}', " +
                                               $"calificacion = '{cliente.calificacion}', " +
                                               $"estado = '{cliente.estado}' " +
                                               $" WHERE id_cliente = {cliente.id_cliente}", conn);
        comando.ExecuteNonQuery();
    }
    
    public ClienteModel EliminarClientePorId(int id) {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"DELETE FROM cliente WHERE id_cliente = {id}", conn);
        using var reader = comando.ExecuteReader();
        return null;
    }
}