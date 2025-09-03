using System.Diagnostics;

namespace Ejemplo;

public class Matriz<T> where T : IComparable<T>
{
    private T[][] Datos { get; }

    public Matriz(T[][] datos)
    {
        if (datos.Length <= 0)
            throw new ArgumentException("Debes proporcionar al menos una fila de datos");
        if (datos[0] == null || datos[0].Length <= 0)
            throw new ArgumentException("Debes proporcionar al menos una columna de datos");
        for (int i = 1; i < datos.Length; i++)
        {
            if (datos[i] == null)
                throw new ArgumentException("Ninguna fila puede ser null");
            if (datos[i].Length != datos[0].Length)
                throw new ArgumentException("Todas las filas deben tener las mismas columnas");
        }
        Datos = datos;
    }

    public Matriz<T> Traspuesta
    {
        get
        {
            int filas = Datos.Length;
            int columnas = Datos[0].Length;
            T[][] transpuesta = new T[columnas][];
            for (int i = 0; i < columnas; i++)
            {
                transpuesta[i] = new T[filas];
                for (int j = 0; j < filas; j++)
                {
                    transpuesta[i][j] = Datos[j][i];
                }
            }
            return new Matriz<T>(transpuesta);
        }
    }

    public override bool Equals(object? obj)
    {
        bool sonIguales = true;
        
        if (obj is not Matriz<T> otraMatriz)
        {
            sonIguales = false;
        }
        else if (Datos.Length != otraMatriz.Datos.Length || Datos[0].Length != otraMatriz.Datos[0].Length)
        {
            sonIguales = false;
        }
        else
        {
            int filas = Datos.Length;
            int columnas = Datos[0].Length;
            for (int i = 0; i < filas && sonIguales; i++)
                for (int j = 0; j < columnas && sonIguales; j++)
                    sonIguales = Datos[i][j].CompareTo(otraMatriz.Datos[i][j]) == 0;
        }

        return sonIguales;
    }

    public override int GetHashCode() => HashCode.Combine(Datos);

    public override string ToString()
    {
        string resultado = string.Empty;
        foreach (var fila in Datos)
        {
            resultado += string.Join(", ", fila) + "\n";
        }
        return resultado.TrimEnd('\n');
    }
}

public class Hora : IComparable<Hora>
{
    public int H { get; }
    public int M { get; }
    public Hora(int h, int m)
    {
        H = h;
        M = m;
    }
    public override string ToString() => $"{H:D2}:{M:D2}";
    public int CompareTo(Hora? hora)
    {
        // Si no quisiéramos permitir null
        // ArgumentNullException.ThrowIfNull(hora);
        int comparacion = (hora == null) ? 1 : H - hora.H;
        if (comparacion == 0)
            comparacion = M - hora!.M;
        return comparacion;
    }
}

public static class Program
{
    public static void Main()
    {
        Matriz<Hora> m1 = new(
        [
            [ new (0,  0), new (0,  30) ],
            [ new (12, 0), new (12, 30) ],
            [ new (18, 0), new (18, 30) ]
        ]);
        Matriz<Hora> m2 = new(
        [
            [ new (0,  0), new (0,  30) ],
            [ new (12, 0), new (12, 30) ],
            [ new (18, 0), new (18, 30) ]
        ]);
        Console.WriteLine(m1.Equals(m2));
    }
}