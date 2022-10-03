using System.Diagnostics;

internal class CasoDeEstudio
{
    private static void Main()
    {
        static void OcultaDirectorio(string ruta)
        {
            string log = $"Ocultando el directorio '{ruta}'";
            // Mensaje para la consola de depuración.
            Debug.WriteLine(log);
            try
            {
                FileSystemInfo d = new DirectoryInfo(ruta);
                // Genero la excepción indicándo lo que estoy haciendo y además le añado
                // como innerException otra instáncia donde indico realmente el error.
                if (!d.Exists)
                    throw new FileNotFoundException(log,
                          new FileNotFoundException($"El directorio '{ruta}' no existe"));
                // Añado con un OR de bit el atributo Hidden (Oculto) a la máscara de 
                // atributos del FileSystemInfo del directorio.
                d.Attributes |= FileAttributes.Hidden;
            }
            catch (UnauthorizedAccessException e)
            {
                throw new UnauthorizedAccessException(log, e);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(log, e);
            }
            catch (PathTooLongException e)
            {
                throw new PathTooLongException(log, e);
            }
        }


        try
        {
            DirectoryInfo d = Directory.CreateDirectory("oculto");
            // Fíjate que Directory.CreateDirectory(..) devuelve un DirectoryInfo con la
            // información del directorio que acabo de crear y que aprovecho para 
            // pasar la información de la ruta completa a OcultaDirectorio(...)
            OcultaDirectorio(d.FullName);
        }
        catch (Exception? e)
        {
            while (e != null)
            {
                Console.WriteLine(e.Message);
                e = e.InnerException;
            }
        }
    }
}