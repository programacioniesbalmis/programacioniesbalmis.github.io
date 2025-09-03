public class Empleado
{
    public string Id { get; }
    public string Nombre { get; }
    public double Sueldo { get; }

    public Empleado(
        string id,
        string nombre,
        double sueldo)
    {
        Id = id;
        Nombre = nombre;
        Sueldo = sueldo;
    }
}

public class IgualdadEmpleadosPorId : IEqualityComparer<Empleado>
{
    public bool Equals(Empleado? x, Empleado? y) => (x, y) switch
    {
        (null, null) => true,
        (null, _) => false,
        (_, null) => false,
        _ => x.Id == y.Id   // Compara por el Id
    };
    public int GetHashCode(Empleado obj) => obj.Id.GetHashCode();
}

public class ComparaEmpleado : IComparer<Empleado>
{
    public int Compare(Empleado? x, Empleado? y) => (x, y) switch
    {
        (null, null) => 0,
        (null, _) => -1,
        (_, null) => 1,
        _ => x.Id.CompareTo(y.Id)
    };
}

public class Program
{
    public static void Main()
    {
        List<Empleado> empleados =
        [
            new(id: "001", nombre: "Juanjo", sueldo: 2000),
            new(id: "002", nombre: "Carmen", sueldo: 2800),
            new(id: "003", nombre: "Xusa", sueldo: 2400)
        ];

        // Búsqueda lineal
        bool encontrado = empleados.Contains(
                                    value: new("002", "Carmen", 2800),
                                    comparer: new IgualdadEmpleadosPorId());

        // Búsqueda binaria
        IComparer<Empleado> comparaEmpleados = new ComparaEmpleado();
        empleados.Sort(comparaEmpleados);
        encontrado = empleados.BinarySearch(
                        item: new Empleado(id: "002", nombre: "Carmen", sueldo: 2800),
                        comparer: comparaEmpleados) >= 0;

    }
}