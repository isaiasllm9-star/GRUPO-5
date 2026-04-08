using Spectre.Console;
using PasswordManagerApp.Models;
using PasswordManagerApp.Services;
using PasswordManagerApp.Repositories;

namespace PasswordManagerApp.Screens
{
    public static class CredentialManager
    {
        public static void ListCredentials(CredentialRepository credRepo)
        {
            var credentials = credRepo.GetAllWithCategories();

            if (!credentials.Any())
            {
                AnsiConsole.MarkupLine("[yellow]No hay credenciales guardadas.[/]");
                AnsiConsole.MarkupLine("[grey]Presiona cualquier tecla para continuar...[/]");
                Console.ReadKey();
                return;
            }

            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.AddColumn("[cyan]ID[/]");
            table.AddColumn("[cyan]Sitio[/]");
            table.AddColumn("[cyan]Usuario[/]");
            table.AddColumn("[cyan]Categoría[/]");
            table.AddColumn("[cyan]Contraseña (Enc)[/]");

            foreach (var cred in credentials)
            {
                table.AddRow(
                    cred.Id.ToString(),
                    cred.SiteName,
                    cred.Username,
                    cred.Category.Name,
                    "[grey]********[/]"
                );
            }

            AnsiConsole.Write(table);

            if (AnsiConsole.Confirm("¿Deseas ver una contraseña desencriptada?"))
            {
                var id = AnsiConsole.Ask<int>("[yellow]Ingresa el ID de la credencial:[/]");
                var cred = credRepo.GetById(id);

                if (cred != null)
                {
                    string decrypted = EncryptionService.Decrypt(cred.EncryptedPassword);
                    AnsiConsole.MarkupLine($"[green]Contraseña para {cred.SiteName}:[/] [bold yellow]{decrypted}[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Credencial no encontrada.[/]");
                }
            }

            AnsiConsole.MarkupLine("[grey]Presiona cualquier tecla para continuar...[/]");
            Console.ReadKey();
        }

        public static void AddCredential(CredentialRepository credRepo, CategoryRepository catRepo)
        {
            AnsiConsole.Write(new Rule("[yellow]Nueva Credencial[/]").LeftJustified());

            var site = AnsiConsole.Ask<string>("Nombre del Sitio:");
            var user = AnsiConsole.Ask<string>("Nombre de Usuario:");
            var pass = AnsiConsole.Prompt(
                new TextPrompt<string>("Contraseña:")
                    .PromptStyle("red")
                    .Secret());

            var categories = catRepo.GetAll();
            var categoryChoice = AnsiConsole.Prompt(
                new SelectionPrompt<Category>()
                    .Title("Selecciona una Categoría:")
                    .UseConverter(c => c.Name)
                    .AddChoices(categories));

            var notes = AnsiConsole.Ask<string>("[grey]Notas (opcional):[/]", "");

            var newCred = new Credential
            {
                SiteName = site,
                Username = user,
                EncryptedPassword = EncryptionService.Encrypt(pass),
                CategoryId = categoryChoice.Id,
                Notes = notes
            };

            credRepo.Add(newCred);

            AnsiConsole.MarkupLine("[green]✔ Credencial guardada correctamente![/]");
            Thread.Sleep(1500);
        }

        public static void SearchCredential(CredentialRepository credRepo)
        {
            var searchTerm = AnsiConsole.Ask<string>("Ingresa el nombre del sitio a buscar:");
            var results = credRepo.SearchBySiteName(searchTerm);

            if (!results.Any())
            {
                AnsiConsole.MarkupLine("[red]No se encontraron resultados.[/]");
            }
            else
            {
                var table = new Table().Border(TableBorder.Rounded);
                table.AddColumn("[cyan]ID[/]");
                table.AddColumn("[cyan]Sitio[/]");
                table.AddColumn("[cyan]Usuario[/]");
                table.AddColumn("[cyan]Categoría[/]");
                
                foreach (var r in results) 
                {
                    table.AddRow(
                        r.Id.ToString(), 
                        r.SiteName, 
                        r.Username,
                        r.Category?.Name ?? "[grey]N/A[/]"
                    );
                }
                AnsiConsole.Write(table);
            }
            
            AnsiConsole.MarkupLine("[grey]Presiona cualquier tecla para continuar...[/]");
            Console.ReadKey();
        }

        public static void EditCredential(CredentialRepository credRepo)
        {
            var id = AnsiConsole.Ask<int>("ID de la credencial a editar:");
            var cred = credRepo.GetById(id);

            if (cred == null)
            {
                AnsiConsole.MarkupLine("[red]No encontrada.[/]");
                return;
            }

            cred.SiteName = AnsiConsole.Ask<string>($"Sitio ({cred.SiteName}):", cred.SiteName);
            cred.Username = AnsiConsole.Ask<string>($"Usuario ({cred.Username}):", cred.Username);
            
            if (AnsiConsole.Confirm("¿Deseas cambiar la contraseña?"))
            {
                var newPass = AnsiConsole.Prompt(
                    new TextPrompt<string>("Nueva Contraseña:")
                        .Secret());
                cred.EncryptedPassword = EncryptionService.Encrypt(newPass);
            }

            credRepo.Update(cred);
            AnsiConsole.MarkupLine("[green]✔ Actualizada.[/]");
            Thread.Sleep(1000);
        }

        public static void DeleteCredential(CredentialRepository credRepo)
        {
            var id = AnsiConsole.Ask<int>("ID de la credencial a eliminar:");
            var cred = credRepo.GetById(id);

            if (cred != null)
            {
                if (AnsiConsole.Confirm($"[red]¿Estás seguro de eliminar la credencial de {cred.SiteName}?[/]"))
                {
                    credRepo.Delete(id);
                    AnsiConsole.MarkupLine("[green]Eliminada.[/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No encontrada.[/]");
            }
            Thread.Sleep(1000);
        }

        public static void ShowStats(CredentialRepository credRepo)
        {
            var total = credRepo.GetTotalCount();
            var byCategory = credRepo.GetCountByCategory();

            var chart = new BreakdownChart()
                .FullSize()
                .AddItem("Total", total, Color.Cyan1);

            foreach (var cat in byCategory)
            {
                chart.AddItem(cat.Key, cat.Value, Color.Yellow);
            }

            AnsiConsole.Write(new Panel(chart).Header("Estadísticas de Credenciales"));
            
            AnsiConsole.MarkupLine("[grey]Presiona cualquier tecla para continuar...[/]");
            Console.ReadKey();
        }
    }
}
