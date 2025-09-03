
namespace Ejemplo;

public class Libro
{
    private readonly string _titulo;
    private readonly int _año;
    private readonly int _paginas;
    private int _paginasLeidas;

    public string GetTitulo()
    {
        return _titulo;
    }

    public int GetAño()
    {
        return _año;
    }

    public int GetPaginas()
    {
        return _paginas;
    }

    public int GetPaginasLeidas()
    {
        return _paginasLeidas;
    }
    public void SetPaginasLeidas(int paginas)
    {
        _paginasLeidas = paginas;
    }

    public Libro(string titulo,
        int año,
        int paginas)
    {
        _titulo = titulo;
        _año = año;
        _paginas = paginas;
        SetPaginasLeidas(0);
    }
    public Libro(Libro l)
    {
        _titulo = l._titulo;
        _año = l._año;
        _paginas = l._paginas;
        SetPaginasLeidas(l._paginasLeidas);
    }

    public int Lee(in int paginas)
    {
        int leídas = Math.Clamp(paginas, 0, GetPaginas() - GetPaginasLeidas());
        SetPaginasLeidas(GetPaginasLeidas() + leídas);
        return leídas;
    }

    public int PorcentajeLeido()
    {
        return Convert.ToInt32(GetPaginasLeidas() * 100D / GetPaginas());
    }

    public string ATexto()
    {
        return $"""
        Título: {GetTitulo()}
        Año: {GetAño()}
        Páginas: {GetPaginas()}
        """;
    } 
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
            Console.WriteLine($"leidas: {leidas} {libro.PorcentajeLeido()}%");
        }
    }
}
