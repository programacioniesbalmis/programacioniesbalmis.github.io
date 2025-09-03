namespace EjemploStrategy;

public interface IEstrategiaDeAtaque
{
    int Ataca(Unidad u);
}

public interface IGeneradorDePuntos
{
    int Fuerza { get; }
    int Velocidad { get; }
    int Inteligencia { get; }
}

public class GeneradorDePuntosFijo : IGeneradorDePuntos
{
    public int Fuerza => 6;
    public int Velocidad => 6;
    public int Inteligencia => 6;
    
}

public class GeneradorDePuntosAleatorio : IGeneradorDePuntos
{
    private static int PuntosAlestorios => new Random().Next(0, 8);
    public int Fuerza => PuntosAlestorios;
    public int Velocidad => PuntosAlestorios;
    public int Inteligencia => PuntosAlestorios;

}

public class EstrategiaAtaqueDemoledor : IEstrategiaDeAtaque
{
    public int Ataca(Unidad u) => u.Fuerza * (u.Velocidad / 2);
}

public class EstrategiaAtaqueRapido : IEstrategiaDeAtaque
{
    public int Ataca(Unidad u) => u.Velocidad + u.Inteligencia;
}

public class Unidad
{
    public int Fuerza { get; }
    public int Velocidad { get; }
    public int Inteligencia { get; }
    private IGeneradorDePuntos GeneradorDePuntos { get; }
    private IEstrategiaDeAtaque EstrategiaDeAtaque { get; }

    public Unidad(
        IGeneradorDePuntos generadorDePuntos,
        IEstrategiaDeAtaque estrategiaDeAtaque)
    {
        GeneradorDePuntos = generadorDePuntos;
        EstrategiaDeAtaque = estrategiaDeAtaque;
        Fuerza = GeneradorDePuntos.Fuerza;
        Velocidad = GeneradorDePuntos.Velocidad;
        Inteligencia = GeneradorDePuntos.Inteligencia;
    }
    public int Ataca() => EstrategiaDeAtaque.Ataca(this);
    public override string ToString()
    => $"Unidad con {GeneradorDePuntos.GetType().Name} y {EstrategiaDeAtaque.GetType().Name} " +
       $"F={Fuerza} V={Velocidad} I={Inteligencia} A={Ataca()}";
}

class Principal
{
    public static void Main()
    {
        Unidad uAleatoriaDeAtaqueRapido = new (
                    generadorDePuntos: new GeneradorDePuntosAleatorio(),
                    estrategiaDeAtaque: new EstrategiaAtaqueRapido());
        Unidad uFijaDeAtaqueDemoledor = new (
                    generadorDePuntos: new GeneradorDePuntosFijo(),
                    estrategiaDeAtaque: new EstrategiaAtaqueDemoledor());
        Console.WriteLine(uAleatoriaDeAtaqueRapido);
        Console.WriteLine(uFijaDeAtaqueDemoledor);
    }
}