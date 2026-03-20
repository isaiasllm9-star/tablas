using Microsoft.Extensions.DependencyInjection;
using GymApp.Database;
using GymApp.Repository;
using GymApp.Services;
using GymApp.Screens;

// Configurar Inyección de Dependencias
var serviceCollection = new ServiceCollection();

// Registrar servicios
serviceCollection.AddSingleton<DatabaseConfig>();
serviceCollection.AddScoped<IMiembroRepository, MiembroRepository>();
serviceCollection.AddScoped<IMiembroService, MiembroService>();
serviceCollection.AddScoped<MainScreen>();

var serviceProvider = serviceCollection.BuildServiceProvider();

// Inicializar la Base de Datos
var dbConfig = serviceProvider.GetRequiredService<DatabaseConfig>();
dbConfig.Initialize();

// Iniciar la UI
var screen = serviceProvider.GetRequiredService<MainScreen>();
screen.Show();
