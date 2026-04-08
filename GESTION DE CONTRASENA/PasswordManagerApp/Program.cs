using PasswordManagerApp.Database;
using PasswordManagerApp.Screens;
using PasswordManagerApp.Repositories;
using Spectre.Console;

// Initializing database
using var db = new AppDbContext();

// Ensure db is created
db.Database.EnsureCreated();

var credRepo = new CredentialRepository(db);
var catRepo = new CategoryRepository(db);

try
{
    MenuHandler.ShowMainMenu(credRepo, catRepo);
}
catch (Exception ex)
{
    AnsiConsole.WriteException(ex);
    AnsiConsole.MarkupLine("[red]Ocurrió un error inesperado. Presiona cualquier tecla para salir...[/]");
    Console.ReadKey();
}
