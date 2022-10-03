using System.Text;
using System.Text.RegularExpressions;

namespace CasoDeEstudio
{
    public struct Matricula
    {
        private readonly string matricula;

        private string GetMatricula()
        {
            return matricula;
        }
        public static bool EsValida(string matricula)
        {
            return Regex.IsMatch(matricula, @"^[0-9]{4} [^a-z\W\s\dAEIOU]{3}$");
        }
        public Matricula(string matricula)
        {
            if (!EsValida(matricula))
                throw new ArgumentException($"La matrícula {matricula} no es correcta.");
            this.matricula = matricula;
        }
        public override string ToString()
        {
            return GetMatricula();
        }
    }

    public class Vehiculo
    {
        public enum Color { Blanco, Morado, Cian, Azul, Rojo, Verde, Negro, Naranja, Gris };
        public enum Marca { DESCONOCIDA, BMW, SEAT, AUDI, RENAULT, MAN, DAF, CITROEN, TOYOTA, SUZUKI, YAMAHA, MERCEDES, PEGASO };

        private readonly Matricula matricula;
        private readonly Color color;
        private readonly Marca marca;
        private readonly ushort ocupantes;

        public Matricula GetMatricula()
        {
            return matricula;
        }
        public Color GetColor()
        {
            return color;
        }
        public Marca GetMarca()
        {
            return marca;
        }
        public ushort GetOcupantes()
        {
            return ocupantes;
        }
        public virtual object GetCategoria()
        {
            return "SinIdentificar";
        }
        public Vehiculo(Matricula matricula, Color color, Marca marca, ushort ocupantes)
        {
            this.matricula = matricula;
            this.color = color;
            this.marca = marca;
            this.ocupantes = ocupantes;
        }
        public override string ToString()
        {
            return $"Matricula: {GetMatricula()}\n"
                   + $"Color: {GetColor()}\n"
                   + $"Marca: {GetMarca()}\n"
                   + $"Ocupantes: {GetOcupantes()}\n"
                   + $"Categoria: {GetCategoria()}";
        }
        public override bool Equals(object? obj)
        {
            return obj is Vehiculo v && GetMatricula().ToString().CompareTo(v.GetMatricula().ToString()) == 0;
        }
        public override int GetHashCode()
        {
            return GetMatricula().ToString().GetHashCode();
        }
    }

    public class Coche : Vehiculo
    {
        public enum Categoria { SinIdentificar, Berlina, Coupe, Sedan, Cabrio, TodoTerreno, MonoVolumen, Crossover };

        private readonly Categoria categoria;

        public override object GetCategoria()
        {
            return categoria;
        }
        public Coche(Matricula matricula, Color color, Marca marca, ushort ocupantes, Categoria categoria)
               : base(matricula, color, marca, ocupantes)
        {
            this.categoria = categoria;
        }
    }
    class Moto : Vehiculo
    {
        public enum Categoria { SinIdentificar, Scooter, Motocross, Naked, Trail, Supermotard }

        private readonly Categoria categoria;

        public override object GetCategoria()
        {
            return categoria;
        }
        public Moto(Matricula matricula, Color color, Marca marca, ushort ocupantes, Categoria categoria)
               : base(matricula, color, marca, ocupantes)
        {
            this.categoria = categoria;
        }
    }

    class Camion : Vehiculo
    {
        public enum Categoria { SinIdentificar, Articulado, Frigorífico, Cisterna, Trailer };

        private readonly ushort ejes;
        private readonly ushort cargaMaximaKg;
        private readonly Categoria categoria;

        public Camion(Matricula matricula, Color color, Marca marca, ushort ocupantes, ushort ejes, ushort cargaMaximaKg, Categoria categoria)
            : base(matricula, color, marca, ocupantes)
        {
            this.ejes = ejes;
            this.cargaMaximaKg = cargaMaximaKg;
            this.categoria = categoria;
        }
        public ushort GetEjes()
        {
            return ejes;
        }
        public ushort GetCargaMaximaKg()
        {
            return cargaMaximaKg;
        }
        public override object GetCategoria()
        {
            return categoria;
        }
        public override string ToString()
        {
            return $"{base.ToString()}\n"
                   + $"Número de ejes: {GetEjes()}\n"
                   + $"Carga Máxima: {GetCargaMaximaKg()} Kg";
        }
    }

    class Autobus : Vehiculo
    {
        public enum Categoria { SinIdentificar, Articulado, UnPiso, DosPisos, Microbus, Eléctrico };

        private readonly Categoria categoria;

        public override object GetCategoria()
        {
            return categoria;
        }
        public Autobus(Matricula matricula, Color color, Marca marca, ushort ocupantes, Categoria categoria)
               : base(matricula, color, marca, ocupantes)
        {
            this.categoria = categoria;
        }
    }

    class CampaVehiculos
    {
        public const int CAPACIDAD = 5;
        private readonly Vehiculo?[] plazas;

