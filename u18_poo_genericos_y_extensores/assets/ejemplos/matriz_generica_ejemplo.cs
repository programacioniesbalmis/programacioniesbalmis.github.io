using System.Diagnostics;

namespace Ejemplo;

public class Matriz<T>
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

public static class Program
{
    public static void Main()
    {
        Matriz<int> m1 = new(
        [
            [1, 2, 3, 4],
            [5, 6, 7, 8],
            [9, 10, 11, 12]
        ]);

        Console.WriteLine("Matriz enteros original:");
        Console.WriteLine(m1);

        Matriz<int> m1t = m1.Traspuesta;
        Console.WriteLine("Matriz enteros traspuesta:");
        Console.WriteLine(m1t);
        Console.WriteLine();

        Matriz<char> m2 = new(
        [
            ['a', 'b', 'c'],
            ['d', 'e', 'f'],
            ['g', 'h', 'i'],
            ['j', 'k', 'l']
        ]);

        Console.WriteLine("Matriz caracteres original:");
        Console.WriteLine(m2);
        Matriz<char> m2t = m2.Traspuesta;
        Console.WriteLine("Matriz caracteres traspuesta:");
        Console.WriteLine(m2t);
    }   
}