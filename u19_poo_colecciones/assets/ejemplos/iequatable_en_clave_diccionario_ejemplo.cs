class Persona : IEquatable<Persona>
{
    public string Nombre { get; }
    public int Edad { get; private set; }
    public Persona(string nombre, int edad)
    {
        Nombre = nombre;
        Edad = edad;
    }
    public override string ToString() => $"{Nombre} {Edad} años";

    // Invalidamos GetHashCode() y una forma simple es usar la función
    // HashCode.Combine(...) para generar el hashcode a partir de los parámetros.
    public override int GetHashCode() => HashCode.Combine(Nombre, Edad);

    // Implementamos el interfaz, que nos obliga a implementar Equals y podemos 
    // comparar fácilmente dos objetos, viendo si tienen el mismo Hash o no.
    public bool Equals(Persona? o) => o != null && Nombre == o.Nombre && Edad == o.Edad;
}

class Program
{
    private static void Main()
    {
        // Definimos el diccionario donde la clave es una persona y
        // el valor una lista de mascotas.
        Dictionary<Persona, List<string>> mascotasXPersona = [];

        // Creamos un objeto persona pepe y para ese objeto  
        // añadimos una lista vacía de mascotas.
        Persona pepe = new("Pepe", 30);
        mascotasXPersona.Add(pepe, []);

        // Usamos la misma referencia al objeto pepe para acceder
        // a su lista de mascotas y añadir dos nombres.
        mascotasXPersona[pepe].Add("Snowball");
        mascotasXPersona[pepe].Add("Velvet");

        // Creamos un objeto persona de nombre María del que no nos guardamos la 
        // referencia y añadimos una lista inicializada en la definición con dos mascotas.
        mascotasXPersona.Add(new("María", 22), ["Simba", "Bella"]);

        // Añadimos una tercera mascota a María, pero volvemos a instanciar otro objeto
        // Persona para María porque no nos guardamos la referencia como con pepe.
        // No debería ser problema porque ambos deberían generar el mismo Hash y además
        // sabemos comparar objetos persona con Equal.
        mascotasXPersona[new("María", 22)].Add("Lucy");
        
        // Mostramos la lista de mascotas por persona.
        foreach (Persona p in mascotasXPersona.Keys)
            Console.WriteLine($"{p}: {string.Join(", ", mascotasXPersona[p])}");
    }
}