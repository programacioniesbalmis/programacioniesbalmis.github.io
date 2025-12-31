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

public record class LibroDto(string Libro, string Autor, int Paginas)
{
    public override string ToString() => $"Libro: {Libro}\nAutor: {Autor}\nPáginas: {Paginas}";
}

class Principal
{
    public static IEnumerable<LibroDto> LibrosDeMenosdeMilPaginas() =>
    Datos.Autores.SelectMany(a => a.Libros.Where(l => l.Paginas < 1000)
                                   .Select(l => new LibroDto(
                                    Libro: l.Titulo, Autor: a.Nombre, Paginas: l.Paginas)));
                                    
    static void Main()
    {
        Console.WriteLine(string.Join("\n\n", Datos.Autores));
        IEnumerable<Autor> autoresConLibrosPublicadosDuranteSigloXVII =
        Datos.Autores.Where(a => a.Libros.Any(l => l.Año >= 1600));

        IEnumerable<Libro> librosSigloXVII =
        Datos.Autores.SelectMany(a => a.Libros.Where(l => l.Año is > 1600 and < 1701));

        Console.WriteLine("\n\n");
        // IEnumerable<Autor> → (Autor → IEnumerable<'a>) → IEnumerable<'a>
        // Donde 'a es un tipo anónimo que tiene los campos solicitados de la proyección.
        var librosDeMenosDeMilPaginas = Datos.Autores.SelectMany(a => a.Libros.Where(l => l.Paginas < 1000)
                                                             .Select(l => new
                                                             {
                                                                 Libro = l.Titulo,
                                                                 Autor = a.Nombre,
                                                                 Páginas = l.Paginas
                                                             }));

        Console.WriteLine("\n\n");
        IEnumerable<LibroDto> librosDeMenosDeMilPaginasDto = LibrosDeMenosdeMilPaginas();
        Console.WriteLine(string.Join("\n\n", librosDeMenosDeMilPaginasDto));

    }
}
