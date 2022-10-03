using System;

namespace ObjetosValor
{
    struct Angulo
    {
        public readonly int Grados;
        public readonly double Radianes;

         public Angulo(in int grados)
         {
             const int GRADOS_TOTALES = 360;
             int gr = grados % GRADOS_TOTALES;
             gr = gr < 0 ? gr + GRADOS_TOTALES : gr;
             Grados = gr;
             Radianes = Grados * Math.PI / 180d;
         }
        public Angulo Suma(in int grados)
        {
            return new Angulo(Grados + grados);
        }
    }

    struct Punto2D
    {
        public readonly double X;
        public readonly double Y;

        public Punto2D(in double x, in double y)
        {
            Y = y;
            X = x;
        }

        public Punto2D Desplaza(in double distancia, in Angulo angulo)
        {

            double fila = Y + distancia * Math.Sin(angulo.Radianes);
            double columna = X + distancia * Math.Cos(angulo.Radianes);
            return new Punto2D(fila, columna);
        }
        public string ATexto()
        {
            return $"({X:G2} - {Y:G2})";
        }
    }

    public static class Principal
    {
        public static void Main()
        {
            Angulo a = new Angulo(0);
            Punto2D p1 = new Punto2D(4d, 4d);
            Punto2D p2 = p1;
            Punto2D p3 = p2.Desplaza(2d, a.Suma(405));
            Console.WriteLine($"a = {a.Grados} grados");
            Console.WriteLine("p1 = " + p1.ATexto());
            Console.WriteLine("p2 = " + p2.ATexto());
            Console.WriteLine("p3 = " + p3.ATexto());
        }
    }
}