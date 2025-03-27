using System.Text;
using Microsoft.Data.Sqlite;

namespace Biblioteca
{
    public class Program
    {
        public static void CrearTablaLibros(string cadenaConexion)
        {
            using SqliteConnection conexion = new(cadenaConexion);
            conexion.Open();

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

        static void MostrarLibros(List<Libro> libros)
        {
            string separador = new($"| {new('-', 3),-3} | {new('-', 55),-55} | {new('-', 25),-25} |\n");
            StringBuilder salida = new StringBuilder(separador)
                .Append($"| {"Id",-3} | {"Título",-55} | {"Autor",-25} |\n")
                .Append(separador);

            foreach (var libro in libros)
            {
                salida.Append($"| {libro.Id,-3} | {libro.Titulo,-55} | {libro.Autor,-25} |\n");
            }
            salida.Append(separador);

            Console.WriteLine(salida);
        }


        static void MostrarLibrosPorAutor(List<LibroDAO.LibrosPorAutorDTO> librosPorAutor)
        {
            string separador = new($"| {new('-', 25),-25} | {new('-', 10),-10} |\n");
            StringBuilder salida = new StringBuilder(separador)
                .Append($"| {"Autor",-25} | {"Títulos",-10} |\n")
                .Append(separador);

            foreach (var libroPorAutor in librosPorAutor)
            {
                salida.Append($"| {libroPorAutor.Autor,-25} | {libroPorAutor.Titulos,-10} |\n");
            }
            salida.Append(separador);

            Console.WriteLine(salida);
        }

        public static void Main()
        {
            try
            {
                string cadenaConexion = "Data Source=biblioteca_1t.db";
                CrearTablaLibros(cadenaConexion);

                using LibroDAO libroDAO = new(cadenaConexion);

                libroDAO.Create(new Libro { Titulo = "Macbeth", Autor = "William Shakespeare" });
                libroDAO.Create(new Libro { Titulo = "20.000 Leguas de Viaje Submarino", Autor = "Julio Verne" });
                MostrarLibros(libroDAO.Read());

                Console.WriteLine("Creando Hamlet...");
                libroDAO.Create(new Libro { Titulo = "Hamlet", Autor = "Julio Verne" });
                MostrarLibros(libroDAO.Read());

                Console.WriteLine("Modificando Hamlet...");
                Libro hamlet = libroDAO.Read("Hamlet")!;
                hamlet.Autor = "William Shakespeare";
                libroDAO.Update(hamlet);
                MostrarLibros(libroDAO.Read());

                Console.WriteLine("Borrando Hamlet...");
                libroDAO.Delete(hamlet.Id);
                MostrarLibros(libroDAO.Read());

                Console.WriteLine("Insertando Sueño de una noche de verano...");
                libroDAO.Create(new Libro { Titulo = "Sueño de una noche de verano", Autor = "William Shakespeare" });
                MostrarLibros(libroDAO.Read());

                Console.WriteLine("Consultando libros por autor...");
                MostrarLibrosPorAutor(libroDAO.ReadLibrosPorAutor());

            }
            catch (SqliteException e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}