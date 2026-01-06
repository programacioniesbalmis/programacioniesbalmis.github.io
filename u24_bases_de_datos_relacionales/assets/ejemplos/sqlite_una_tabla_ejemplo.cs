using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;

public class Biblioteca
{
    public static void CrearTablaLibros(SqliteConnection conexion)
    {
        string borrarTabla = "DROP TABLE IF EXISTS libro";
        using (SqliteCommand comando = new(borrarTabla, conexion))
        {
            comando.ExecuteNonQuery();
        }
        string crearTabla = """
            CREATE TABLE libro (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                titulo VARCHAR(60),
                autor VARCHAR(60)
            );
            """;
        using (SqliteCommand comando = new(crearTabla, conexion))
        {
            comando.ExecuteNonQuery();
        }
    }

    public static void AñadeLibros(SqliteConnection conexion)
    {
        string insertarDatos = """
            INSERT INTO libro (titulo, autor) VALUES
            ('Macbeth', 'William Shakespeare'),
            ('La Celestina (Tragicomedia de Calisto y Melibea)', 'Fernando de Rojas'),
            ('El Lazarillo de Tormes', 'Anónimo'),
            ('20.000 Leguas de Viaje Submarino', 'Julio Verne'),
            ('Alicia en el País de las Maravillas', 'Lewis Carrol'),
            ('Cien Años de Soledad', 'Gabriel García Márquez'),
            ('La tempestad', 'William Shakespeare');
            """;

        using SqliteCommand comando = new(insertarDatos, conexion);
        comando.ExecuteNonQuery();
    }


    static void VerLibros(SqliteConnection conexion)
    {
        string verLibros = "SELECT * FROM libro";
        using SqliteCommand query = new(verLibros, conexion);
        using SqliteDataReader rs = query.ExecuteReader();

        string separador = new(
            $"| {new('-', 3),-3} | {new('-', 55),-55} | {new('-', 25),-25} |\n"
        );

        StringBuilder salida = new StringBuilder(separador)
        .Append($"| {"Id",-3} | {"Título",-55} | {"Autor",-25} |\n")
        .Append(separador);
        while (rs.Read())
        {
            salida.Append(
                $"| {rs["id"],-3} | {rs["titulo"],-55} | {rs["autor"],-25} |\n"
            );
        }
        salida.Append(separador);

        Console.WriteLine(salida);
    }
    public static string RutaEjecucion() => Regex.Match(
        input: Directory.GetCurrentDirectory(),
        pattern: @"^(?<ruta>.*?)(?=\\bin)",
        options: RegexOptions.IgnoreCase
    ).Groups["ruta"].Value;

    public static void Main()
    {
        try
        {
            string rutaEjecucion = RutaEjecucion();
            string rutaDatos = Path.Combine(rutaEjecucion, "datos");
            if (!Directory.Exists(rutaDatos))
                Directory.CreateDirectory(rutaDatos);
            string cadenaConexion = $"Data Source={Path.Combine(rutaDatos, "biblioteca_1t.db")}";
            using SqliteConnection conexion = new(cadenaConexion);
            conexion.Open();
            CrearTablaLibros(conexion);
            AñadeLibros(conexion);
            VerLibros(conexion);
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}