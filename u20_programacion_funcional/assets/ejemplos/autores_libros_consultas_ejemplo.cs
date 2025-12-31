using System.Text;

public record class Autor(string Nombre, string Nacionalidad, DateTime Muerte, IEnumerable<Libro> Libros)
{
    public override string ToString() =>
    $"Nombre: {Nombre}\nNacionalidad: {Nacionalidad}\nMuerte: {Muerte:d}\n" +
    $"Libros:\n\t{string.Join("\n\t", Libros)}";
}
public record class Libro(string Titulo, int Año, int Paginas)
{
    public override string ToString() => $"Titulo: {Titulo,-37}  Año: {Año,-4}  Páginas: {Paginas}";
}


public static class Datos
{
    public static IEnumerable<Autor> Autores
    {
        get
        {
            yield return new
            (
                Nombre: "William Shakespeare",
                Nacionalidad: "Inglesa",
                Muerte: new(1616, 5, 3),
                Libros:
                [
                    new
                    (
                        Titulo: "Macbeth",
                        Año: 1623,
                        Paginas: 128
                    ),
                    new
                    (
                        Titulo: "La tempestad",
                        Año: 1611,
                        Paginas: 160
                    )
                ]
            );
            yield return new
            (
                Nombre: "Miguel de Cervantes",
                Nacionalidad: "Española",
                Muerte: new(1616, 6, 22),
                Libros:
                [
                        new
                        (
                            Titulo: "Don Quijote de la Mancha",
                            Año: 1605,
                            Paginas: 1376
                        ),
                        new
                        (
                            Titulo: "La Galatea",
                            Año: 1585,
                            Paginas: 664
                        ),
                        new
                        (
                            Titulo: "Los trabajos de Persiles y Sigismunda",
                            Año: 1617,
                            Paginas: 888
                        ),
                        new
                        (
                            Titulo: "Novelas ejemplares",
                            Año: 1613,
                            Paginas: 1160
                        )
                ]
            );
            yield return new
            (
                Nombre: "Fernando de Rojas",
                Nacionalidad: "Española",
                Muerte: new(1541, 2, 7),
                Libros:
                [
                    new
                    (
                        Titulo: "La Celestina",
                        Año: 1500,
                        Paginas: 160
                    )
                ]
            );
        }
    }
}

public class Principal
{
    public static void Main()
    {
        Console.WriteLine("Consulta 1: Nombres de autores con más de un libro ordenados.");
        IEnumerable<string> nombreAutoresConMasDeUnLibro = Datos.Autores
                                                                .Where(a => a.Libros.Count() > 1)
                                                                .Select(a => a.Nombre)
                                                                .OrderBy(n => n);
        Console.WriteLine(string.Join("\n", nombreAutoresConMasDeUnLibro) + "\n");

        Console.WriteLine("Consulta 2: Total de libros escritos por escritores Españoles.");
        var totalLibros = Datos.Autores
                            .Where(a => a.Nacionalidad == "Española")
                            .SelectMany(a => a.Libros)
                            .Count();
        Console.WriteLine($"Hay {totalLibros} libros escritos por españoles\n");

        Console.WriteLine("Consulta 3: Nombre y año muerte de Autores agrupados por siglo en el que murioeron.");
        var autoresFallecidosPorSiglos = Datos.Autores
                                              .Select(a => new { Autor = a.Nombre, AñoMuerte = a.Muerte.Year })
                                              .OrderBy(a => a.AñoMuerte)
                                              .GroupBy(a => a.AñoMuerte / 100 + 1,
                                                       (siglo, autores) => new
                                                       {
                                                           Siglo = siglo,
                                                           Autores = autores
                                                       });
        StringBuilder salida = new();
        foreach (var autoresXSiglo in autoresFallecidosPorSiglos)
        {
            salida.AppendLine($"Siglo {autoresXSiglo.Siglo}:");
            foreach (var autor in autoresXSiglo.Autores)
                salida.AppendLine($"\t{autor.Autor} fallecido en {autor.AñoMuerte}");
        }
        Console.WriteLine(salida);

        Console.WriteLine("Consulta 4: Número total de páginas escritas por William Shakespeare.");
        var totalPaginas = Datos.Autores.Where(a => a.Nombre == "William Shakespeare")
                                        .SelectMany(a => a.Libros.Select(l => l.Paginas)).Sum();
        Console.WriteLine($"William Shakespeare escribió {totalPaginas} páginas.");
    }
}
