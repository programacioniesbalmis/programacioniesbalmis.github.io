using System.Text;

public enum Ciudad { Elche, Alicante };

public record Empleado(string Nombre, int Edad, Ciudad Ciudad)
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
    static void Main()
    {
        string[] nombres = Empleados.DepartamentoDeVentas
                                    .Where(e => e.Edad > 40)  // Filtramos por edad.
                                    .Select(e => e.Nombre)    // Proyectamos la propiedad Nombre 
                                                              // a una nueva secuencia.
                                    .OrderBy(n => n)          // Ordenamos por nombre.
                                    .Distinct()               // Eliminamos repetidos.
                                    .ToArray();               // Pasamos la secuencia a array.
        Console.WriteLine(string.Join(", ", nombres));

        // ----------------------------------------------------------------------

        var empleadosXCiudad = Empleados.DepartamentoDeVentas
                                .Where(e => e.Edad > 40)
                                .GroupBy(e => e.Ciudad,
                                         (c, g) => new { Ciudad = c, Empleados = g });
        StringBuilder salida = new StringBuilder();
        foreach (var eXc in empleadosXCiudad)
        {
            salida.Append($"{eXc.Ciudad}:\n");
            foreach (Empleado e in eXc.Empleados.OrderBy(e => e.Edad))
                salida.Append($"\t{e}\n");
        }
        Console.WriteLine(salida);
    }
}
