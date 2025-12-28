class Persona
{
    public string Nombre { get; }
    public int Edad { get; private set; }
    public Persona(string nombre, int edad)
    {
        Nombre = nombre;
        Edad = edad;
    }
    public override string ToString() => $"{Nombre} {Edad} años";
}

class Program
{
    private static void Main()
    {
        Dictionary<string, Persona> personas = new()
        {
            ["11224441K"] = new("Pepe", 30),
            ["11335499M"] = new("María", 22),
            ["12345678O"] = new("Juan", 33),
            ["13898743Y"] = new("Sara", 27)
        };

        Console.WriteLine($"Los datos almacenados son:");
        foreach ((string dni, Persona persona) in personas)
            Console.WriteLine($"- {persona} y DNI {dni}");

        Console.WriteLine("Introduce un DNI para borrar:");
        string dniBuscado = Console.ReadLine()!;
        string salida = personas.ContainsKey(dniBuscado)
            ? $"{personas[dniBuscado]}ha sido borrado"
            : $"No se ha encontrado el DNI {dniBuscado}";
        personas.Remove(dniBuscado);
        Console.WriteLine(salida);

        Console.WriteLine($"Los DNIs almacenados son:");
        Console.WriteLine($"- {string.Join("\n -", personas.Keys)}");
    }
}