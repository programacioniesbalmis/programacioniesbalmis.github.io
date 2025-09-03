
using System.Diagnostics;

namespace Ejemplo;

public class Escritor
{
    private int _publicaciones;
    public string Nombre { get; }
    public int Nacimiento { get; }
    public int Edad => DateTime.Now.Year - Nacimiento;

    public int Publicaciones
    {
        get => _publicaciones;
        private set
        {
            Debug.Assert(value >= 0, "El número de publicaciones no puede ser negativo.");
            _publicaciones = value;
        }
    }
    public Escritor(string nombre, int nacimiento)
    {
        Nombre = nombre;            // Las propiedades autoimplementadas 
        Nacimiento = nacimiento;    // en el constructor será el único sitio donde se
        Publicaciones = 0;          // puedan asignar.
    }

    public string ATexto() => $"""
                    Nombre: {Nombre}
                    Nacimiento: {Nacimiento}
                    Publicaciones: {Publicaciones}
                    """;

    public Libro Escribe(string titulo)
    {
        Range r = 400..800;
        Publicaciones++;
        return new (
            titulo: titulo, 
            año: DateTime.Now.Year, 
            paginas: new Random().Next(r.Start.Value, r.End.Value + 1));
    }
}