        public CampaVehiculos()
        {
            plazas = new Vehiculo[CAPACIDAD];
        }
        public int GetPlazasOcupadas()
        {
            int ocupadas = 0;
            foreach (Vehiculo? v in GetPlazas())
                if (v != null) ocupadas++;
            return ocupadas;
        }
        private Vehiculo?[] GetPlazas()
        {
            return plazas;
        }
        private int Busca(Vehiculo v)
        {
            for (int i = 0; i < GetPlazas().Length; i++)
                if (v.Equals(GetPlazas()[i]))
                    return i;
            return -1;
        }
        private int BuscaPlazaVacia()
        {
            for (int i = 0; i < GetPlazas().Length; i++)
                if (GetPlazas()[i] == null)
                    return i;
            return -1;
        }
        public bool Entra(Vehiculo v, out int plaza, out string? problema)
        {
            bool entra = GetPlazasOcupadas() < CAPACIDAD;
            plaza = -1;
            problema = null;

            if (entra)
            {
                int i = Busca(v);
                entra = i < 0;
                if (entra)
                {
                    plaza = BuscaPlazaVacia() + 1;
                    GetPlazas()[plaza - 1] = v;
                }
                else
                    problema = $"Ya se encuentra en el aparcamiento el vehículo {v.GetMatricula()}";
            }
            else
                problema = "Aparcamiento lleno";

            return entra;
        }
        public bool Sale(Vehiculo v, out int plaza, out string? problema)
        {
            bool sale = GetPlazasOcupadas() > 0;
            problema = null;
            plaza = -1;
            if (sale)
            {
                int i = Busca(v);
                sale = i >= 0;
                if (sale)
                {
                    plaza = i + 1;
                    GetPlazas()[i] = null;
                }
                else
                    problema = $"No se registró la entrada del vehículo {v.GetMatricula()}";
            }
            else
                problema = "Aparcamiento vacío";

            return sale;
        }
        public override string ToString()
        {
            StringBuilder texto = new StringBuilder("\nVehículos en el aparcamiento...\n\n");
            for (int i = 0; i < GetPlazas().Length; i++)
            {
                texto.Append($"Plaza {i + 1}:\n{GetPlazas()[i]?.ToString() ?? "Vacía"}\n\n");
            }
            return texto.ToString();
        }
    }

    static class Principal
    {
        static void Entra(CampaVehiculos campa, Vehiculo v)
        {
            string t1 = $"Entrando {v.GetType().Name} {v.GetCategoria()} {v.GetMatricula()}";
            string t2 = campa.Entra(v, out int plaza, out string? problema) ? $"Aparcado en la plaza {plaza}" : $"{problema}";
            Console.WriteLine($"{t1,-42}-> {t2}");
        }
        static void Sale(CampaVehiculos campa, Vehiculo v)
        {
            string t1 = $"Saliendo {v.GetType().Name} {v.GetCategoria()} {v.GetMatricula()}";
            string t2 = campa.Sale(v, out int plaza, out string? problema) ? $"Deja libre la plaza {plaza}" : $"{problema}";
            Console.WriteLine($"{t1,-42}-> {t2}");
        }

        static void Main()
        {
            CampaVehiculos campa = new CampaVehiculos();

            Entra(campa, new Coche(new Matricula("1020 DRG"), 
                 Vehiculo.Color.Azul, Vehiculo.Marca.SEAT, 3, Coche.Categoria.Coupe));
            Entra(campa, new Camion(new Matricula("8798 JWR"), 
                  Vehiculo.Color.Blanco, Vehiculo.Marca.DAF, 1, 2, 6000, Camion.Categoria.Frigorífico));
            Entra(campa, new Coche(new Matricula("7643 LRF"), 
                  Vehiculo.Color.Rojo, Vehiculo.Marca.BMW, 4, Coche.Categoria.TodoTerreno));
            Entra(campa, new Coche(new Matricula("1020 DRG"), 
                  Vehiculo.Color.Azul, Vehiculo.Marca.SEAT, 3, Coche.Categoria.Coupe));
            Entra(campa, new Vehiculo(new Matricula("0000 DGP"), 
                  Vehiculo.Color.Negro, Vehiculo.Marca.DESCONOCIDA, 2));
            Entra(campa, new Moto(new Matricula("1111 GRF"), 
                  Vehiculo.Color.Rojo, Vehiculo.Marca.YAMAHA, 2, Moto.Categoria.Naked));
            Entra(campa, new Coche(new Matricula("1020 DRG"), 
                  Vehiculo.Color.Azul, Vehiculo.Marca.SEAT, 3, Coche.Categoria.Coupe));
            Sale(campa, new Camion(new Matricula("8798 JWR"), 
                 Vehiculo.Color.Blanco, Vehiculo.Marca.DAF, 1, 2, 6000, Camion.Categoria.Frigorífico));
            Sale(campa, new Camion(new Matricula("8798 JWR"), 
                 Vehiculo.Color.Blanco, Vehiculo.Marca.DAF, 1, 2, 6000, Camion.Categoria.Frigorífico));
            Sale(campa, new Moto(new Matricula("1111 GRF"), 
                 Vehiculo.Color.Rojo, Vehiculo.Marca.YAMAHA, 2, Moto.Categoria.Naked));
            Entra(campa, new Moto(new Matricula("1111 GRF"), 
                  Vehiculo.Color.Rojo, Vehiculo.Marca.YAMAHA, 2, Moto.Categoria.Naked));
            Entra(campa, new Camion(new Matricula("8798 JWR"), 
                  Vehiculo.Color.Blanco, Vehiculo.Marca.DAF, 1, 2, 6000, Camion.Categoria.SinIdentificar));

            Console.WriteLine(campa);
        }
    }
}