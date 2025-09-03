using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

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
    private List<Escritor> Autores { get; }

    public Libro(
        Isbn13 isbn,
        string titulo,
        DateOnly fechaPublicacion,
        int paginas,
        List<Escritor> autores)
    {
        Isbn = isbn;
        Titulo = titulo;
        FechaPublicacion = fechaPublicacion;
        Paginas = paginas;
        Autores = [.. autores];
    }

    public string ATexto()
    {

        StringBuilder autoresTexto = new();
        foreach (Escritor autor in Autores)
        {
            autoresTexto.AppendLine($"\t- {autor.Nombre}");
        }
        return $"""
                Libro
                --------------------------
                ISBN: {Isbn.ATexto("-")}
                Título: {Titulo}
                Fecha Publicación: {FechaPublicacion:dd-MM-yyyy}
                Páginas: {Paginas}
                Autores:
                {autoresTexto}
                """;
    }
}


public class Escritor
{
    public Guid Id { get; }
    public string Nombre { get; }
    public DateOnly FechaNacimiento { get; }
    public int Edad => DateTime.Now.Year - FechaNacimiento.Year;
    private List<Libro> LibrosPublicados { get; }
    public int Publicaciones => LibrosPublicados.Count;

    public Escritor(string nombre, DateOnly fechaNacimiento)
    {
        Id = Guid.NewGuid();
        Nombre = nombre;
        FechaNacimiento = fechaNacimiento;
        LibrosPublicados = [];
    }

    public void Publica(Libro libro)
    {
        LibrosPublicados.Add(libro);
    }

    public string ATexto()
    {

        StringBuilder librosTexto = new();
        foreach (Libro libro in LibrosPublicados)
        {
            librosTexto.AppendLine($"\t- {libro.Titulo}");
        }
        return $"""
                Escritor
                --------------------------
                ID: {Id}
                Nombre: {Nombre}
                Nacimiento: {FechaNacimiento}
                Publicaciones: {Publicaciones}
                Edad: {Edad}
                Libros:
                {librosTexto}
                """;
    }
}

public class Editorial
{
    public string Cif { get; }
    public string Nombre { get; }
    public string Direccion { get; }
    private List<Libro> LibrosPublicados  { get; }

    public static bool ValidarCif(string cif) =>
            !string.IsNullOrWhiteSpace(cif)
            && Regex.IsMatch(cif, @"^[A-Z]\d{8}$");

    public Editorial(string cif, string nombre, string direccion)
    {
        Debug.Assert(
            condition: ValidarCif(cif),
            message: $"El CIF {cif} no es válido.");
        Cif = cif;
        Nombre = nombre;
        Direccion = direccion;
        LibrosPublicados = [];
    }

    public Libro Publica(
        string titulo,
        int paginas,
        List<Escritor> autores)
    {
        Libro l = new(
            isbn: new(
                prefijo: 978,
                grupoDeRegistro: 84,
                titular: 935489,
                publicacion: LibrosPublicados.Count + 1),
            titulo: titulo,
            fechaPublicacion: DateOnly.FromDateTime(DateTime.Now),
            paginas: paginas,
            autores: autores);

        foreach (Escritor autor in autores) autor.Publica(l);
        LibrosPublicados.Add(l);

        return l;
    }

    public string ATexto()
    {
        StringBuilder librosTexto = new();
        foreach (Libro libro in LibrosPublicados)
        {
            librosTexto.AppendLine($"\t- {libro.Titulo}");
        }
        return $"""
                Editorial
                --------------------------
                CIF: {Cif}
                Nombre: {Nombre}
                Dirección: {Direccion}
                Libros Publicados:
                {librosTexto}
                """;
    }
}


public static class Program
{
    public static void Main()
    {
        Editorial editorial = new(
            cif: "A12345678",
            nombre: "Editorial Balmis S.L.",
            direccion: "Calle La Cerámica, 12");

        Escritor e1 = new(
            nombre: "María Pérez",
            fechaNacimiento: new(1980, 5, 15));
        Escritor e2 = new(
            nombre: "Juan López",
            fechaNacimiento: new(1975, 3, 20));
        Escritor e3 = new(
            nombre: "Ana García",
            fechaNacimiento: new(1990, 8, 30));

        Libro l1 = editorial.Publica(
            titulo: "Aprendiendo C#",
            paginas: 300,
            autores: [e1, e2]);

        Console.WriteLine(l1.ATexto());
        Console.WriteLine(e1.ATexto());
        Console.WriteLine(e2.ATexto());
        Console.WriteLine(e3.ATexto());
        Console.WriteLine(editorial.ATexto());

        Libro l2 = editorial.Publica(
            titulo: "Roles entre clases",
            paginas: 200,
            autores: [e2, e3]);

        Console.WriteLine(l2.ATexto());
        Console.WriteLine(e1.ATexto());
        Console.WriteLine(e2.ATexto());
        Console.WriteLine(e3.ATexto());
        Console.WriteLine(editorial.ATexto());
    }
}
