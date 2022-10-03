internal class Program
{
    private static void Main()
    {
        char s = Path.DirectorySeparatorChar;
        string ruta = $".{s}datos{s}datos.txt";

        // Si no existe el directorio datos en la ruta relativa actual lo crearé.
        if (Directory.Exists(Path.GetDirectoryName(ruta)) == false)
            Directory.CreateDirectory("datos");

        // Creo el fichero datos.txt vacío. Más adelante en el tema
        // veremos que llamar al Close() es importante para que no
        // se quede abierto.
        File.Create(ruta).Close();

        // Me sitúo en el directorio datos.
        Directory.SetCurrentDirectory(Path.GetDirectoryName(ruta) ?? $"{s}");

        Console.Write("El fichero " + ruta + " ");

        // Compruebo si se ha creado el fichero correctamente viendo si 
        // existe o no en el directorio datos (donde me acabo de situar).
        // Mostraré si existe o no por pantalla.
        Console.WriteLine(File.Exists(Path.GetFileName(ruta)) ? "existe" : "no existe");
    }
}