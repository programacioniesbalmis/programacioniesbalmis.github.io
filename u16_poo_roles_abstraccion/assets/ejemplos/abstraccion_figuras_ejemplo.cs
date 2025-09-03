namespace EjemploAbstraccion;

public enum ColorFigura { Rojo, Azul, Verde, Naranja }

public abstract class Figura
{
    private ColorFigura Color { get; }
    protected Figura(ColorFigura color)
    {
        Color = color;
    }

    abstract public double Area_cm2 { get; }
    abstract public double Perimetro_cm { get; }
    public override string ToString() => $"""
                    Color: {Color}
                    Area: {Area_cm2:F2} cm²
                    Perímetro: {Perimetro_cm:F2} cm
                    """;
}

public class Circulo : Figura
{
    public double Radio_cm { get; }
    public Circulo(
           ColorFigura color,
           double radio_cm) : base(color)
    {
        Radio_cm = radio_cm;
    }
    public override double Area_cm2 =>
        Math.PI * Math.Pow(Radio_cm, 2);
    public override double Perimetro_cm =>
        Math.PI * Radio_cm * 2;
    public override string ToString() => $"""
                    ____Círculo____
                    Radio: {Radio_cm} cm
                    {base.ToString()}
                    """;
}

public class Cuadrado : Figura
{
    private double Lado_cm { get; }

    public Cuadrado(
           ColorFigura color,
           double lado_cm) : base(color)
    {
        Lado_cm = lado_cm;
    }

    public override double Area_cm2 =>
        Lado_cm * Lado_cm;
    public override double Perimetro_cm =>
        Lado_cm * 4d;
    public override string ToString() => $"""
                    ____Cuadrado____
                    Lado: {Lado_cm} cm
                    {base.ToString()}
                    """;
}
public class Rombo : Figura
{
    private double Diagonal1_cm { get; }
    private double Diagonal2_cm { get; }
    private double Lado_cm =>
        Math.Sqrt(Math.Pow(Diagonal1_cm / 2d, 2d)
        + Math.Pow(Diagonal2_cm / 2d, 2d));

    public Rombo(
           ColorFigura color,
           double d1_cm,
           double d2_cm) : base(color)
    {
        Diagonal1_cm = d1_cm;
        Diagonal2_cm = d2_cm;
    }

    public override double Area_cm2 =>
        Diagonal1_cm * Diagonal2_cm / 2d;
    public override double Perimetro_cm =>
        Lado_cm * 4d;
    public override string ToString() => $"""
                    ____Rombo____
                    Diagonal1: {Diagonal1_cm} cm
                    Diagonal2: {Diagonal2_cm} cm
                    Lado: {Lado_cm:F2} cm
                    {base.ToString()}
                    """;
}

public static class Principal
{
    public static void Main()
    {
        List<Figura> figuras =
        [
            new Cuadrado(
                color: ColorFigura.Rojo,
                lado_cm: 2),
            new Rombo(
                color: ColorFigura.Azul,
                d1_cm: 2,
                d2_cm: 2),
            new Circulo(
                color: ColorFigura.Verde,
                radio_cm: 2)
        ];

        foreach (Figura f in figuras)
            Console.WriteLine(f);
    }
}

