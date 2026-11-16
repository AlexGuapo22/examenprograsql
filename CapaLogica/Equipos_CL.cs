using System.Data;
using ExamenReparaciones.CapaDatos;
using Microsoft.Data.SqlClient;

namespace ExamenReparaciones.CapaLogica
{
    public class Equipos_CL
    {
        public static List<Cls_Equipos> Listarequipos()
        {
            var lista = new List<Cls_Equipos>();
            using (SqlConnection Conn = DBconn.obtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("select EquipoID, TipoEquipo, Modelo, UsuarioID from Equipos order by EquipoID", Conn);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        lista.Add(new Cls_Equipos(
                            rdr.GetInt32(0),
                            rdr.GetString(1),
                            rdr.GetString(2),
                            rdr.GetInt32(3)));
                    }
                }
            }
            return lista;
        }

        public static int Agregarequipos(Cls_Equipos equipo)
        {
            int retorno = 0;
            try
            {
                using (SqlConnection Conn = DBconn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("insert into Equipos (TipoEquipo, Modelo, UsuarioID) values (@tipo, @modelo, @usuario)", Conn)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new SqlParameter("@tipo", equipo.TipoEquipo));
                    cmd.Parameters.Add(new SqlParameter("@modelo", equipo.Modelo));
                    cmd.Parameters.Add(new SqlParameter("@usuario", equipo.UsuarioID));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                retorno = 0;
            }
            return retorno;
        }

        public static int Actualizarequipos(Cls_Equipos equipo)
        {
            int retorno = 0;
            try
            {
                using (SqlConnection Conn = DBconn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("update Equipos set TipoEquipo = @tipo, Modelo = @modelo, UsuarioID = @usuario where EquipoID = @id", Conn)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new SqlParameter("@id", equipo.EquipoID));
                    cmd.Parameters.Add(new SqlParameter("@tipo", equipo.TipoEquipo));
                    cmd.Parameters.Add(new SqlParameter("@modelo", equipo.Modelo));
                    cmd.Parameters.Add(new SqlParameter("@usuario", equipo.UsuarioID));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                retorno = 0;
            }
            return retorno;
        }

        public static int Eliminarequipos(int equipoID)
        {
            int retorno = 0;
            try
            {
                using (SqlConnection Conn = DBconn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("delete from Equipos where EquipoID = @id", Conn)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd.Parameters.Add(new SqlParameter("@id", equipoID));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                // Ej: el equipo tiene reparaciones asociadas (llave foranea)
                retorno = 0;
            }
            return retorno;
        }
    }
}
