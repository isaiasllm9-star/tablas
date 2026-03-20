using System.Collections.Generic;
using GymApp.Models;
using GymApp.Repository;

namespace GymApp.Services
{
    public class MiembroService : IMiembroService
    {
        private readonly IMiembroRepository _repository;

        public MiembroService(IMiembroRepository repository)
        {
            _repository = repository;
        }

        public void RegistrarMiembro(Miembro miembro)
        {
            if (_repository.BuscarPorCedula(miembro.Cedula) != null)
            {
                throw new System.Exception("Un miembro con esta cédula ya existe.");
            }
            _repository.Registrar(miembro);
        }

        public IEnumerable<Miembro> ListarMiembros() => _repository.ListarTodos();

        public Miembro? BuscarMiembro(string cedula) => _repository.BuscarPorCedula(cedula);

        public void ActualizarTelefono(string cedula, string nuevoTelefono)
        {
            if (_repository.BuscarPorCedula(cedula) == null)
            {
                throw new System.Exception("Miembro no encontrado.");
            }
            _repository.ActualizarTelefono(cedula, nuevoTelefono);
        }

        public void EliminarMiembro(string cedula)
        {
            if (_repository.BuscarPorCedula(cedula) == null)
            {
                throw new System.Exception("Miembro no encontrado.");
            }
            _repository.Eliminar(cedula);
        }
    }
}
