public record class Persona(string Nombre, int Edad)
{
    // Definición de la clase que implementa el interfaz 
    // con la estrategia de comparación.
    public class ComparaEdad : IComparer<Persona>
    {
        int IComparer<Persona>.Compare(Persona? x, Persona? y) => (x, y) switch
        {
            (null, null) => 0,
            (null, _) => -1,
            (_, null) => 1,
            _ => x.Edad.CompareTo(y.Edad)
        };
    }
}

public class Principal
{
    public static int ComparaEdad(Persona p1, Persona p2) => p1.Edad.CompareTo(p2.Edad);

    public static void Main()
    {
        List<Persona> personas =
         [
            new ("Sonia", 35), new ("Antonio", 55),
            new ("Margarita", 32), new ("Manuel", 50)
         ];

        personas.Sort(new Persona.ComparaEdad());
        Console.WriteLine(string.Join("\n", personas));
        personas.Sort(ComparaEdad);
        Console.WriteLine(string.Join("\n", personas));
    }
}