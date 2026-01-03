using System.Text;

public class EjemploFileInfo
{
    public static string ObtenInformacion(string rutaAFichero)
    {
        // Obtenemos la información del fichero y 
        // hacemos una sustitución a la superclase.
        FileSystemInfo f = new FileInfo(rutaAFichero);

        StringBuilder informacion = new ();

        // Añadimos información del fichero.
        // Fíjate que f.Attributes muestra todos 
        // los valores dele enum añadidos a la máscara.
        if (f.Exists)
            informacion.Append($"Nombre completo: {f.FullName}\n")
                       .Append($"Nombre : {f.Name}\n")
                       .Append($"Extensión : {f.Extension}\n")
                       .Append($"Fecha creación: {f.CreationTime}\n")
                       .Append($"Fecha último acceso: {f.LastAccessTime}\n")
                       .Append($"Fecha última modificación: {f.LastWriteTime}\n")
                       .Append($"Atributos: {f.Attributes}\n");
        else 
            informacion.Append("Archivo no encontrado");

        return informacion.ToString();
    }
    public static void Main()
    {
        Console.WriteLine(ObtenInformacion(@"C:\Windows\write.exe"));
    }
}
