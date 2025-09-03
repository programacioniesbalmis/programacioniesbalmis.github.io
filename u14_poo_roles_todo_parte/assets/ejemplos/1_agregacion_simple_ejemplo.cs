
using System.Diagnostics;

namespace Ejemplo;

public class Escritor
{
    private int _publicaciones;
    public string Nombre { get; }
    public int Nacimiento { get; }
    public int Edad => DateTime.Now.Year - Nacimiento;

    public int Publicaciones
    {
        get => _publicaciones;
        private set
        {
            Debug.Assert(value >= 0, "El número de publicaciones no puede ser negativo.");
            _publicaciones = value;
        }
    }
    public Escritor(string nombre, int nacimiento)
    {
        Nombre = nombre;            // Las propiedades autoimplementadas 
        Nacimiento = nacimiento;    // en el constructor será el único sitio donde se
        Publicaciones = 0;          // puedan asignar.
    }

    public string ATexto() => $"""
                    Nombre: {Nombre}
                    Nacimiento: {Nacimiento}
                    Publicaciones: {Publicaciones}
                    """;

    public Libro Escribe(string titulo)
    {
        Range r = 400..800;
        Publicaciones++;
        return new (
            titulo: titulo, 
            año: DateTime.Now.Year, 
            paginas: new Random().Next(r.Start.Value, r.End.Value + 1));
    }
}

public class Libro
{
    private int _paginasLeidas;
    public string Titulo { get; }
    public int Año { get; }
    public int Paginas { get; }

    public int PaginasLeidas
    {
        get => _paginasLeidas;
        set
        {
            Debug.Assert(
                condition: value >= 0,
                message: "El número de páginas leídas no puede ser negativo.");
            Debug.Assert(
                condition: value <= Paginas,
                message: "El número de páginas leídas no puede ser mayor que el total de páginas.");
            _paginasLeidas = value;
        }
    }

    public int PorcentajeLeido => Convert.ToInt32(PaginasLeidas * 100D / Paginas);

    public Libro(string titulo,
        int año,
        int paginas)
    {
        Titulo = titulo;
        Año = año;
        Paginas = paginas;
        PaginasLeidas = 0;
    }
    public int Lee(in int paginas)
    {
        int leidas = Math.Clamp(paginas, 0, Paginas - PaginasLeidas);
        PaginasLeidas += leidas;
        return leidas;
    }
    public string ATexto() => $"""
                Título: {Titulo}
                Año: {Año}
                Páginas: {Paginas}
                Páginas leídas: {PaginasLeidas}
                """;
}

public static class Program
{
    public static void Main()
    {
        Libro libro = new("Code Complete 2", 2004, 960);

        Console.WriteLine(libro.ATexto());

        const int MAXIMO_PAGINAS_A_LEER = 300;
        int leidas;
        while ((leidas = libro.Lee(MAXIMO_PAGINAS_A_LEER)) > 0)
        {
            Console.WriteLine($"leidas: {leidas} {libro.PorcentajeLeido}%");
        }
    }
}
