
namespace Ejemplo;

public class Sesion : IDisposable
{
    public string Usuario { get; private set; } = string.Empty;
    private string Clave { get; set; } = string.Empty;
    public bool Iniciada { get; private set; } = false;
    public void Login(string usuario, string clave)
    {
        if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
            throw new ArgumentException("Usuario y clave no pueden estar vacíos.");

        if (Iniciada)
        {
            Console.WriteLine($"Ya hay una sesión iniciada. Cerrando la sesión actual de {Usuario} en servidor...");
            Dispose();
        }

        Usuario = usuario;
        Clave = clave;
        Iniciada = true;

        Console.WriteLine($"Usuario {Usuario} autenticado exitosamente. Registrando sesión en servidor...");
    }

    public void Logout()
    {
        if (!Iniciada)
            throw new InvalidOperationException("No hay una sesión iniciada para cerrar.");
        Console.WriteLine($"Usuario {Usuario} ha cerrado sesión.");
        Dispose();
    }

    public void Dispose()
    {
        if (Iniciada)
        {
            Console.WriteLine("Liberando recursos de la sesión en el servidor...");
            Usuario = string.Empty;
            Clave = string.Empty;
            Iniciada = false;
        }
    }
}

public static class RecursosProtegidos
{
    /// <summary>
    /// Accede a un recurso 1 protegido que requiere sesión iniciada.
    /// </summary>
    /// <param name="sesion"></param>
    /// <exception cref="UnauthorizedAccessException"></exception>
    /// <remarks>Si no hay una sesión iniciada, lanza UnauthorizedAccessException.</remarks>
    public static void AccederRecurso1(Sesion sesion)
    {
        if (!sesion.Iniciada)
            throw new UnauthorizedAccessException("Acceso denegado a recurso protegido 1. No hay una sesión iniciada.");
        Console.WriteLine("Acceso correcto a Recurso Protegido 1...");
    }
    public static void AccederRecurso2(Sesion sesion)
    {
        if (!sesion.Iniciada)
            throw new UnauthorizedAccessException("Acceso denegado a recurso protegido 2. No hay una sesión iniciada.");

        Console.WriteLine("Acceso correcto a Recurso Protegido 2 se producirá un error inesperado ...");
        throw new InvalidOperationException("Se produjo un error inesperado al acceder al Recurso Protegido 2.");
    }
}


public class Program
{
    public static string Menu()
    {
        return """
        1. Iniciar sesión
        2. Acceder a Recurso Protegido 1
        3. Acceder a Recurso Protegido 2
        4. Cerrar sesión
        5. Salir
        """;
    }  

    public static void Main()
    {
        // Iniciamos la sesión fuera del try para que esté accesible
        Sesion sesion = new();
        try
        {
            Console.Clear();
            bool salir = false;
            do
            {
                Console.WriteLine(Menu());
                Console.Write("Seleccione una opción: ");
                string? opcion = Console.ReadLine()!;

                switch (opcion)
                {
                    case "1":
                        Console.Write("Usuario: ");
                        string usuario = Console.ReadLine()!;
                        Console.Write("Clave: ");
                        string clave = Console.ReadLine()!;
                        sesion.Login(usuario, clave);
                        break;

                    case "2":
                        // Debemos ser nosotros quienes verifiquemos si la sesión está iniciada antes de llamar al método
                        // en optro caso, el método lanzaría una excepción por no cumplir la precondición de uso.
                        // Podemos verlo en la documentación del método AccederRecurso1.
                        if (sesion.Iniciada)
                            RecursosProtegidos.AccederRecurso1(sesion);
                        else
                            Console.WriteLine("Debe iniciar sesión antes de acceder al recurso protegido 1.");
                        break;

                    case "3":
                        // Aquí no verificamos si la sesión está iniciada, para demostrar que el método lanza una excepción
                        // si no se cumple la precondición de uso.
                        RecursosProtegidos.AccederRecurso2(sesion);
                        break;

                    case "4":
                        // Cerrar sesión si está iniciada. Como antes, si queremos recuperanos del error,
                        // este es el punto donde debemos hacerlo y no en el método Logout.
                        if (sesion.Iniciada)
                            sesion.Logout();
                        else
                            Console.WriteLine("No hay una sesión iniciada para cerrar.");
                        break;

                    case "5":
                        Console.WriteLine("Saliendo de la aplicación...");
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            }
            while (!salir);
        }
        catch (Exception e)        
        {
            // Gestion de errores centralizada.
            // no se debe usar como control de flujo, sino para capturar errores inesperados.
            Console.WriteLine($"Error inesperado: {e.Message}");
        }
        finally
        {
            // Siempre se ejecuta, haya o no error.
            // Nos aseguramos de liberar los recursos de la sesión.
            sesion.Dispose();
        }
    }
}
