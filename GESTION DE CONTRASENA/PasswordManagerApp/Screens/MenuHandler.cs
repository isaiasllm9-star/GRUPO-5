using Spectre.Console;
using PasswordManagerApp.Models;
using PasswordManagerApp.Services;
using PasswordManagerApp.Repositories;

namespace PasswordManagerApp.Screens
{
    public static class MenuHandler
    {
        public static void ShowMainMenu(CredentialRepository credRepo, CategoryRepository catRepo)
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(
                    new FigletText("PassGuard")
                        .LeftJustified()
                        .Color(Color.Cyan1));

                AnsiConsole.Write(new Panel("[italic]Gestor de Contraseñas Seguro - Arquitectura en Capas[/]").BorderColor(Color.Green));

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[yellow]¿Qué deseas hacer?[/]")
                        .PageSize(10)
                        .AddChoices(new[] {
                            "1. Ver Credenciales",
                            "2. Agregar Nueva Credencial",
                            "3. Buscar por Sitio",
                            "4. Editar Credencial",
                            "5. Eliminar Credencial",
                            "6. Estadísticas",
                            "7. Salir"
                        }));

                switch (choice)
                {
                    case "1. Ver Credenciales":
                        CredentialManager.ListCredentials(credRepo);
                        break;
                    case "2. Agregar Nueva Credencial":
                        CredentialManager.AddCredential(credRepo, catRepo);
                        break;
                    case "3. Buscar por Sitio":
                        CredentialManager.SearchCredential(credRepo);
                        break;
                    case "4. Editar Credencial":
                        CredentialManager.EditCredential(credRepo);
                        break;
                    case "5. Eliminar Credencial":
                        CredentialManager.DeleteCredential(credRepo);
                        break;
                    case "6. Estadísticas":
                        CredentialManager.ShowStats(credRepo);
                        break;
                    case "7. Salir":
                        return;
                }
            }
        }
    }
}
