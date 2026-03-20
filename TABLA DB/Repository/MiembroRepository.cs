using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using GymApp.Models;
using GymApp.Database;

namespace GymApp.Repository
{
    public class MiembroRepository : IMiembroRepository
    {
        private readonly DatabaseConfig _dbConfig;

        public MiembroRepository(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public void Registrar(Miembro miembro)
        {
            using (var connection = new SqliteConnection(_dbConfig.ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO Miembro (nombre_completo, cedula, telefono) VALUES (@nombre, @cedula, @telefono)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", miembro.NombreCompleto);
                    command.Parameters.AddWithValue("@cedula", miembro.Cedula);
                    command.Parameters.AddWithValue("@telefono", miembro.Telefono);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Miembro> ListarTodos()
        {
            var miembros = new List<Miembro>();
            using (var connection = new SqliteConnection(_dbConfig.ConnectionString))
            {
                connection.Open();
                string query = "SELECT nombre_completo, cedula, telefono FROM Miembro";
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            miembros.Add(new Miembro
                            {
                                NombreCompleto = reader.GetString(0),
                                Cedula = reader.GetString(1),
                                Telefono = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            return miembros;
        }

        public Miembro? BuscarPorCedula(string cedula)
        {
            using (var connection = new SqliteConnection(_dbConfig.ConnectionString))
            {
                connection.Open();
                string query = "SELECT nombre_completo, cedula, telefono FROM Miembro WHERE cedula = @cedula";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cedula", cedula);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Miembro
                            {
                                NombreCompleto = reader.GetString(0),
                                Cedula = reader.GetString(1),
                                Telefono = reader.GetString(2)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void ActualizarTelefono(string cedula, string nuevoTelefono)
        {
            using (var connection = new SqliteConnection(_dbConfig.ConnectionString))
            {
                connection.Open();
                string query = "UPDATE Miembro SET telefono = @telefono WHERE cedula = @cedula";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@telefono", nuevoTelefono);
                    command.Parameters.AddWithValue("@cedula", cedula);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(string cedula)
        {
            using (var connection = new SqliteConnection(_dbConfig.ConnectionString))
            {
                connection.Open();
                string query = "DELETE FROM Miembro WHERE cedula = @cedula";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cedula", cedula);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
