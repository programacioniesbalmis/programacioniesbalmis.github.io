
public class EjemploFileInfo
{
    public static void Main()
    {
        //La siguiente expresión está fuera del alcance de este curso pero por resumir
        // lo que hace diremos que ...
        // Si me estoy ejecutando en Unix, Linux y MacOS X tomo el valor de la
        // carpeta de usuario de la variable HOME, en caso contrário de la
        // ubicación que indique Windows en su variable de ambiente.
        string home = Environment.OSVersion.Platform == PlatformID.Unix
                      ? Environment.GetEnvironmentVariable("HOME") ?? "."
                      : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
        // Compruebo si existe la ruta devuelta por el entorno.
        if (Directory.Exists(home))
        {
            // Me sitúo en el directorio home.
            Directory.SetCurrentDirectory(home);
            // Instancio el objeto de tipo DirectoryInfo con la información de la carpeta.
            DirectoryInfo infoCarpeta = new DirectoryInfo(home);
            // Obtengo información de todos los objetos que hay en dicha carpeta 
            // ya sean otras capetas o archivos. 
            // Para eso llamo a GetFileSystemInfos() que me
            // devuelve un array de FileSystemInfo con dicha información.
            FileSystemInfo[] infosEnFS = infoCarpeta.GetFileSystemInfos();

            // Si hubiera querido ver si hay otras carpetas hubiera hecho...
            // -> DirectoryInfo[] infoCarpetas = infoCarpeta.GetDirectories();
            // De forma análoga si hubiera querido coger solo información de archivos...
            // -> FileInfo[] infoArchivos = infoCarpeta.GetFiles();
            // Recorro el array.
            foreach (FileSystemInfo infoEnFS in infosEnFS)
            {
                // Compruebo si en la máscara el item que estoy recorriendo 
                // me indica que es una carpeta.
                bool esCarpeta = (infoEnFS.Attributes & FileAttributes.Directory)
                                  == FileAttributes.Directory;
                // Muestro el nombre completo indicando si es un archivo o una carpeta.
                string info = $"{(esCarpeta ? "Carpeta" : "Archivo")}->{infoEnFS.FullName}";
                Console.WriteLine(info);
            }
        }
        else
            Console.WriteLine("No se ha podido encontrar la carpeta home.");
    }
}
