
namespace Ejemplo;

public class Escritor
{
    private readonly string _nombre;
    private readonly int _nacimiento;
    private int _publicaciones;

    public string GetNombre()
    {
        return _nombre;
    }
    public int GetNacimiento()
    {
        return _nacimiento;
    }
    public int GetPublicaciones()
    {
        return _publicaciones;
    }
    private void SetPublicaciones(int publicaciones)
    {
        _publicaciones = publicaciones;
    }

    public Escritor(string nombre, int nacimiento)
    {
        _nombre = nombre;
        _nacimiento = nacimiento;
        SetPublicaciones(0);
    }

    public string ATexto() {
        return $"""
        Nombre: {Nombre}
        Nacimiento: {Nacimiento}
        Publicaciones: {Publicaciones}
        """;
    }

    public Libro Escribe(string titulo)
    {
        Range r = 400..800;
        SetPublicaciones(GetPublicaciones() + 1); 
        return new (
            titulo: titulo, 
            año: DateTime.Now.Year, 
            paginas: new Random().Next(r.Start.Value, r.End.Value + 1));
    }
}
