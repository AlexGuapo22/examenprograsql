namespace ExamenReparaciones.CapaDatos
{
    // Capa de datos: representa una fila de la tabla Usuarios
    public class Cls_Usuarios
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; } = "";
        public string CorreoElectronico { get; set; } = "";
        public string Telefono { get; set; } = "";

        public Cls_Usuarios() { }

        public Cls_Usuarios(int usuarioID, string nombre, string correoElectronico, string telefono)
        {
            UsuarioID = usuarioID;
            Nombre = nombre;
            CorreoElectronico = correoElectronico;
            Telefono = telefono;
        }
    }
}
