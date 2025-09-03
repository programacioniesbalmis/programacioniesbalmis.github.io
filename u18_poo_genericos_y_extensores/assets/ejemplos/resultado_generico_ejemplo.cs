public abstract record class Resultado<T, E>
{
    public record Exito(T Value) : Resultado<T, E>
    {
        public override string ToString() => $"{Value}";
    }
    public record Fallo(E Error) : Resultado<T, E>
    {
        public override string ToString() => $"Fallo: {Error}";
    }
}

public static class Calculadora
{
    public static Resultado<int, string> Divide(int dividendo, int divisor) =>
        divisor == 0
        ? new Resultado<int, string>.Fallo("No se puede dividir por cero.")
        : new Resultado<int, string>.Exito(dividendo / divisor);

    public static Resultado<double, Exception> Divide(double dividendo, double divisor) =>
        divisor < 1e-5
        ? new Resultado<double, Exception>.Fallo(new DivideByZeroException())
        : new Resultado<double, Exception>.Exito(double.Round(dividendo / divisor, 2));
}

class Program
{
    static void Main()
    {
        Resultado<int, string> resultado1 = Calculadora.Divide(10, 0);
        Console.WriteLine(resultado1);

        Resultado<int, string> resultado2 = Calculadora.Divide(10, 3);
        Console.WriteLine(resultado2);

        Console.WriteLine();

        Resultado<double, Exception> resultado3 = Calculadora.Divide(10d, 0d);
        Console.WriteLine(resultado3);

        Resultado<double, Exception> resultado4 = Calculadora.Divide(10d, 3d);
        Console.WriteLine(resultado4);
    }
}