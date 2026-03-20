using Spectre.Console;
using GymApp.Services;
using GymApp.Models;
using System.Collections.Generic;

namespace GymApp.Screens
{
    public class MainScreen
    {
        private readonly IMiembroService _service;

        public MainScreen(IMiembroService service)
        {
            _service = service;
        }

        public void Show()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("GYM MANAGER")
                        .Centered()
                        .Color(Color.DeepSkyBlue1));

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[cyan]Seleccione una opción:[/]")
                        .PageSize(10)
                        .AddChoices(new[] {
                            "Registrar Miembro",
                            "Listar Miembros",
                            "Buscar Miembro por Cédula",
                            "Actualizar Teléfono",
                            "Eliminar Miembro",
                            "Salir"
                        }));

                switch (choice)
                {
                    case "Registrar Miembro":
                        Registrar();
                        break;
                    case "Listar Miembros":
                        Listar();
                        break;
                    case "Buscar Miembro por Cédula":
                        Buscar();
                        break;
                    case "Actualizar Teléfono":
                        Actualizar();
                        break;
                    case "Eliminar Miembro":
                        Eliminar();
                        break;
                    case "Salir":
                        return;
                }
                AnsiConsole.MarkupLine("\n[grey]Presiona ENTER para continuar...[/]");
                System.Console.ReadLine();
            }
        }

        private void Registrar()
        {
            AnsiConsole.MarkupLine("[bold blue]REGISTRO DE MIEMBRO[/]");
            var nombre = AnsiConsole.Ask<string>("Nombre completo:");
            var cedula = AnsiConsole.Ask<string>("Cédula:");
            var telefono = AnsiConsole.Ask<string>("Teléfono:");

            try
            {
                _service.RegistrarMiembro(new Miembro { 
                    NombreCompleto = nombre, 
                    Cedula = cedula, 
                    Telefono = telefono 
                });
                AnsiConsole.MarkupLine("[green]Miembro registrado con éxito![/]");
            }
            catch (System.Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }

        private void Listar()
        {
            AnsiConsole.MarkupLine("[bold blue]LISTA DE MIEMBROS[/]");
            var miembros = _service.ListarMiembros();
            var table = new Table().Border(TableBorder.Rounded);
            table.AddColumn("[yellow]Nombre Completo[/]");
            table.AddColumn("[yellow]Cédula[/]");
            table.AddColumn("[yellow]Teléfono[/]");

            foreach (var m in miembros)
            {
                table.AddRow(m.NombreCompleto, m.Cedula, m.Telefono);
            }

            AnsiConsole.Write(table);
        }

        private void Buscar()
        {
            AnsiConsole.MarkupLine("[bold blue]BÚSQUEDA POR CÉDULA[/]");
            var cedula = AnsiConsole.Ask<string>("Ingresa la cédula a buscar:");
            var m = _service.BuscarMiembro(cedula);

            if (m != null)
            {
                var panel = new Panel(
                    new Markup($"[bold]Nombre:[/] {m.NombreCompleto}\n[bold]Cédula:[/] {m.Cedula}\n[bold]Teléfono:[/] {m.Telefono}")
                );
                panel.Header = new PanelHeader("[green]Resultado[/]");
                panel.Border = BoxBorder.Rounded;
                AnsiConsole.Write(panel);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Miembro no encontrado.[/]");
            }
        }

        private void Actualizar()
        {
            AnsiConsole.MarkupLine("[bold blue]ACTUALIZAR TELÉFONO[/]");
            var cedula = AnsiConsole.Ask<string>("Cédula del miembro a actualizar:");
            var m = _service.BuscarMiembro(cedula);
            
            if (m == null)
            {
                AnsiConsole.MarkupLine("[red]Miembro no encontrado.[/]");
                return;
            }

            AnsiConsole.MarkupLine($"Miembro: [bold]{m.NombreCompleto}[/]");
            var nuevoTelefono = AnsiConsole.Ask<string>("Nuevo teléfono:");

            try
            {
                _service.ActualizarTelefono(cedula, nuevoTelefono);
                AnsiConsole.MarkupLine("[green]Teléfono actualizado con éxito![/]");
            }
            catch (System.Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }

        private void Eliminar()
        {
            AnsiConsole.MarkupLine("[bold blue]ELIMINAR MIEMBRO[/]");
            var cedula = AnsiConsole.Ask<string>("Cédula del miembro a eliminar:");
            var m = _service.BuscarMiembro(cedula);

            if (m == null)
            {
                AnsiConsole.MarkupLine("[red]Miembro no encontrado.[/]");
                return;
            }

            if (AnsiConsole.Confirm($"¿Estás seguro de que deseas eliminar a [bold]{m.NombreCompleto}[/]?"))
            {
                try
                {
                    _service.EliminarMiembro(cedula);
                    AnsiConsole.MarkupLine("[green]Miembro eliminado con éxito.[/]");
                }
                catch (System.Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
                }
            }
        }
    }
}
