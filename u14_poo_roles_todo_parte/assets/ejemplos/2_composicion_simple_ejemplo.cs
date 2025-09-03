
using System.Diagnostics;

namespace Ejemplo;

public record class Isbn13
{
    private static readonly int[] PREFIJOS = [978, 979];
    private const int MAX_LONGITUD_GRUPO = 5;
    private const int MAX_LONGITUD_TITULAR = 7;
    private const int MAX_LONGITUD_PUBLICACION = 6;
    private const int LONGITUD_ISBN = 13;

    public string Prefijo { get; }
    public string GrupoDeRegistro { get; }
    public string Titular { get; }
    public string Publicacion { get; }

    public int DigitoDeControl
    {
        get
        {
            string isbn = string.Join("", Prefijo, GrupoDeRegistro, Titular, Publicacion);
            double suma = 0;
            for (int i = 0; i < isbn.Length; i++)
                suma += ((i % 2 == 0) ? 1 : 3) * int.Parse(isbn[i].ToString());
            double resto = suma % 10;
            return resto == 0 ? 0 : Convert.ToInt32(10 - resto);
        }
    }

    public Isbn13(in int prefijo, in int grupoDeRegistro, in int titular, in int publicacion)
    {
        Prefijo = prefijo.ToString();
        bool esPrefijoValido = Array.IndexOf(PREFIJOS, prefijo) >= 0;
        Debug.Assert(
            condition: esPrefijoValido,
            message: $"El prefijo {prefijo} no es válido. Debe ser 978 o 979.");

        GrupoDeRegistro = grupoDeRegistro.ToString();
        Debug.Assert(
            condition: GrupoDeRegistro.Length <= MAX_LONGITUD_GRUPO,
            message: $"El grupo de registro {grupoDeRegistro} es demasiado largo.");

        Titular = titular.ToString();
        Debug.Assert(
            condition: Titular.Length <= MAX_LONGITUD_TITULAR,
            message: $"El titular {titular} es demasiado largo.");

        Publicacion = publicacion.ToString();
        Debug.Assert(
            condition: Publicacion.Length <= MAX_LONGITUD_PUBLICACION,
            message: $"La publicación {publicacion} es demasiado larga.");

        string isbn = string.Join("", prefijo, grupoDeRegistro, titular, publicacion);
        int excesoLongitud = isbn.Length - (LONGITUD_ISBN - 1);
        if (excesoLongitud < 0)
            Publicacion = Publicacion.PadLeft(Math.Abs(excesoLongitud) + Publicacion.Length, '0');
    }

    public string ATexto(string separador) =>
        string.Join(separador, Prefijo, GrupoDeRegistro, Titular, Publicacion, DigitoDeControl.ToString());
}

public class Libro
{
    public Isbn13 Isbn { get; }
    public string Titulo { get; }
    public DateOnly FechaPublicacion { get; }
    public int Paginas { get; }
    public Escritor Autor { get; }


    public Libro(
        Isbn13 isbn,
        string titulo,
        DateOnly fechaPublicacion,
        int paginas,
        Escritor autor)
    {
        Isbn = isbn;
        Titulo = titulo;
        FechaPublicacion = fechaPublicacion;
        Paginas = paginas;
        Autor = autor;
    }

    public string ATexto() => $"""
                Libro
                --------------------------
                ISBN: {Isbn.ATexto("-")}
                Título: {Titulo}
                Fecha Publicación: {FechaPublicacion:dd-MM-yyyy}
                Páginas: {Paginas}
                Autor --------------------
                {Autor.Nombre}
                """;
}


public class Escritor
{
    private int _publicaciones;
    public Guid Id { get; }
    public string Nombre { get; }
    public DateOnly FechaNacimiento { get; }
    public int Edad => DateTime.Now.Year - FechaNacimiento.Year;

    public int Publicaciones
    {
        get => _publicaciones;
        private set
        {
            Debug.Assert(value >= 0, "El número de publicaciones no puede ser negativo.");
            _publicaciones = value;
        }
    }
    public Escritor(string nombre, DateOnly fechaNacimiento)
    {
        Id = Guid.NewGuid();
        Nombre = nombre;
        FechaNacimiento = fechaNacimiento;
        Publicaciones = 0;
    }

    public string ATexto() => $"""
                    Id: {Id}
                    Nombre: {Nombre}
                    Nacimiento: {FechaNacimiento}
                    Publicaciones: {Publicaciones}
                    """;

    public Libro Escribe(string titulo, Isbn13 isbn)
    {
        Range r = 400..800;
        Publicaciones++;
        return new(
            isbn: isbn,
            titulo: titulo,
            fechaPublicacion: DateOnly.FromDateTime(DateTime.Now),
            paginas: new Random().Next(r.Start.Value, r.End.Value + 1),
            autor: this);
    }
}

public static class Program
{
    public static void Main()
    {
        Escritor e = new(
            nombre: "María Pérez",
            fechaNacimiento: new DateOnly(1985, 5, 15));
        Libro l = e.Escribe(
            titulo: "Programación en C#",
            isbn: new Isbn13(
                prefijo: 978,
                grupoDeRegistro: 84,
                titular: 935489,
                publicacion: 1));
        Console.WriteLine(l.ATexto());
    }
}
