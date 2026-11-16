using Microsoft.Data.SqlClient;

namespace ExamenReparaciones.CapaLogica
{
    public class DBconn
    {
        // Se carga desde appsettings.json (ConnectionStrings:conexion) en Program.cs
        public static string Cadena { get; set; } = "";

        public static SqlConnection obtenerConexion()
        {
            SqlConnection conexion = new SqlConnection(Cadena);
            conexion.Open();
            return conexion;
        }
    }
}
