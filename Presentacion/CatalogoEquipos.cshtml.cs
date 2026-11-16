using ExamenReparaciones.CapaDatos;
using ExamenReparaciones.CapaLogica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExamenReparaciones.Presentacion
{
    public class CatalogoEquiposModel : PageModel
    {
        [BindProperty]
        public Cls_Equipos Equipo { get; set; } = new Cls_Equipos();

        public List<Cls_Equipos> Lista { get; set; } = new List<Cls_Equipos>();
        public List<Cls_Usuarios> Usuarios { get; set; } = new List<Cls_Usuarios>();
        public string Mensaje { get; set; } = "";
        public bool EsError { get; set; }

        public void OnGet(int? id)
        {
            LlenarGrid();
            if (id.HasValue)
            {
                Equipo = Lista.FirstOrDefault(e => e.EquipoID == id.Value) ?? new Cls_Equipos();
            }
        }

        public void OnPostAgregar()
        {
            if (Validar())
            {
                if (Equipos_CL.Agregarequipos(Equipo) > 0) Exito("Equipo agregado correctamente.");
                else Error("No se pudo agregar el equipo.");
            }
            LlenarGrid();
        }

        public void OnPostActualizar()
        {
            if (Equipo.EquipoID == 0) Error("Seleccione un equipo de la tabla para actualizar.");
            else if (Validar())
            {
                if (Equipos_CL.Actualizarequipos(Equipo) > 0) Exito("Equipo actualizado correctamente.");
                else Error("No se pudo actualizar el equipo.");
            }
            LlenarGrid();
        }

        public void OnPostEliminar()
        {
            if (Equipo.EquipoID == 0) Error("Seleccione un equipo de la tabla para eliminar.");
            else if (Equipos_CL.Eliminarequipos(Equipo.EquipoID) > 0) Exito("Equipo eliminado correctamente.");
            else Error("No se pudo eliminar el equipo. Verifique que no tenga reparaciones asociadas.");
            LlenarGrid();
        }

        private void LlenarGrid()
        {
            Lista = Equipos_CL.Listarequipos();
            Usuarios = Usuarios_CL.Listarusuarios();
        }

        private bool Validar()
        {
            if (string.IsNullOrWhiteSpace(Equipo.TipoEquipo) || string.IsNullOrWhiteSpace(Equipo.Modelo) || Equipo.UsuarioID == 0)
            {
                Error("TipoEquipo, Modelo y Usuario son obligatorios.");
                return false;
            }
            return true;
        }

        private void Exito(string m)
        {
            Mensaje = m;
            EsError = false;
            ModelState.Clear();
            Equipo = new Cls_Equipos();
        }

        private void Error(string m)
        {
            Mensaje = m;
            EsError = true;
        }
    }
}
