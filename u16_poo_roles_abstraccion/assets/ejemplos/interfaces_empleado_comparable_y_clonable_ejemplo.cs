using System.Diagnostics;

namespace EjemploInterfaces;

public class Empleado : IComparable, ICloneable
{
    private double _sueldo;
    public double Sueldo
    {
        get => _sueldo;
        set
        {
            Debug.Assert(
                condition: value >= 1200D && value <= 3000D,
                message: "El sueldo debe estar entre 1200 y 3000 euros");
            _sueldo = value;
        }
    }
    public string Nombre { get; }

    public Empleado(string nombre, double sueldo)
    {
        Nombre = nombre;
        Sueldo = sueldo;
    }

    public int CompareTo(object? obj) => Sueldo.CompareTo((obj as Empleado)!.Sueldo);

    public object Clone() => new Empleado(Nombre, Sueldo);

    public override string ToString()
    => $"Nombre: {Nombre,-8}Sueldo: {Sueldo:F0}";

}

static class Programa
{

    static void Main()
    {
        Empleado e1 = new (nombre: "Juanjo", sueldo: 2000);
        Empleado e2 = new (nombre: "Carmen", sueldo: 2800);
        Empleado e3 = new (nombre: "Xusa", sueldo: 2400);

        // Aunque sabemos que e1 es ICloneable, podemos siempre
        // preguntar a un objeto si implementa un interfaz
        // para poder llamar a sus métodos en esta caso Clone()
        // Como clone devuelve un object, debemos hacer un downcasting.
        Empleado e4 = e1 is ICloneable
                        ? (e1.Clone() as Empleado)! 
                        : e1;
        // Modificamos el sueldo de e4 y no debería afectar a e1.
        e4.Sueldo = 2900;

        Empleado[] empleados = [e1, e2, e3, e4];

        // Puesto que Empleado es IComparable
        // podemos usar Array.Sort() para ordenar el array por sueldo.
        // Si no lo fuera, se produciría un error al ejecutar.
        Array.Sort(empleados);
        Console.WriteLine(string.Join<Empleado>("\n", empleados));
    }
}
