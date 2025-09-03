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

    public virtual string ATexto() => $"""
                Id: {Id}
                Nombre: {Nombre}
                Precio: {Precio:F2}€
                """;
    public virtual string ATextoLineaTicket() => $"""
                {Nombre,-20} {Precio,26:F2}€
                """;
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
    public override string ATexto() => $"""
                        Id: {Id}
                        Nombre: {Nombre}
                        Rebaja: {PorcentajeRebaja}%
                        Antes: {base.Precio:F2}€
                        Ahora: {Precio:F2}€
                        """;
    public override string ATextoLineaTicket() => $"""
                        {Nombre,-20} {PrecioBase,8:F2}€ con {PorcentajeRebaja:D2}% {Precio,8:F2}€
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
                        {base.ATexto}
                        Fecha reacondicionamiento: {FechaReacondicionamiento.ToShortDateString()}
                        Empresa: {Empresa}
                        Descripción: {Descripcion}
                        """;

    public override string ATextoLineaTicket() => $"""
                        {base.ATextoLineaTicket()} R ({Descripcion})
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

    public override string ATexto() => $"""
                        {base.ATexto()}
                        En exposición desde: {InicioExposicion.ToShortDateString()} total {DiasEnExposicion} días
                        """;

    public override string ATextoLineaTicket() => $"""
                        {base.ATextoLineaTicket()} E (desde {InicioExposicion.ToShortDateString()})
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

    public override string ATexto() => $"""
                        {base.ATexto()}
                        Defecto: {Defecto}
                        """;
    public override string ATextoLineaTicket() => $"""
                        {base.ATextoLineaTicket()} D ({Defecto})
                        """;                        
}

public class TicketCompra
{
    // Puesto que no definimos constructor y dejamos el de por defecto,
    // inicializamos la lista de artículos directamente.
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
    // Definimos un interfaz fluido como los que utiliza la clase StringBuilder.
    public TicketCompra Añade(Articulo a)
    {
        Articulos.Add(a);
        return this;
    }
    public string ATexto()
    {
        StringBuilder ticket = new();
        foreach (Articulo a in Articulos)
        {
            ticket.AppendLine(a.ATextoLineaTicket());
        }
        ticket.AppendLine(new string('-', 70))
              .AppendLine($"Total: {Total,41:F2}€");
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
                        fechaReacondicionamiento: new(2023, 1, 15),
                        empresa: "Reacondicionados S.A.",
                        descripcion: "Con caja original"))
        .Añade(new ArticuloEnExposicion(
                        id: "A004",
                        nombre: "Chaqueta",
                        precio: 89.99,
                        inicioExposicion: new(2025, 2, 1)))
        .Añade(new ArticuloDefectuoso(
                        id: "A005",
                        nombre: "Gorra",
                        precio: 15.99,
                        porcentajeRebaja: 10,
                        defecto: "Pequeño rasguño"));
        Console.WriteLine(ticket.ATexto());
    }
}
