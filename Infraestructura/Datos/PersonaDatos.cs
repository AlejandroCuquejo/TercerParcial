using Infraestructura.Modelos;
using Infraestructura.Conexiones;
using System.Data;

namespace Infraestructura.Datos;
public class PersonaDatos
{
    private ConexionDB conexion;

    public PersonaDatos(string cadenaConexion)
    {
        conexion = new ConexionDB(cadenaConexion);
    }
    
       
    public List<PersonaModel> obtenerTodasLasPersonas()
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"SELECT c.*, p.* " +
                                               $"FROM ciudad c " +
                                               $"INNER JOIN persona p ON p.id_ciudad = c.id_ciudad ", conn);
        List<PersonaModel> personas = new List<PersonaModel>();

        using var reader = comando.ExecuteReader();
        while (reader.Read())
        {
            personas.Add(new PersonaModel()
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
                    postal_code = reader.GetInt32("postal_code"),                }
            });
        }
    
        return personas;
    }

    
      public PersonaModel obtenerPersonaPorId(int id)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"SELECT c.*, p.* " +
                                                   $"FROM ciudad c " +
                                                   $"INNER JOIN persona p ON p.id_ciudad = c.id_ciudad " +
                                                   $"WHERE p.id_persona = '{id}'", conn);
            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new PersonaModel()
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
                };
            }
            return null;
        }
      
      public void RegistrarPersona(PersonaModel persona)
      {
          var conn = conexion.GetConexion();
          var comando = new Npgsql.NpgsqlCommand("INSERT INTO public.persona (nombre, apellido, nro_documento, direccion, email, celular, estado, id_ciudad) " +
                                                 "VALUES (@nombre, @apellido, @nro_documento, @direccion, @email, @celular, @estado, @id_ciudad)", conn);

          // Agregar parámetros
          comando.Parameters.AddWithValue("nombre", persona.nombre);
          comando.Parameters.AddWithValue("apellido", persona.apellido);
          comando.Parameters.AddWithValue("nro_documento", persona.nro_documento);
          comando.Parameters.AddWithValue("direccion", persona.direccion);
          comando.Parameters.AddWithValue("email",  persona.email);
          comando.Parameters.AddWithValue("celular",  persona.celular);
          comando.Parameters.AddWithValue("estado",  persona.estado);
          comando.Parameters.AddWithValue("id_ciudad", persona.ciudad.id_ciudad);

          comando.ExecuteNonQuery();
      }

      public void modificarPersonaPorId(PersonaModel persona) {
          var conn = conexion.GetConexion();
          var comando = new Npgsql.NpgsqlCommand($"UPDATE persona " +
                                                 $"SET nombre = '{persona.nombre}', " +
                                                 $"apellido = '{persona.apellido}', " +
                                                 $"nro_documento = '{persona.nro_documento}', " +
                                                 $"direccion = '{persona.direccion}', " +
                                                 $"email = '{persona.email}', " +
                                                 $"celular = '{persona.celular}', " +
                                                 $"estado = '{persona.estado}', " + 
                                                 $"id_ciudad = {persona.ciudad.id_ciudad} " +
                                                 $"WHERE id_persona = {persona.id_persona}", conn);

          comando.ExecuteNonQuery();
      }
      
      public CiudadModel EliminarPersonaPorId(int id) {
          var conn = conexion.GetConexion();
          var comando = new Npgsql.NpgsqlCommand($"DELETE FROM persona WHERE id_persona = {id}", conn);
          using var reader = comando.ExecuteReader();
          return null ;
      }

}