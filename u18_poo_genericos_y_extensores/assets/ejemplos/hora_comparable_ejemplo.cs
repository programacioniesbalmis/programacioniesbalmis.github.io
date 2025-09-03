namespace Ejemplo;

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
        List<Hora> horas = 
        [
            new(9, 55), new(10, 50), new(8, 30), new(7, 15)
        ];
        Hora[] aHoras = [.. horas];

        horas.Sort();
        Console.WriteLine(string.Join(", ", horas));

        Array.Sort<Hora>(aHoras);
        Console.WriteLine(string.Join<Hora>(", ", aHoras));
    }
}