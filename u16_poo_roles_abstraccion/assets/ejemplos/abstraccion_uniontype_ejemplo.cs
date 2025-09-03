namespace EjemploSumType;

public abstract record class Validacion
{
    public record Exito() : Validacion;
    public record Error(string Mensaje) : Validacion;
}

public static class Validador
{
    public static Validacion ValidaEdad(int edad) => edad switch
    {
        >= 18 => new Validacion.Exito(),
        _ => new Validacion.Error(Mensaje: "El usuario debe ser mayor de 18 años.")
    };
    public static Validacion ValidaNombre(string nombre) => string.IsNullOrWhiteSpace(nombre) switch
    {
        false => new Validacion.Exito(),
        true => new Validacion.Error(Mensaje: "El nombre no puede estar vacío.")
    };
}


public record class Acceso
{
    public DateTime FechaHora { get; }
    public string Nombre { get; }
    public int Edad { get; }

    public Validacion Validacion =>
        Validador.ValidaNombre(Nombre) is Validacion.Error errorNombre
        ? errorNombre
        : Validador.ValidaEdad(Edad);

    public Acceso(string nombre, int edad, DateTime? fechaHora = null)
    {
        FechaHora = fechaHora ?? DateTime.Now;
        Nombre = nombre;
        Edad = edad;
    }

    public override string ToString()
    => $"{Nombre} ({Edad} años) el {FechaHora:dd/MM/yyyy} a las {FechaHora:HH:mm}";
}

public static class Ejemplo
{
    public static void Main()
    {
        List<Acceso> accesos =
        [
            new (nombre: "", edad: 30, fechaHora: new DateTime(2026, 3, 22, 10, 35, 0)),
            new (nombre: "Luis", edad: 17, fechaHora: new DateTime(2026, 3, 22, 10, 39, 0)),
            new (nombre: "Marta", edad: 22, fechaHora: new DateTime(2026, 3, 22, 10, 50, 0)),
        ];

        foreach (var acceso in accesos)
        {
            string mensaje = acceso.Validacion switch
            {
                Validacion.Error error => $"Acceso denegado a {acceso}.\nMotivo: {error.Mensaje}\n",
                _ => $"Acceso permitido a {acceso}\n",
            };
            Console.WriteLine(mensaje);
        }
    }
}
