namespace Ejemplo;

public class Articulo
{
    public string Id { get; }
    public virtual double Precio { get; private set; }
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

    public virtual string ATexto() => $"""
                Id: {Id}
                Nombre: {Nombre}
                Precio: {Precio:F2}€
                """;
}

public class ArticuloRebajado : Articulo
{
    public ushort PorcentajeRebaja { get; private set; }
    private double Descuento => base.Precio * PorcentajeRebaja / 100d;
    // Añado el modificador override para indicar que estoy invalidando la propiedad Precio.
    public override double Precio => base.Precio - Descuento;
    public double PrecioBase => base.Precio;

    public ArticuloRebajado(
                        string id,
                        string nombre,
                        double precio,
                        ushort porcentajeRebaja) 
                        : base(id, nombre, precio)
    {
        PorcentajeRebaja = porcentajeRebaja;
    }
    // Añado el modificador override para indicar que estoy invalidando la el método ATexto().
    public override string ATexto() => $"""
                        Id: {base.Id}
                        Nombre: {base.Nombre}
                        Rebaja: {PorcentajeRebaja}%
                        Antes: {base.Precio:F2}€
                        Ahora: {this.Precio:F2}€
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
        Console.WriteLine(ar.ATexto());
        Console.WriteLine(new string('-', 20));
    }
}