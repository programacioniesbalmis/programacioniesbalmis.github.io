using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace CasoDeEstudio;

// Enums extraídos de las clases
public enum Color { Blanco, Morado, Cian, Azul, Rojo, Verde, Negro, Naranja, Gris }
public enum Marca { DESCONOCIDA, BMW, SEAT, AUDI, RENAULT, MAN, DAF, CITROEN, TOYOTA, SUZUKI, YAMAHA, MERCEDES, PEGASO }
public enum TipoCoche { SinIdentificar, Berlina, Coupe, Sedan, Cabrio, TodoTerreno, MonoVolumen, Crossover }
public enum TipoMoto { SinIdentificar, Scooter, Motocross, Naked, Trail, Supermotard }
public enum TipoCamion { SinIdentificar, Articulado, Frigorífico, Cisterna, Trailer }
public enum TipoAutobus { SinIdentificar, Articulado, UnPiso, DosPisos, Microbus, Eléctrico }

public record class Matricula
{
    private string _matricula;

    public static bool EsValida(string matricula) =>
        Regex.IsMatch(
            input: matricula,
            pattern: @"^[0-9]{4} [^a-z\W\s\dAEIOU]{3}$");

    public Matricula(string matricula)
    {
        Debug.Assert(
            condition: EsValida(matricula: matricula),
            message: $"La matrícula {matricula} no es correcta.");
        _matricula = matricula;
    }

    public override string ToString() => _matricula;
}

public class Vehiculo
{
    public Matricula Matricula { get; }
    public Color Color { get; }
    public Marca Marca { get; }
    public ushort Ocupantes { get; }

    public virtual string Tipo => "SinIdentificar";

    public Vehiculo(
        Matricula matricula,
        Color color,
        Marca marca,
        ushort ocupantes)
    {
        Matricula = matricula;
        Color = color;
        Marca = marca;
        Ocupantes = ocupantes;
    }

    public override string ToString() => $"""
        {GetType().Name} {Marca} {Tipo} {Matricula} color {Color} y {Ocupantes} ocupantes.
        """;

    public override bool Equals(object? obj) =>
        obj is Vehiculo v && Matricula.Equals(v.Matricula.ToString());

    public override int GetHashCode() => Matricula.ToString().GetHashCode();
}

public class Coche : Vehiculo
{
    private TipoCoche _tipo;
    public override string Tipo => _tipo.ToString();

    public Coche(
        Matricula matricula,
        Color color,
        Marca marca,
        ushort ocupantes,
        TipoCoche tipo)
        : base(
            matricula: matricula,
            color: color,
            marca: marca,
            ocupantes: ocupantes)
    {
        _tipo = tipo;
    }
}

class Moto : Vehiculo
{
    private TipoMoto _tipo;
    public override string Tipo => _tipo.ToString();

    public Moto(
        Matricula matricula,
        Color color,
        Marca marca,
        ushort ocupantes,
        TipoMoto tipo)
        : base(
            matricula: matricula,
            color: color,
            marca: marca,
            ocupantes: ocupantes)
    {
        _tipo = tipo;
    }
}

class Camion : Vehiculo
{
    private TipoCamion _tipo;
    public ushort Ejes { get; }
    public ushort CargaMaximaKg { get; }

    public Camion(
        Matricula matricula,
        Color color,
        Marca marca,
        ushort ocupantes,
        ushort ejes,
        ushort cargaMaximaKg,
        TipoCamion tipo)
        : base(
            matricula: matricula,
            color: color,
            marca: marca,
            ocupantes: ocupantes)
    {
        Ejes = ejes;
        CargaMaximaKg = cargaMaximaKg;
        _tipo = tipo;
    }

    public override string Tipo => _tipo.ToString();

    public override string ToString()
    {
        StringBuilder textoBase = new(base.ToString());
        textoBase.Replace(
            oldValue: GetType().Name,
            newValue: $"{GetType().Name} {Ejes} ejes con MMA de {CargaMaximaKg} Kg");
        return textoBase.ToString();
    }
}

