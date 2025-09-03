namespace CasoEsturdioStrategy;

public interface IAcelerable
{
    void Acelera();
}

public abstract class Motor : IAcelerable
{
    public int Revoluciones { get; protected set; } = 0;
    abstract public void Acelera();
}

public class MotorGasolina : Motor
{
    public override void Acelera()
    {
        Console.Write("inyectando gasolina para explosión");
        Revoluciones += 2;
    }
}
public class MotorElectrico : Motor
{
    public override void Acelera()
    {
        Console.Write("aumentando potencia eléctrica");
        Revoluciones += 6;
    }
}

public interface INeumaticos
{
    string IndiceVelocidad { get; }
    int IndiceCarga { get; }
    int Radio { get; }
    int Perfil { get; }
    int Ancho { get; }
    string Descripcion =>
        $"{Ancho}/{Perfil} R{Radio} {IndiceCarga}{IndiceVelocidad}";
}

public record class NeumaticosNormal : INeumaticos
{
    public string IndiceVelocidad => "H";
    public int IndiceCarga => 88;
    public int Radio => 16;
    public int Perfil => 55;
    public int Ancho => 205;
}

public record class NeumaticosSport : INeumaticos
{
    public string IndiceVelocidad => "Y";
    public int IndiceCarga => 92;
    public int Radio => 18;
    public int Perfil => 40;
    public int Ancho => 225;
}

public class Coche : IAcelerable
{
    public string Id { get; }
    public Motor Motor { get; }
    public INeumaticos Neumaticos { get; }

    public Coche(
        string id ,
        Motor motor,
        INeumaticos neumaticos)
    {
        Id = id;
        Motor = motor;
        Neumaticos = neumaticos;
    }

    public void Acelera()
    {
        Console.Write($"Coche {Id} ");
        Motor.Acelera();
        Console.WriteLine($" a {Motor.Revoluciones} r.p.m.");
    }

    public override string ToString() =>
        $"Coche {Id} {Motor.GetType().Name} y neumaticos {Neumaticos.Descripcion}";
}


public class Principal
{
    public static void Main()
    {
        Coche c1 = new (
            id: "C1",
            motor: new MotorGasolina(),
            neumaticos: new NeumaticosNormal());
        Console.WriteLine(c1);
        c1.Acelera();
        c1.Acelera();
        Console.WriteLine();

        Coche c2 = new (
            id: "C2",
            motor: new MotorElectrico(),
            neumaticos: new NeumaticosSport());
        Console.WriteLine(c2);
        c2.Acelera();
        c2.Acelera();
    }
}



