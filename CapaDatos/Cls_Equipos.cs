namespace ExamenReparaciones.CapaDatos
{
    // Capa de datos: representa una fila de la tabla Equipos
    public class Cls_Equipos
    {
        public int EquipoID { get; set; }
        public string TipoEquipo { get; set; } = "";
        public string Modelo { get; set; } = "";
        public int UsuarioID { get; set; }

        public Cls_Equipos() { }

        public Cls_Equipos(int equipoID, string tipoEquipo, string modelo, int usuarioID)
        {
            EquipoID = equipoID;
            TipoEquipo = tipoEquipo;
            Modelo = modelo;
            UsuarioID = usuarioID;
        }
    }
}