class Autobus : Vehiculo
{
    private TipoAutobus _tipo;
    public override string Tipo => _tipo.ToString();

    public Autobus(
        Matricula matricula,
        Color color,
        Marca marca,
        ushort ocupantes,
        TipoAutobus tipo)
        : base(
            matricula: matricula,
            color: color,
            marca: marca,
            ocupantes: ocupantes)
    {
        _tipo = tipo;
    }
}

class CampaVehiculos
{
    private List<Vehiculo?> Plazas { get; }

    public CampaVehiculos(int capacidad)
    {
        Debug.Assert(capacidad > 0, "La capacidad del aparcamiento debe ser mayor que cero.");
        Plazas = [..new Vehiculo?[capacidad]];
    }

    private int PlazasOcupadas
    {
        get
        {
            int ocupadas = 0;
            foreach (Vehiculo? v in Plazas)
                if (v != null) ocupadas++;
            return ocupadas;
        }
    }

    private int? Busca(Vehiculo v)
    {
        for (int i = 0; i < Plazas.Count; i++)
            if (v.Equals(Plazas[i]))
                return i;
        return null;
    }

    private int? PlazaVacia
    {
        get
        {
            for (int i = 0; i < Plazas.Count; i++)
                if (Plazas[i] == null)
                    return i;
            return null;
        }
    }

    public (bool puedeEntrar, int plaza, string? problema) PuedeEntrar(Vehiculo v)
    {
        bool puedeEntrar = PlazasOcupadas < Plazas.Count;
        int plaza = -1;
        string? problema = null;

        if (puedeEntrar)
        {
            puedeEntrar = !Busca(v).HasValue;
            if (puedeEntrar)
            {
                plaza = PlazaVacia!.Value + 1;
            }
            else
            {
                problema = "Ya se encuentra en el aparcamiento el vehículo " + v.Matricula;
            }
        }
        else
        {
            problema = "Aparcamiento lleno";
        }

        return (puedeEntrar, plaza, problema);
    }

    public void Entra(Vehiculo v)
    {
        (bool puedeEntrar, int plaza, string? problema) = PuedeEntrar(v);
        Debug.Assert(puedeEntrar, problema);
        Plazas[plaza - 1] = v;
    }

    public (bool puedeSalir, int plaza, string? problema) PuedeSalir(Vehiculo v)
    {
        bool puedeSalir = PlazasOcupadas > 0;
        string? problema = null;
        int plaza = -1;
        
        if (puedeSalir)
        {
            int? i = Busca(v);
            puedeSalir = i.HasValue;
            if (puedeSalir)
            {
                plaza = i!.Value + 1;
            }
            else
            {
                problema = "No se registró la entrada del vehículo " + v.Matricula;
            }
        }
        else
        {
            problema = "Aparcamiento vacío";
        }

        return (puedeSalir, plaza, problema);
    }

    public void Sale(Vehiculo v)
    {
        (bool puedeSalir, int plaza, string? problema) = PuedeSalir(v);
        Debug.Assert(puedeSalir, problema);
        Plazas[plaza - 1] = null;
    }

    public override string ToString()
    {
        StringBuilder sb = new("Veículos en el aparcamiento...\n\n");
        foreach ((int i, Vehiculo? v) in Plazas.Index())
        {
            sb.AppendLine($"Plaza {i + 1}: {v?.ToString() ?? "Vacía"}");
        }      
        return sb.ToString();
    }
}

public static class Principal
{
    static void Entra(CampaVehiculos campa, Vehiculo v)
    {
        string t1 = "Entrando " + v.GetType().Name + " " + v.Tipo + " " + v.Matricula;
        var (puedeEntrar, plaza, problema) = campa.PuedeEntrar(v);
        if (puedeEntrar)
        {
            campa.Entra(v);
            Console.WriteLine(t1.PadRight(42) + "-> Aparcado en la plaza " + plaza);
        }
        else
        {
            Console.WriteLine(t1.PadRight(42) + "-> " + (problema ?? ""));
        }
    }

