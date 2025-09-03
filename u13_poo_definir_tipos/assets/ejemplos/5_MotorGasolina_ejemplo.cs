using System.Diagnostics;

namespace Ejemplo;

public class MotorGasolina
{
    public int Cilindrada { get; }
    public int Cilindros { get; }
    private bool _encendido;
    private int _revoluciones;

    public bool Encendido
    {
        get => _encendido;
        private set
        {
            Debug.Assert(
                condition: value != _encendido,
                message: "El motor ya está en el estado solicitado.");
            _encendido = value;
        }
    }
    public int Revoluciones
    {
        get => _revoluciones;
        private set
        {
            int maxRevoluciones = Cilindrada * Cilindros * 10;
            Debug.Assert(
                condition: value >= 0 && value <= maxRevoluciones,
                message: $"Las revoluciones deben estar entre 0 y {maxRevoluciones}.");
            _revoluciones = value;
        }
    }

    public double Consumo => Revoluciones / 500.0 * (Cilindrada / 1000.0) * Cilindros * 0.2;

    public string Estado => $"""
                   Cilindrada: {Cilindrada} cc
                   Cilindros: {Cilindros}
                   Revoluciones: {Revoluciones} RPM
                   Encendido: {Encendido}
                   Consumo: {Consumo:F2} L/100Km
                   """;

    public MotorGasolina(int cilindrada, int cilindros)
    {
        Cilindrada = cilindrada;
        Cilindros = cilindros;
        _revoluciones = 0;
        _encendido = false;
    }

    public void Enciende()
    {
        Encendido = true;
        Revoluciones = 800;
    }

    public void Apaga()
    {
        Encendido = false;
        Revoluciones = 0;
    }

    public void DarGas(int revoluciones)
    {
        Debug.Assert(
            condition: Encendido,
            message: "El motor debe estar encendido para dar Gas.");
        Revoluciones += revoluciones;
    }

    public void QuitarGas(int revoluciones)
    {
        Debug.Assert(
            condition: Encendido,
            message: "El motor debe estar encendido para quitar Gas.");
        Revoluciones = Math.Max(800, Revoluciones - revoluciones);
    }
}

public class Program
{
    public static void Main()
    {
        MotorGasolina motor = new (cilindrada: 1600, cilindros: 4);

        Console.WriteLine("Estado inicial del motor:");
        Console.WriteLine(motor.Estado);

        Console.WriteLine("\nEncendiendo el motor...");
        motor.Enciende();
        Console.WriteLine(motor.Estado);

        Console.WriteLine("\nAumentando revoluciones...");
        motor.DarGas(2000);
        Console.WriteLine(motor.Estado);

        Console.WriteLine("\nReduciendo revoluciones...");
        motor.QuitarGas(1000);
        Console.WriteLine(motor.Estado);

        Console.WriteLine("\nApagando el motor...");
        motor.Apaga();
        Console.WriteLine(motor.Estado);
    }
}