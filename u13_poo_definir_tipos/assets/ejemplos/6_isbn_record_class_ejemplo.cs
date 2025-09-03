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

public static class Program
{
    public static void Main()
    {
        Isbn13 isbn1 = new(978, 84, 935489, 1);
        Console.WriteLine(isbn1.ATexto("-"));

        Isbn13 isbn2 = new(978, 1, 78528, 144);
        Console.WriteLine(isbn2.ATexto(" "));
    }
}