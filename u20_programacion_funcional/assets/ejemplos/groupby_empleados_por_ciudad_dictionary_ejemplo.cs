using System.Text;

public enum Ciudad { Elche, Alicante };

public record class Empleado(string Nombre, int Edad, Ciudad Ciudad)
{
    public override string ToString() => $"{Nombre,-9}{Edad,-3}{Ciudad}";
}

public static class Empleados
{
    public static IEnumerable<Empleado> DepartamentoDeVentas
    {
        get
        {
            yield return new(Nombre: "Lola", Edad: 45, Ciudad: Ciudad.Alicante);
            yield return new(Nombre: "Pedro", Edad: 51, Ciudad: Ciudad.Alicante);
            yield return new(Nombre: "Juana", Edad: 27, Ciudad: Ciudad.Elche);
            yield return new(Nombre: "Marco", Edad: 52, Ciudad: Ciudad.Elche);
            yield return new(Nombre: "Ana", Edad: 52, Ciudad: Ciudad.Elche);    
        }
    }
}


class Program
{
    // En este caso no necesitamos un tipo concreto, podemos usar un diccionario.
    // y mejoramos la funcionalidad anterior para que admita un filtro mediante una HOF.
    public static Dictionary<Ciudad, List<Empleado>>
    EmpleadosVentasPorCiudad(Func<Empleado, bool> filtroEmpleado) =>
    Empleados.DepartamentoDeVentas
              .Where(filtroEmpleado)
              .GroupBy(e => e.Ciudad,
                    (c, g) => new { Ciudad = c, Empleados = g })
              .ToDictionary(eXc => eXc.Ciudad, eXc => new List<Empleado>(eXc.Empleados));


    public static string ATexto(Dictionary<Ciudad, List<Empleado>> empleadosXCiudad)
    {
        StringBuilder salida = new StringBuilder();
        foreach (var (cuidad, empleados) in empleadosXCiudad)
        {
            salida.Append($"{cuidad}:\n");
            foreach (Empleado e in empleados.OrderBy(e => e.Edad))
                salida.Append($"\t{e}\n");
        }
        return salida.ToString();
    }

    static void Main()
    {
        var empleadosVentasPorCiudadDeMasDe40Años = 
        EmpleadosVentasPorCiudad(e => e.Edad > 40);
        Console.WriteLine(ATexto(empleadosVentasPorCiudadDeMasDe40Años));
    }
}
