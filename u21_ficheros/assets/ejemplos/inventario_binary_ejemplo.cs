using System.Text;

record class Producto(int Id, string Nombre, double Precio)
{
    // Tamaño fijo en bytes (4 bytes ID + 64 bytes Nombre + 8 bytes Precio)
    public const int RecordSizeBytes = 4 + 64 + 8; 
    public override string ToString() => $"ID: {Id,04:D} {Nombre,-32} {Precio,8:F2} EUR";
}

class Program
{
    const string FILE_PATH = "inventario.dat";

    static void Main()
    {
        EscribirProducto(0, new Producto(101, "Laptop Gaming", 1200.50));
        EscribirProducto(1, new Producto(102, "Mouse Optico", 25.99));
        // Saltamos posiciones a propósito
        EscribirProducto(5, new Producto(105, "Monitor 4K", 450.00)); 

        Console.WriteLine("--- Datos guardados ---");

        var producto1 = LeerProducto(5); // Leer el monitor
        if (producto1 != null)
            Console.WriteLine($"[Registro 5] {producto1}");
        
        var producto2 = LeerProducto(1); // Leer el mouse
        if (producto2 != null)
            Console.WriteLine($"[Registro 1] {producto2}");
    }

    static void EscribirProducto(int indice, Producto producto)
    {
        using FileStream fs = new (FILE_PATH, FileMode.OpenOrCreate, FileAccess.Write);
        using BinaryWriter writer = new (fs, Encoding.UTF8);
        
        // SEEK: Movemos el puntero al lugar exacto del índice
        fs.Seek(indice * Producto.RecordSizeBytes, SeekOrigin.Begin);

        // Aseguramos que el nombre ocupa exactamente 32 caracteres
        // como cada caracter en UTF-8 ocupa 2 bytes, serán 64 bytes en total.
        string nombreFijo = producto.Nombre.PadRight(32)[..32];

        writer.Write(producto.Id); // 4 bytes
        writer.Write(nombreFijo); // 64 bytes
        writer.Write(producto.Precio); // 8 bytes
    }
    
    static Producto? LeerProducto(int indice)
    {
        if (!File.Exists(FILE_PATH)) return null;

        using FileStream fs = new (FILE_PATH, FileMode.Open, FileAccess.Read);
        using BinaryReader reader = new (fs, Encoding.UTF8);
        long posicion = indice * Producto.RecordSizeBytes;

        if (posicion < fs.Length)
        {
            // SEEK: Saltamos directamente al registro deseado sin leer los anteriores
            fs.Seek(posicion, SeekOrigin.Begin);

            int id = reader.ReadInt32();
            string nombre = reader.ReadString().Trim();
            double precio = reader.ReadDouble();

            return new Producto(id, nombre, precio);
        }
        else
        {
            Console.WriteLine($"El registro {indice} no existe.");
            return null;
        }
    }
}