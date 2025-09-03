namespace Ejemplo;

public class Articulo
{
    // Propiedade de sólo lectura por ser un Id y público el get por +GetId(): string
    public string Id { get; }
    // Propiedades privadas de modificación y protegidas para acceso
    // y así solo se pueda acceder desde la clase y sus subclases
    protected double Precio { get; private set; }
    protected string Nombre { get; private set; }

    public Articulo(
                string id,
                string nombre,
                double precio)
    {
        Id = id;
        Nombre = nombre;
        Precio = precio;
    }

    public string ATexto() => $"""
                Id: {Id}
                Nombre: {Nombre}
                Precio: {Precio:F2}€
                """;
}

public class ArticuloRebajado : Articulo
{
    public ushort PorcentajeRebaja { get; private set; }
    private double Descuento => base.Precio * PorcentajeRebaja / 100d;
    public double PrecioRebajado => base.Precio - Descuento;
    public double PrecioBase => base.Precio;

    public ArticuloRebajado(
                        string id,
                        string nombre,
                        double precio,
                        ushort porcentajeRebaja) 
                        // Llamada al constructor de la clase base encargado
                        // de 'construir' la parte de Articulo del objeto
                        : base(id, nombre, precio)
    {
        PorcentajeRebaja = porcentajeRebaja;
    }
    public string ATextoRebajado() => $"""
                        Id: {base.Id}
                        Nombre: {base.Nombre}
                        Rebaja: {PorcentajeRebaja}%
                        Antes: {base.Precio:F2}€
                        Ahora: {PrecioRebajado:F2}€
                        """;
}

public class Ejemplo
{
    public static void Main()
    {
        Articulo a = new (
            id: "A001",
            nombre: "Polo Ralph Lauren",
            precio: 75d);

        Console.WriteLine(new string('-', 20));
        Console.WriteLine(a.ATexto());

        ArticuloRebajado ar = new (
            id: "A002",
            nombre: "Polo Fred Perry",
            precio: 70d,
            porcentajeRebaja: 15);

        Console.WriteLine(new string('-', 20));
        Console.WriteLine(ar.ATexto());
        Console.WriteLine();
        Console.WriteLine(ar.ATextoRebajado());
        Console.WriteLine(new string('-', 20));
    }
}