using ExamenReparaciones.CapaDatos;
using ExamenReparaciones.CapaLogica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExamenReparaciones.Presentacion
{
    public class CatalogoUsuariosModel : PageModel
    {
        [BindProperty]
        public Cls_Usuarios Usuario { get; set; } = new Cls_Usuarios();

        public List<Cls_Usuarios> Lista { get; set; } = new List<Cls_Usuarios>();
        public string Mensaje { get; set; } = "";
        public bool EsError { get; set; }

        public void OnGet(int? id)
        {
            LlenarGrid();
            if (id.HasValue)
            {
                Usuario = Lista.FirstOrDefault(u => u.UsuarioID == id.Value) ?? new Cls_Usuarios();
            }
        }

        public void OnPostAgregar()
        {
            if (Validar())
            {
                if (Usuarios_CL.Agregarusuarios(Usuario) > 0)
                {
                    Exito("Usuario agregado correctamente.");
                }
                else Error("No se pudo agregar el usuario.");
            }
            LlenarGrid();
        }

        public void OnPostActualizar()
        {
            if (Usuario.UsuarioID == 0) Error("Seleccione un usuario de la tabla para actualizar.");
            else if (Validar())
            {
                if (Usuarios_CL.Actualizarusuarios(Usuario) > 0) Exito("Usuario actualizado correctamente.");
                else Error("No se pudo actualizar el usuario.");
            }
            LlenarGrid();
        }

        public void OnPostEliminar()
        {
            if (Usuario.UsuarioID == 0) Error("Seleccione un usuario de la tabla para eliminar.");
            else if (Usuarios_CL.Eliminarusuarios(Usuario.UsuarioID) > 0) Exito("Usuario eliminado correctamente.");
            else Error("No se pudo eliminar el usuario. Verifique que no tenga equipos asociados.");
            LlenarGrid();
        }

        private void LlenarGrid()
        {
            Lista = Usuarios_CL.Listarusuarios();
        }

        private bool Validar()
        {
            if (string.IsNullOrWhiteSpace(Usuario.Nombre) || string.IsNullOrWhiteSpace(Usuario.CorreoElectronico))
            {
                Error("Nombre y Correo son obligatorios.");
                return false;
            }
            Usuario.Telefono ??= "";
            return true;
        }

        private void Exito(string m)
        {
            Mensaje = m;
            EsError = false;
            ModelState.Clear();
            Usuario = new Cls_Usuarios();
        }

        private void Error(string m)
        {
            Mensaje = m;
            EsError = true;
        }
    }
}
