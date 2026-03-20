using Microsoft.Data.Sqlite;
using System;

namespace GymApp.Database
{
    public class DatabaseConfig
    {
        private const string DbFile = "gym.db";
        // Microsoft.Data.Sqlite is used to fix the runtime error 'StaticIsInitialized'
        public string ConnectionString => $"Data Source={DbFile}";

        public void Initialize()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string query = @"
                    CREATE TABLE IF NOT EXISTS Miembro (
                        nombre_completo TEXT NOT NULL,
                        cedula TEXT PRIMARY KEY,
                        telefono TEXT NOT NULL
                    );";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
