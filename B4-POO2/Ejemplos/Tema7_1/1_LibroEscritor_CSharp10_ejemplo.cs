using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Ejemplo
{
    class Isbn13
    {
        private static readonly int[] PREFIJOS = new int[] { 978, 979 };
        private const int MAX_LONGITUD_GRUPO = 5;
        private const int MAX_LONGITUD_TITULAR = 7;
        private const int MAX_LONGITUD_PUBLICACION = 6;
        private const int LONGITUD_ISBN = 13;
        private readonly string prefijo;
        private readonly string grupoDeRegistro;
        private readonly string titular;
        private readonly string publicacion;

        public Isbn13(in int prefijo, in int grupoDeRegistro, in int titular, in int publicacion)
        {
            this.prefijo = prefijo.ToString();
            if (Array.IndexOf(PREFIJOS, prefijo) < 0)
                throw new ArgumentException($"El prefijo {prefijo} no es válido.");

            this.grupoDeRegistro = grupoDeRegistro.ToString();
            if (this.grupoDeRegistro.Length > MAX_LONGITUD_PUBLICACION)
               throw new ArgumentException($"El grupo de registro {grupoDeRegistro} es demasiado largo.");

            this.titular = titular.ToString();
            if (this.titular.Length > MAX_LONGITUD_PUBLICACION)
               throw new ArgumentException($"El titular {titular} es demasiado largo.");

            this.publicacion = publicacion.ToString();
            if (this.publicacion.Length > MAX_LONGITUD_PUBLICACION)
               throw new ArgumentException($"La publicacion {publicacion} es demasiado larga.");

            string isbn = string.Join("", prefijo, grupoDeRegistro, titular, publicacion);
            int excesoLongitud = isbn.Length - (LONGITUD_ISBN - 1);

            if (excesoLongitud < 0)
               this.publicacion = this.publicacion.PadLeft(Math.Abs(excesoLongitud) + this.publicacion.Length, '0');

            if (excesoLongitud > 0)
                throw new ArgumentException($"El ISBN {isbn} esn demasiado largo.");
        }

        public Isbn13(string isbn13)
        {
            string s = "[ -]?";
            string erPrefijo = $"(?<prefijo>{string.Join("|", PREFIJOS)})";
            string erGrupoDeRegistro = @"(?<grupoDeRegistro>\d{1," + MAX_LONGITUD_GRUPO + "})";
            string erTitular = @"(?<titular>\d{1," + MAX_LONGITUD_TITULAR + "})";
            string erPublicacion = @"(?<publicacion>\d{1," + MAX_LONGITUD_PUBLICACION + "})";
            string erDC = @"(?<dc>\d)";

            Match m = Regex.Match(isbn13, $"^({erPrefijo}{s}{erGrupoDeRegistro}{s}{erTitular}{s}{erPublicacion}{s}{erDC})$");
            if (!m.Success)
                throw new ArgumentException($"{isbn13} no es un valor válido para un ISBN");

            prefijo = m.Groups["prefijo"].Value;
            grupoDeRegistro = m.Groups["grupoDeRegistro"].Value;
            titular = m.Groups["titular"].Value;
            publicacion = m.Groups["publicacion"].Value;
            int dc = int.Parse(m.Groups["dc"].Value);

            if (ATexto("").Length != LONGITUD_ISBN)
                throw new ArgumentException($"El dígito de control para {isbn13} debería ser un EAN13");

            int dcCorrecto = DigitoDeControl();
            if (dc != dcCorrecto)
                throw new ArgumentException($"El dígito de control para {isbn13} debería ser {dcCorrecto} en lugar de {dc}");
        }

        public Isbn13(Isbn13 isbn)
        {
            prefijo = isbn.prefijo;
            grupoDeRegistro = isbn.grupoDeRegistro;
            titular = isbn.titular;
            publicacion = isbn.publicacion;
        }

        public int GetPrefijo()
        {
            return int.Parse(prefijo);
        }
        public int GetGrupoDeRegistro()
        {
            return int.Parse(grupoDeRegistro);
        }
        public int GetTitular()
        {
            return int.Parse(titular);
        }
        public int GetPublicacion()
        {
            return int.Parse(publicacion);
        }

        public int DigitoDeControl()
        {
            string isbn = string.Join("", prefijo, grupoDeRegistro, titular, publicacion);
            double suma = 0;
            for (int i = 0; i < isbn.Length; i++)
                suma += ((i % 2 == 0) ? 1 : 3) * int.Parse(isbn[i].ToString());
            double resto = suma % 10;
            return resto == 0 ? 0 : Convert.ToInt32(10 - resto);
        }
        public string ATexto(string separador)
        {
            return string.Join(separador, prefijo, grupoDeRegistro, titular, publicacion, DigitoDeControl().ToString());
        }
    }

    class Libro
    {
        // <atributos>
        private readonly string titulo;
        private readonly DateTime fecha;
        private readonly int paginas;
        private int paginasLeidas;
        private readonly Escritor autor;
        private readonly Isbn13 isbn;

        public string GetTitulo()
        {
            return titulo;
        }

        public DateTime GetFecha()
        {
            return fecha;
        }

        public int GetPaginas()
        {
            return paginas;
        }

        public int GetPaginasLeidas()
        {
            return paginasLeidas;
        }
        public void SetPaginasLeidas(int paginas)
        {
            paginasLeidas = paginas;
        }
        public Escritor GetAutor()
        {
            return autor;
        }

        public Libro(string titulo,
            in DateTime fecha,
            in int paginas,
            Escritor autor,
            Isbn13 isbn)
        {
            this.titulo = titulo;
            this.fecha = fecha;
            this.paginas = paginas;
            this.autor = autor;
            this.isbn = new Isbn13(isbn);
            SetPaginasLeidas(0);
        }
        public Libro(Libro l)
        {
            titulo = l.titulo;
            fecha = l.fecha;
            paginas = l.paginas;
            autor = l.autor;
            isbn = new Isbn13(l.isbn);
            SetPaginasLeidas(l.paginasLeidas);
        }

        public int Lee(in int paginas)
        {
            int leídas = Math.Clamp(paginas, 0, GetPaginas() - GetPaginasLeidas());
            SetPaginasLeidas(GetPaginasLeidas() + leídas);
            return leídas;
        }

        public int PorcentajeLeido()
        {
            return Convert.ToInt32(GetPaginasLeidas() * 100D / GetPaginas());
        }

        public string Descripcion()
        {
            return $"Libro\n" +
                   "--------------------------\n" +
                   $"Título: {GetTitulo()}\n" +
                   $"Fecha: {GetFecha().ToShortTimeString()}\n" +
                   $"Páginas: {GetPaginas()}\n" +
                   $"ISBN: {isbn.ATexto("-")}\n" +
                   $"Autor --------------------\n" +
                   $"{autor.Descripcion()}\n";
        }
    }

    class Escritor
    {
        private readonly string nombre;
        private readonly int nacimiento;
        private int publicaciones;

        public string GetNombre()
        {
            return nombre;
        }
        public int GetNacimiento()
        {
            return nacimiento;
        }
        public int GetPublicaciones()
        {
            return publicaciones;
        }
        private void SetPublicaciones(in int publicaciones)
        {
            this.publicaciones = publicaciones;
        }
        public Escritor(string nombre, in int nacimiento)
        {
            this.nombre = nombre;
            this.nacimiento = nacimiento;
            SetPublicaciones(0);
        }
        public Escritor(Escritor e)
        {
            nombre = e.nombre;
            nacimiento = e.nacimiento;
            SetPublicaciones(e.publicaciones);
        }
        public string Descripcion()
        {
            return $"Nombre: {GetNombre()}\n" +
                   $"Nacimiento: {GetNacimiento()}\n" +
                   $"Publicaciones: {GetPublicaciones()}";
        }
        public Libro Escribe(string titulo, Isbn13 isbn)
        {
            Range r = 400..800;
            SetPublicaciones(GetPublicaciones() + 1);
            return new Libro(titulo, DateTime.Now, new Random().Next(r.Start.Value, r.End.Value + 1), this, isbn);
        }
    }

    static class Programa
    {
        static void Main()
        {
            Escritor e = new Escritor("María", 1980);
            Isbn13 isbn = new Isbn13("9788420454665");
            Libro l = e.Escribe("Programación en C#", isbn);
            Console.WriteLine(l.Descripcion());
        }
    }
}