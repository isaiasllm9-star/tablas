using System.Collections.Generic;
using GymApp.Models;

namespace GymApp.Repository
{
    public interface IMiembroRepository
    {
        void Registrar(Miembro miembro);
        List<Miembro> ListarTodos();
        Miembro? BuscarPorCedula(string cedula);
        void ActualizarTelefono(string cedula, string nuevoTelefono);
        void Eliminar(string cedula);
    }
}
