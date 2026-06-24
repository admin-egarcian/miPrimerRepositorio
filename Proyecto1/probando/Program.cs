using System;
using System.IO;
using Newtonsoft.Json.Linq;

static string? FindAppSettings(string fileName)
{
    var directory = new DirectoryInfo(AppContext.BaseDirectory);
    while (directory != null)
    {
        var candidate = Path.Combine(directory.FullName, fileName);
        if (File.Exists(candidate))
        {
            return candidate;
        }
        directory = directory.Parent;
    }
    return null;
}

static void SimulateConnection(string connectionString)
{
    Console.WriteLine("Simulando conexión a la base de datos...");
    Console.WriteLine($"Cadena de conexión: {connectionString}");
    Console.WriteLine("Intentando conectar...");
    System.Threading.Thread.Sleep(1000);
    Console.WriteLine("Conexión simulada exitosa.");
}

var settingsPath = FindAppSettings("appsettings.json");
if (settingsPath is null)
{
    Console.WriteLine("No se encontró appsettings.json en el árbol de directorios.");
    return;
}

var json = File.ReadAllText(settingsPath);
var config = JObject.Parse(json);
var connectionString = config["ConnectionStrings"]?["DefaultConnection"]?.Value<string>()
                       ?? config["ConnectionString"]?.Value<string>();

if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("No se encontró una cadena de conexión válida en appsettings.json.");
    return;
}

SimulateConnection(connectionString);
