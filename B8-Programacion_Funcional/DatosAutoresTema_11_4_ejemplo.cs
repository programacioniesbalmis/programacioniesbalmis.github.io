using System;
using System.Collections.Generic;
using System.Linq;

public class Autor
{
    public string Nombre { get; init; }
    public string Nacionalidad { get; init; }
    public DateTime Muerte { get; init; }
    public IEnumerable<Libro> Libros { get; init; }

    public override string ToString() =>
    $"Nombre: {Nombre}\nNacionalidad: {Nacionalidad}\nMuerte: {Muerte.ToShortDateString()}\n" +
    $"Libros:\n\t{string.Join("\n\t", Libros)}";
}
public class Libro
{
    public string Titulo { get; init; }
    public int Año { get; init; }
    public int Paginas { get; init; }

    public override string ToString() =>
    $"Titulo: {Titulo,-37}  Año: {Año,-4}  Páginas: {Paginas}";
}

public static class Datos
{
    public static IEnumerable<Autor> Autores
    {
        get
        {
            yield return new()
            {
                Nombre = "William Shakespeare",
                Nacionalidad = "Inglesa",
                Muerte = new DateTime(1616, 5, 3),
                Libros = new Libro[]
                {
                        new Libro()
                        {
                            Titulo = "Macbeth",
                            Año = 1623,
                            Paginas = 128
                        },
                        new Libro()
                        {
                            Titulo = "La tempestad",
                            Año = 1611,
                            Paginas = 160
                        }
                }
            };
            yield return new()
            {
                Nombre = "Miguel de Cervantes",
                Nacionalidad = "Española",
                Muerte = new DateTime(1616, 6, 22),
                Libros = new Libro[]
                {
                        new Libro()
                        {
                            Titulo = "Don Quijote de la Mancha",
                            Año = 1605,
                            Paginas = 1376
                        },
                        new Libro()
                        {
                            Titulo = "La Galatea",
                            Año = 1585,
                            Paginas = 664
                        },
                        new Libro()
                        {
                            Titulo = "Los trabajos de Persiles y Sigismunda",
                            Año = 1617,
                            Paginas = 888
                        },
                        new Libro()
                        {
                            Titulo = "Novelas ejemplares",
                            Año = 1613,
                            Paginas = 1160
                        }
                }
            };
            yield return new()
            {
                Nombre = "Fernando de Rojas",
                Nacionalidad = "Española",
                Muerte = new DateTime(1541, 2, 7),
                Libros = new Libro[]
                {
                        new Libro()
                        {
                            Titulo = "La Celestina",
                            Año = 1500,
                            Paginas = 160
                        }
                }
            };
        }
    }
}

class Principal
{

    static void Main()
    {
        string SeparadorDato = "\n" + new string('-', 80) + "\n";

        Console.WriteLine(
                SeparadorDato
                + string.Join(SeparadorDato, Datos.Autores)
                + SeparadorDato);
    }
}
