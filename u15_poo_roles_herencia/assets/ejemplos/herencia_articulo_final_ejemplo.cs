using System.Text;

namespace Ejemplo;

public class Articulo
{
    public string Id { get; }
    public virtual double Precio { get; private set; }
    public string Nombre { get; private set; }

    public Articulo(
                string id,
                string nombre,
                double precio)
    {
        Id = id;
        Nombre = nombre;
        Precio = precio;
    }

    public override string ToString() => $"""
                Id: {Id}
                Nombre: {Nombre}
                Precio: {Precio:F2}€
                """;
    public override bool Equals(object? obj) => obj is Articulo a
                && a.Id.CompareTo(Id) == 0;
    public override int GetHashCode() => HashCode.Combine(Id, Precio, Nombre);
}

public class ArticuloRebajado : Articulo
{
    public override double Precio => base.Precio - Descuento;
    public virtual ushort PorcentajeRebaja { get; private set; }
    public double PrecioBase => base.Precio;
    private double Descuento => base.Precio * PorcentajeRebaja / 100d;

    public ArticuloRebajado(
                        string id,
                        string nombre,
                        double precio,
                        ushort porcentajeRebaja) : base(id, nombre, precio)
    {
        PorcentajeRebaja = porcentajeRebaja;
    }
    public override string ToString() => $"""
                        Id: {Id}
                        Nombre: {Nombre}
                        Rebaja: {PorcentajeRebaja}%
                        Antes: {base.Precio:F2}€
                        Ahora: {Precio:F2}€
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

    public override string ToString() => $"""
                        {base.ToString}
                        Fecha reacondicionamiento: {FechaReacondicionamiento.ToShortDateString()}
                        Empresa: {Empresa}
                        Descripción: {Descripcion}
                        """;
}

public class ArticuloEnExposicion : ArticuloRebajado
{
    public DateOnly InicioExposicion { get; }
    public ArticuloEnExposicion(
                        string id,
                        string nombre,
                        double precio,
                        DateOnly inicioExposicion) : base(id, nombre, precio, 0)
    {
        InicioExposicion = inicioExposicion;
    }

    public override ushort PorcentajeRebaja => Convert.ToUInt16(Math.Clamp(DiasEnExposicion, 1, 75));

    public int DiasEnExposicion => DateOnly.FromDateTime(DateTime.Now).DayNumber - InicioExposicion.DayNumber;

    public override string ToString() => $"""
                        {base.ToString()}
                        En exposición desde: {InicioExposicion.ToShortDateString()} total {DiasEnExposicion} días
                        """;
}

public class ArticuloDefectuoso : ArticuloRebajado
{
    public string Defecto { get; }
    public ArticuloDefectuoso(
                        string id,
                        string nombre,
                        in double precio,
                        in ushort porcentajeRebaja,
                        string defecto) : base(id, nombre, precio, porcentajeRebaja)
    {
        Defecto = defecto;
    }

    public override string ToString() => $"""
                        {base.ToString()}
                        Defecto: {Defecto}
                        """;
}

public class TicketCompra
{
    private List<Articulo> Articulos { get; } = [];
    public double Total
    {
        get
        {
            double total = 0d;
            foreach (Articulo a in Articulos)
                total += a.Precio;
            return total;
        }
    }

    public TicketCompra Añade(Articulo a)
    {
        Articulos.Add(a);
        return this;
    }
    public override string ToString()
    {
        StringBuilder ticket = new();
        foreach (Articulo a in Articulos)
        {
            string precio = (a is ArticuloRebajado ar)
            ? $"{ar.PrecioBase,8:F2}€ con {ar.PorcentajeRebaja:D2}% {ar.Precio,8:F2}€"
            : $"{a.Precio,8+8+10:F2}€";

            string nota = a switch
            {
                ArticuloReacondicionado are => $"R ({are.Descripcion})",
                ArticuloEnExposicion ae => $"E (desde {ae.InicioExposicion.ToShortDateString()})",
                ArticuloDefectuoso ad => $"D ({ad.Defecto})",
                _ => string.Empty
            };

            ticket.AppendLine($"{a.Nombre,-22} {precio} {nota}");
        }
        ticket.AppendLine($"Total: {Total:F2}€");
        return ticket.ToString();
    }
}

static public class EjemploHerencia
{
    public static void Main()
    {
        TicketCompra ticket = new();
        ticket.Añade(new Articulo(
                        id: "A001",
                        nombre: "Camiseta",
                        precio: 19.99))
        .Añade(new ArticuloRebajado(
                        id: "A002",
                        nombre: "Pantalón",
                        precio: 39.99,
                        porcentajeRebaja: 20))
        .Añade(new ArticuloReacondicionado(
                        id: "A003",
                        nombre: "Zapatillas",
                        precio: 59.99,
                        fechaReacondicionamiento: new DateOnly(2023, 1, 15),
                        empresa: "Reacondicionados S.A.",
                        descripcion: "Sin uso, con caja original"))
        .Añade(new ArticuloEnExposicion(
                        id: "A004",
                        nombre: "Chaqueta",
                        precio: 89.99,
                        inicioExposicion: new DateOnly(2023, 2, 1)))
        .Añade(new ArticuloDefectuoso(
                        id: "A005",
                        nombre: "Gorra",
                        precio: 15.99,
                        porcentajeRebaja: 10,
                        defecto: "Pequeño rasguño"));
        Console.WriteLine(ticket);
    }
}
