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
    public virtual ushort PorcentajeRebaja { get; private set; }
    private double Descuento => base.Precio * PorcentajeRebaja / 100d;
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
    public override string ATexto() => $"""
                        Id: {base.Id}
                        Nombre: {base.Nombre}
                        Rebaja: {PorcentajeRebaja}%
                        Antes: {base.Precio:F2}€
                        Ahora: {this.Precio:F2}€
                        """;
}

public class ArticuloReacondicionado : Articulo
{
    public DateOnly FechaReacondicionamiento { get; }
    public string Empresa { get; }
    public string Descripcion { get; }

    public ArticuloReacondicionado(
                        string id,
                        string nombre,
                        double precio,
                        DateOnly fechaReacondicionamiento,
                        string empresa,
                        string descripcion) : base(id, nombre, precio)
    {
        FechaReacondicionamiento = fechaReacondicionamiento;
        Empresa = empresa;
        Descripcion = descripcion;
    }

    public override string ATexto() => $"""
                        {base.ATexto()}
                        Fecha reacondicionamiento: {FechaReacondicionamiento.ToShortDateString()}
                        Empresa: {Empresa}
                        Descripción: {Descripcion}
                        """;
}

public class ArticuloEnExposicion : ArticuloRebajado
{
    public DateOnly InicioExposicion { get; }

    // En un principio el descuento es 0 y lo calculeramos dinámicamente.
    public ArticuloEnExposicion(
                        string id,
                        string nombre,
                        double precio,
                        DateOnly inicioExposicion) : base(id, nombre, precio, 0)
    {
        InicioExposicion = inicioExposicion;
    }

    // Invalidamo la obtención porcentaje para calcularlo en función de los días en exposición.
    public override ushort PorcentajeRebaja => Convert.ToUInt16(Math.Clamp(DiasEnExposicion, 1, 75));

    // Los días en esposición se calculan en el momento actual, desde el incio de la exposición.
    public int DiasEnExposicion => DateOnly.FromDateTime(DateTime.Now).DayNumber - InicioExposicion.DayNumber;

    // Invalidamos el método ATexto() de Articulo y ArticuloRebajado para que añada la nueva información.
    public override string ATexto() => $"""
                        {base.ATexto()}
                        En exposición desde: {InicioExposicion.ToShortDateString()} total {DiasEnExposicion} días
                        """;
}


public class Ejemplo
{
    public static void Main()
    {
        Articulo a = new ArticuloEnExposicion(
            id: "A006-E",
            nombre: "TV Samsung OLED 50''",
            precio: 999d,
            inicioExposicion: DateOnly.FromDateTime(DateTime.Now.AddDays(-10)));

        Console.WriteLine(new string('-', 20));
        Console.WriteLine(a.ATexto());
        Console.WriteLine(new string('-', 20));

    }
}