    static void Sale(CampaVehiculos campa, Vehiculo v)
    {
        string t1 = "Saliendo " + v.GetType().Name + " " + v.Tipo + " " + v.Matricula;
        var (puedeSalir, plaza, problema) = campa.PuedeSalir(v);
        if (puedeSalir)
        {
            campa.Sale(v);
            Console.WriteLine(t1.PadRight(42) + "-> Deja libre la plaza " + plaza);
        }
        else
        {
            Console.WriteLine(t1.PadRight(42) + "-> " + (problema ?? ""));
        }
    }

    public static void Main()
    {
        CampaVehiculos campa = new(capacidad: 5);

        Entra(
            campa: campa,
            v: new Coche(
                matricula: new Matricula(matricula: "1020 DRG"),
                color: Color.Azul,
                marca: Marca.SEAT,
                ocupantes: 3,
                tipo: TipoCoche.Coupe));

        Entra(
            campa: campa,
            v: new Camion(
                matricula: new Matricula(matricula: "8798 JWR"),
                color: Color.Blanco,
                marca: Marca.DAF,
                ocupantes: 1,
                ejes: 2,
                cargaMaximaKg: 6000,
                tipo: TipoCamion.Frigorífico));

        Entra(
            campa: campa,
            v: new Coche(
                matricula: new Matricula(matricula: "7643 LRF"),
                color: Color.Rojo,
                marca: Marca.BMW,
                ocupantes: 4,
                tipo: TipoCoche.TodoTerreno));

        Entra(
            campa: campa,
            v: new Coche(
                matricula: new Matricula(matricula: "1020 DRG"),
                color: Color.Azul,
                marca: Marca.SEAT,
                ocupantes: 3,
                tipo: TipoCoche.Coupe));

        Entra(
            campa: campa,
            v: new Vehiculo(
                matricula: new Matricula(matricula: "0000 DGP"),
                color: Color.Negro,
                marca: Marca.DESCONOCIDA,
                ocupantes: 2));

        Entra(
            campa: campa,
            v: new Moto(
                matricula: new Matricula(matricula: "1111 GRF"),
                color: Color.Rojo,
                marca: Marca.YAMAHA,
                ocupantes: 2,
                tipo: TipoMoto.Naked));

        Entra(
            campa: campa,
            v: new Coche(
                matricula: new Matricula(matricula: "1020 DRG"),
                color: Color.Azul,
                marca: Marca.SEAT,
                ocupantes: 3,
                tipo: TipoCoche.Coupe));

        Sale(
            campa: campa,
            v: new Camion(
                matricula: new Matricula(matricula: "8798 JWR"),
                color: Color.Blanco,
                marca: Marca.DAF,
                ocupantes: 1,
                ejes: 2,
                cargaMaximaKg: 6000,
                tipo: TipoCamion.Frigorífico));

        Sale(
            campa: campa,
            v: new Camion(
                matricula: new Matricula(matricula: "8798 JWR"),
                color: Color.Blanco,
                marca: Marca.DAF,
                ocupantes: 1,
                ejes: 2,
                cargaMaximaKg: 6000,
                tipo: TipoCamion.Frigorífico));

        Sale(
            campa: campa,
            v: new Moto(
                matricula: new Matricula(matricula: "1111 GRF"),
                color: Color.Rojo,
                marca: Marca.YAMAHA,
                ocupantes: 2,
                tipo: TipoMoto.Naked));

        Entra(
            campa: campa,
            v: new Moto(
                matricula: new Matricula(matricula: "1111 GRF"),
                color: Color.Rojo,
                marca: Marca.YAMAHA,
                ocupantes: 2,
                tipo: TipoMoto.Naked));

        Entra(
            campa: campa,
            v: new Camion(
                matricula: new Matricula(matricula: "8798 JWR"),
                color: Color.Blanco,
                marca: Marca.DAF,
                ocupantes: 1,
                ejes: 2,
                cargaMaximaKg: 6000,
                tipo: TipoCamion.SinIdentificar));

        Console.WriteLine(campa);
    }
}
