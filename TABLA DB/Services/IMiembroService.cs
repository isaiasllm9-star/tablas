using System.Collections.Generic;
using GymApp.Models;

namespace GymApp.Services
{
    public interface IMiembroService
    {
        void RegistrarMiembro(Miembro miembro);
        IEnumerable<Miembro> ListarMiembros();
        Miembro? BuscarMiembro(string cedula);
        void ActualizarTelefono(string cedula, string nuevoTelefono);
        void EliminarMiembro(string cedula);
    }
}
