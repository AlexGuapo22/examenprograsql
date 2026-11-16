using System.Data;
using ExamenReparaciones.CapaDatos;
using Microsoft.Data.SqlClient;

namespace ExamenReparaciones.CapaLogica
{
    public class Usuarios_CL
    {
        public static List<Cls_Usuarios> Listarusuarios()
        {
            var lista = new List<Cls_Usuarios>();
            using (SqlConnection Conn = DBconn.obtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("select UsuarioID, Nombre, CorreoElectronico, Telefono from Usuarios order by UsuarioID", Conn);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        lista.Add(new Cls_Usuarios(
                            rdr.GetInt32(0),
                            rdr.GetString(1),
                            rdr.GetString(2),
                            rdr.IsDBNull(3) ? "" : rdr.GetString(3)));
                    }
                }
            }
            return lista;
        }

        public static int Agregarusuarios(Cls_Usuarios usuario)
        {
            int retorno = 0;
            try
            {
                using (SqlConnection Conn = DBconn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("insert into Usuarios (Nombre, CorreoElectronico, Telefono) values (@nombre, @correo, @telefono)", Conn)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new SqlParameter("@nombre", usuario.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@correo", usuario.CorreoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@telefono", usuario.Telefono));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                retorno = 0;
            }
            return retorno;
        }

        public static int Actualizarusuarios(Cls_Usuarios usuario)
        {
            int retorno = 0;
            try
            {
                using (SqlConnection Conn = DBconn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("update Usuarios set Nombre = @nombre, CorreoElectronico = @correo, Telefono = @telefono where UsuarioID = @id", Conn)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new SqlParameter("@id", usuario.UsuarioID));
                    cmd.Parameters.Add(new SqlParameter("@nombre", usuario.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@correo", usuario.CorreoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@telefono", usuario.Telefono));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                retorno = 0;
            }
            return retorno;
        }

        public static int Eliminarusuarios(int usuarioID)
        {
            int retorno = 0;
            try
            {
                using (SqlConnection Conn = DBconn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("delete from Usuarios where UsuarioID = @id", Conn)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new SqlParameter("@id", usuarioID));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                // Ej: el usuario tiene equipos asociados (llave foranea)
                retorno = 0;
            }
            return retorno;
        }
    }
}
