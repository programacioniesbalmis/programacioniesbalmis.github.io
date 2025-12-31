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
    // Definimos el tipo.
    public record class EmpleadosPorCiudad(Ciudad Ciudad, IEnumerable<Empleado> Empleados);

    public static IEnumerable<EmpleadosPorCiudad> EmpleadosVentasPorCiudad() =>
    Empleados.DepartamentoDeVentas
             .GroupBy(e => e.Ciudad,
                    (c, g) => new EmpleadosPorCiudad(Ciudad: c, Empleados: g));


    // Al tener un tipo concreto, también podemos modularizar la composición de la salida.
    public static string ATexto(IEnumerable<EmpleadosPorCiudad> empleadosXCiudad)
    {
        StringBuilder salida = new StringBuilder();
        foreach (var eXc in empleadosXCiudad)
        {
            salida.Append($"{eXc.Ciudad}:\n");
            foreach (Empleado e in eXc.Empleados.OrderBy(e => e.Edad))
                salida.Append($"\t{e}\n");
        }
        return salida.ToString();
    }

    static void Main()
    {
        var empleadosXCiudad = EmpleadosVentasPorCiudad();
        Console.WriteLine(empleadosXCiudad);

        // Mapeamos al mismo DTO pero filtrando la propiedad Empleados por edad.
        // Fíjate que al ser inmutable EmpleadosPorCiudadDto, 
        // para no tener que crear un nuevo objeto copiando propiedad a propiedad.
        // Usamos el operador with para crear una nueva instancia 
        // con la propiedad Empleados filtrada.
        var empleadosXCiudadMas40 = empleadosXCiudad.Select(
                                        eXc => eXc with { 
                                            Empleados = eXc.Empleados.Where(e => e.Edad > 40) 
                                        });

        Console.WriteLine(ATexto(empleadosXCiudadMas40)); 

    }
}
