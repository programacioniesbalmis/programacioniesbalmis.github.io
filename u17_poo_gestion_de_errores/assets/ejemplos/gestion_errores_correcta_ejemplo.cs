public static class Consola
{
    public static double Lee(string etiqueta)
    {
        double valor;
        bool error;

        do
        {
            Console.Write($"{etiqueta}: ");
            string textoEntrada = Console.ReadLine() ?? "";
            error = double.TryParse(textoEntrada, out valor);
            if (error)
                Console.WriteLine($"El valor introducido {textoEntrada}" +
                                  "no es un valor real válido.");
        }
        while (error);

        return valor;
    }
}

public class Pinrcipal
{

    /// <summary>
    /// Divide dos números, lanzando una excepción si el divisor es cero.
    /// </summary>
    /// <param name="numerador"></param>
    /// <param name="divisor"></param>
    /// <returns>El cociente resultado de la división.</returns>
    /// <exception cref="DivideByZeroException"></exception>
    /// <remarks>El divisor se considera cero si es menor que 1e-5.</remarks>
    public static double Divide(double numerador, double divisor)
    {
        if (divisor < 1e-5)
            throw new DivideByZeroException();
        return numerador / divisor;
    }
    public static void Main()
    {
        bool errorDivisionPorCero;
        do
        {
            double numerador = Consola.Lee("Introduce el numerador");
            double divisor = Consola.Lee("Introduce el divisor");
            errorDivisionPorCero = divisor < 1e-5;
            string textoError = errorDivisionPorCero
                                ? $"No se puede dividir por cero.\nIntroduzca de nuevo los valores."
                                : $"La división es {Divide(numerador, divisor)}";
            Console.WriteLine(textoError);
        }
        while (errorDivisionPorCero);
    }
}
