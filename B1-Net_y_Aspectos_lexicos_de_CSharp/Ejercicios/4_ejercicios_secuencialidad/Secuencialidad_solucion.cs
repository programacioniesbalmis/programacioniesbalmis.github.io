using System;

namespace Secuencialidad
{
    class Program
    {
        static void Main()
        {
            #region Ejercicio1
            //Pide dos números y calcula su producto, división y modulo.

            Console.WriteLine("Introduce el primer numero");

            double num1 = double.Parse(Console.ReadLine());

            Console.WriteLine("Introduce el segundo numero");

            double num2 = double.Parse(Console.ReadLine());

            double producto = num1 * num2;
            double division = num1 / num2;
            double modulo = num1 % num2;

            Console.WriteLine($"El resultado es:\nProducto = {producto}\nDivision = {division}\nModulo = {modulo}");

            #endregion

            #region Ejercicio2
            //Algoritmo que toma como dato de entrada un número que corresponde
            //a la longitud de un radio y nos calcula y escribe la longitud de la
            //circunferencia, el área del círculo que se corresponden con dicho radio.
            //l = 2πr; a = πr2

            Console.WriteLine("Introduce el radio en centimetros");

            double radio_cm = double.Parse(Console.ReadLine());

            double l = 2 * Math.PI * radio_cm;
            double a = Math.PI * Math.Pow(radio_cm, 2);

            Console.WriteLine($"El resultado es:\nLongitud = {l}\nArea = {a}");

            #endregion

            #region Ejercicio3
            //Pide un número(por ejemplo el 6)  y que salga en la pantalla dos mensajes:
            //El número anterior es 5.El número posterior es 7.

            Console.WriteLine("Introduce un numero");

            int numero = int.Parse(Console.ReadLine());

            int anterior = numero - 1;
            int posterior = numero + 1;

            Console.WriteLine($"El resultado es:\nAnterior = {anterior}\nPosterior = {posterior}");

            #endregion

            #region Ejercicio4
            //Pide un número por teclado y calcula su cubo.

            Console.WriteLine("Introduce un numero");

            int numeroAElevar = int.Parse(Console.ReadLine());

            double numeroAlCubo = Math.Pow(numeroAElevar, 3);

            Console.WriteLine($"El resultado es: {numeroAlCubo}");

            #endregion

            #region Ejercicio5
            //Algoritmo que transforme una velocidad en kilómetros por hora a metros por segundo.

            const int M_X_KM = 1000;
            const int H_ENTRE_S = 3600;

            Console.WriteLine("Introduce un numero");

            double velocidad_kh = double.Parse(Console.ReadLine());

            double velocidad_ms = velocidad_kh * M_X_KM / H_ENTRE_S;

            Console.WriteLine($"El resultado es: {velocidad_ms}");

            #endregion

            #region Ejercicio6
            //Algoritmo que calcule el precio de una venta conociendo el precio por unidad(sin IVA) del producto,
            //el número de productos vendidos y el porcentaje de IVA aplicado. Los datos se leerán de teclado

            Console.WriteLine("Introduce el precio base");
            double precioBase = double.Parse(Console.ReadLine());

            Console.WriteLine("Introduce el iva en %");
            double iva_tpu = double.Parse(Console.ReadLine()) / 100;

            Console.WriteLine("Introduce la cantidad");
            int cantidadUnidades = int.Parse(Console.ReadLine());

            double ventaTotal = (precioBase + precioBase * iva_tpu) * cantidadUnidades;

            Console.WriteLine($"El precio de la venta a sido: {ventaTotal}");

            #endregion

            #region Ejercicio7
            //Se piden dos números y se calcula su media.El programa presenta en pantalla la media con dos decimales de precisión.

            Console.WriteLine("Introduce el primer numero");

            int valor1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduce el segundo numero");

            int valor2 = int.Parse(Console.ReadLine());

            double media = (valor1 + valor2) / 2;

            Console.WriteLine($"El precio de la venta a sido: {media:F2}");

            #endregion
        }
    }
}
