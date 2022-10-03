using System;
using System.Text;

namespace EjemploHerencia
{
    class Articulo
    {
        private double precio;
        private string nombre;

        public Articulo(
                    string nombre,
                    double precio)
        {
            this.nombre = nombre;
            this.precio = precio;
        }
        public string GetNombre()
        {
            return nombre;
        }
        public virtual double GetPrecio()
        {
            return precio;
        }
        public override string ToString()
        {
            return $"Nombre: {nombre}\nPrecio: {precio:F2}€";
        }
        public override bool Equals(object? obj)
        {
            return obj is Articulo a
                           && a.nombre.CompareTo(nombre) == 0
                           && a.precio - precio < 1e-5;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(precio, nombre);
        }
    }

    class ArticuloRebajado : Articulo
    {
        private ushort porcentajeRebaja;

        public ArticuloRebajado(
                    string nombre, in double precio, in ushort porcentajeRebaja) : base(nombre, precio)
        {
            this.porcentajeRebaja = porcentajeRebaja;
        }
        protected virtual ushort GetPorcentajeRebaja()
        {
            return porcentajeRebaja;
        }
        private double GetDescuento()
        {
            return base.GetPrecio() * GetPorcentajeRebaja() / 100d;
        }
        public override double GetPrecio()
        {
            return base.GetPrecio() - GetDescuento();
        }

        public override string ToString()
        {
            return $"Nombre: {GetNombre()}\nRebaja: {GetPorcentajeRebaja()}%\n" +
                   $"Antes: {base.GetPrecio():F2}€\nAhora: {GetPrecio():F2}€";
        }
        public override bool Equals(object? obj)
        {
            return obj is ArticuloRebajado a
                           && base.Equals((Articulo)obj)
                           && a.porcentajeRebaja == porcentajeRebaja;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), porcentajeRebaja);
        }
    }

    class ArticuloReacondicionado : Articulo
    {
        private readonly DateTime fechaReacondicionamiento;
        private readonly string empresa;
        private readonly string descripcion;

        public ArticuloReacondicionado(
                    string nombre,
                    double precio,
                    DateTime fechaReacondicionamiento,
                    string empresa,
                    string descripcion) : base(nombre, precio)
        {
            this.fechaReacondicionamiento = fechaReacondicionamiento;
            this.empresa = empresa;
            this.descripcion = descripcion;
        }

        public string GetEmpresa()
        {
            return empresa;
        }
        public override string ToString()
        {
            return base.ToString() +
                  $"\nFecha reacondicionamiento: {fechaReacondicionamiento.ToShortDateString()}\n" +
                  $"Empresa: {GetEmpresa()}\n" +
                  $"Descripción: {descripcion}";
        }
    }

    class ArticuloEnExposicion : ArticuloRebajado
    {
        private readonly DateTime inicioExposicion;

        public ArticuloEnExposicion(string nombre, in double precio, in DateTime inicioExposicion) : base(nombre, precio, 0)
        {
            this.inicioExposicion = inicioExposicion;
        }

        protected override ushort GetPorcentajeRebaja()
        {
            return Convert.ToUInt16(Math.Clamp(GetDiasEnExposicion(), 1, 75));
        }
        public int GetDiasEnExposicion()
        {
            return (DateTime.Now - inicioExposicion).Days;
        }

        public override string ToString()
        {
            return base.ToString() +
                $"\nEn exposición desde: {inicioExposicion.ToShortDateString()} total {GetDiasEnExposicion()} días";
        }
    }

    class ArticuloDefectuoso : ArticuloRebajado
    {
        private readonly string defecto;

        public ArticuloDefectuoso(string nombre, in double precio, 
                                  in ushort porcentajeRebaja, string defecto) 
                                  : base(nombre, precio, porcentajeRebaja)
        {
            this.defecto = defecto;
        }
        public string GetDefecto()
        {
            return defecto;
        }
        public override string ToString()
        {
            return base.ToString() +
                $"\nDefecto: {defecto}";
        }
    }

    class TicketCompra
    {
        private Articulo[] articulos;
        public TicketCompra(Articulo a)
        {
            articulos = new Articulo[] { a };
        }
        public double GetTotal()
        {
            double total = 0d;
            foreach (Articulo a in articulos)
                total += a.GetPrecio();
            return total;
        }
        public TicketCompra Añade(Articulo a)
        {
            Array.Resize(ref articulos, articulos.Length + 1);
            articulos[^1] = a;
            return this;
        }
        public override string ToString()
        {
            StringBuilder ticket = new StringBuilder();
            foreach (Articulo a in articulos)
                ticket.Append($"{a.GetNombre(),-22} {a.GetPrecio(),10:F2}€\n");
            ticket.Append($"{"Toral:",22} {GetTotal(),10:F2}€\n");
            return ticket.ToString();
        }
    }

    static class EjemploHerencia
    {
        static void Main()
        {
			Articulo arc1 = new ArticuloRebajado("Honor Band 5", 35d, 15);
            Console.WriteLine(arc1.Equals(new ArticuloRebajado("Honor Band 4", 30d, 20))); // False
            Console.WriteLine(arc1.Equals(new ArticuloRebajado("Honor Band 5", 35d, 15))); // True
            Console.WriteLine(arc1.Equals(new ArticuloRebajado("Honor Band 5", 35d, 10))); // False
            Console.WriteLine(arc1.Equals(new Articulo("Honor Band 5", 35d)));             // False
       }
    }
}