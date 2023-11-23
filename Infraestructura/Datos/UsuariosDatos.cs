using Infraestructura.Modelos;
using Infraestructura.Conexiones;
using System.Data;

namespace Infraestructura.Datos;

public class UsuariosDatos
{
    private ConexionDB conexion;

    public UsuariosDatos(string cadenaConexion)
    {
        conexion = new ConexionDB(cadenaConexion);
    }
    
    public List<UsuariosModel> obtenerTodasLosUsuarios()
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"SELECT p.*, u.* " +
                                               $"FROM persona p " +
                                               $"INNER JOIN usuarios u ON p.id_persona = u.id_persona ", conn);
        List<UsuariosModel> usuarios = new List<UsuariosModel>();

        using var reader = comando.ExecuteReader();
        while (reader.Read())
        {
            usuarios.Add(new UsuariosModel()
            {
                id_usuarios = reader.GetInt32("id_usuarios"),
                nombre_usuario = reader.GetString("nombre_usuario"),
                contrasena = reader.GetString("contrasena"),
                nivel = reader.GetString("nivel"),
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
                }
            });
        }
    
        return usuarios;
    }

    
      public UsuariosModel obtenerUsuariosPorId(int id)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"SELECT p.*, u.* " +
                                                   $"FROM persona p " +
                                                   $"INNER JOIN usuarios u ON p.id_persona = u.id_persona " +
                                                   $"WHERE u.id_usuarios = {id}", conn);
            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new UsuariosModel()
                {
                    id_usuarios = reader.GetInt32("id_usuarios"),
                    nombre_usuario = reader.GetString("nombre_usuario"),
                    contrasena = reader.GetString("contrasena"),
                    nivel = reader.GetString("nivel"),
                    estado = reader.GetString("estado"),
                    persona = new PersonaModel()
                    {
                        id_persona = reader.GetInt32("id_persona"),
                        nombre = reader.GetString("nombre"),
                        apellido = reader.GetString("apellido"),
                        nro_documento = reader.GetString("nro_documento"),
                        direccion = reader.GetString("direccion"),    
                    }
                };
            }
            return null;
        }
      
      public void RegistrarUsuarios(UsuariosModel usuarios)
      {
          var conn = conexion.GetConexion();
          var comando = new Npgsql.NpgsqlCommand("INSERT INTO public.usuarios (nombre_usuario, contrasena, nivel, estado, id_persona) " +
                                                 "VALUES (@nombre_usuario, @contrasena, @nivel, @estado, @id_persona)", conn);
          comando.Parameters.AddWithValue("nombre_usuario", usuarios.nombre_usuario);
          comando.Parameters.AddWithValue("contrasena", usuarios.contrasena);
          comando.Parameters.AddWithValue("nivel", usuarios.nivel);
          comando.Parameters.AddWithValue("estado", usuarios.estado);
          comando.Parameters.AddWithValue("id_persona", usuarios.persona.id_persona);

          comando.ExecuteNonQuery();
      }

      
     
      public void modificarUsuariosPorId(UsuariosModel usuarios) {
          var conn = conexion.GetConexion();
          var comando = new Npgsql.NpgsqlCommand($"UPDATE persona " +
                                                 $"SET nombre_usuario = '{usuarios.nombre_usuario}', " +
                                                 $"contrasena = '{usuarios.contrasena}', " +
                                                 $"nivel = '{usuarios.nivel}', " +
                                                 $"estado = '{usuarios.estado}', " + 
                                                 $"id_persona = {usuarios.persona.id_persona} " +
                                                 $"WHERE id_usuarios = {usuarios.id_usuarios}", conn);

          comando.ExecuteNonQuery();
      }
      
      public UsuariosModel EliminarUsuariosPorId(int id) {
          var conn = conexion.GetConexion();
          var comando = new Npgsql.NpgsqlCommand($"DELETE FROM usuarios WHERE id_usuarios = {id}", conn);
          using var reader = comando.ExecuteReader();
          return null ;
      }
      
      public UsuariosModel obtenerNombreUsuario(string username)
      {
          var conn = conexion.GetConexion();
          var comando = new Npgsql.NpgsqlCommand(
              $"SELECT u.*, p.* FROM usuarios u " +
              $"INNER JOIN persona p ON u.id_persona = p.id_persona " +
              $"WHERE u.nombre_usuario = '{username}'", conn);

          using var reader = comando.ExecuteReader();
          if (reader.Read())
          {
              return new UsuariosModel
              {
                  id_usuarios = reader.GetInt32("id_usuarios"),
                  id_persona = reader.GetInt32("id_persona"),
                  nombre_usuario = reader.GetString("nombre_usuario"),
                  nivel = reader.GetString("nivel"),
                  estado = reader.GetString("estado"),
                  contrasena = reader.GetString("contrasena"),
                  persona = new PersonaModel()
                  {
                      id_persona = reader.GetInt32("id_persona"),
                      nombre = reader.GetString("nombre"),
                      apellido = reader.GetString("apellido"),
                      nro_documento = reader.GetString("nro_documento"),
                      direccion = reader.GetString("direccion"),
                      email = reader.GetString("email")
                  }
              };
          }
          return null;
      }


}