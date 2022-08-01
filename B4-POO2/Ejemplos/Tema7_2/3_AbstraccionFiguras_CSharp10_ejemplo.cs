using System;

namespace CasoDeEstudio
{
    abstract class Figura
    {
        public enum Color { Rojo, Azul, Verde, Naranja }
        private Color color;
        private Color GetColor()
        {
            return color;
        }
        public void SetColor(in Color color)
        {
            this.color = color;
        }
        protected Figura(in Color color)
        {
            SetColor(color);
        }
        abstract public double Area();
        abstract public double Perimetro();

        public override string ToString()
        {
            return $"Color: {GetColor()}\nArea: {Area():F2} cm2\nPerímetro: {Perimetro()} cm";
        }
    }

    class Circulo : Figura
    {
        private double radio_cm;

        public void SetRadio(double radio_cm)
        {
            this.radio_cm = radio_cm;
        }
        private double GetRadio()
        {
            return radio_cm;
        }

        public Circulo(in Color color, double radio_cm) : base(color)
        {
            SetRadio(radio_cm);
        }

        public override double Area()
        {
            return Math.PI * Math.Pow(GetRadio(), 2);
        }

        public override double Perimetro()
        {
            return Math.PI * GetRadio() * 2;
        }

        public override string ToString()
        {
            return $"Círculo\nRadio: {GetRadio()} cm\n{base.ToString()}";
        }
    }
    class Cuadrado : Figura
    {
        private double lado_cm;

        public void SetLado(double lado_cm)
        {
            this.lado_cm = lado_cm;
        }
        private double GetLado()
        {
            return lado_cm;
        }

        public Cuadrado(in Color color, double lado_cm) : base(color)
        {
            SetLado(lado_cm);
        }

        public override double Area()
        {
            return GetLado() * GetLado();
        }

        public override double Perimetro()
        {
            return GetLado() * 4d;
        }
        public override string ToString()
        {
            return $"Cuadrado\nLado: {GetLado()} cm\n{base.ToString()}";
        }
    }
    class Rombo : Figura
    {
        private double diagonal1_cm;
        private double diagonal2_cm;

        public void SetDiagonal1(double d_cm)
        {
            diagonal1_cm = d_cm;
        }
        private double GetDiagonal1()
        {
            return diagonal1_cm;
        }
        public void SetDiagonal2(double d_cm)
        {
            diagonal2_cm = d_cm;
        }
        private double GetDiagonal2()
        {
            return diagonal2_cm;
        }
        private double GetLado()
        {
            return Math.Sqrt(Math.Pow(GetDiagonal1() / 2d, 2d) + Math.Pow(GetDiagonal2() / 2d, 2d));
        }

        public Rombo(in Color color, double d1_cm, double d2_cm) : base(color)
        {
            SetDiagonal1(d1_cm);
            SetDiagonal2(d2_cm);
        }

        public override double Area()
        {
            return GetDiagonal1() * GetDiagonal2() / 2d;
        }

        public override double Perimetro()
        {
            return GetLado() * 4d;
        }
        public override string ToString()
        {
            return $"Rombo\nDiagonal1: {GetDiagonal1()} cm\nDiagonal2: {GetDiagonal2()} cm\nLado: {GetLado()} cm\n{base.ToString()}";
        }
    }

    public static class Principal
    {
        public static void Main()
        {
            Figura[] figuras = new Figura[]
            {
            new Cuadrado(Figura.Color.Rojo, 2),
            new Rombo(Figura.Color.Azul, 2, 2),
            new Circulo(Figura.Color.Verde, 2)
            };
            foreach (Figura f in figuras) Console.WriteLine(f);
        }
    }
}
