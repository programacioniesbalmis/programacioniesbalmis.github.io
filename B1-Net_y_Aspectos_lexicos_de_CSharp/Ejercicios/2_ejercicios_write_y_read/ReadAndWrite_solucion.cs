using System;

namespace ReadAndWrite
{
    class Program
    {
        static void Main()
        {
            #region Ejercicio1
            // Realizar un programa en C# que muestre por pantalla la siguiente
            // salida, realízalo primero utilizando varios WriteLine y después
            // con uno solo:
            // Con varios WriteLine
            Console.WriteLine("12345");
            Console.WriteLine("12345");
            Console.WriteLine("0");
            Console.WriteLine("{0,10}", "abc");
            Console.WriteLine("{0,10}", "abcdef");
            Console.WriteLine();
            Console.WriteLine("{0,-10} {1,-10}", "abc", "abcdef");
            Console.WriteLine();
            Console.WriteLine("{0:F2}", 54.87);
            //Con un solo WriteLine
            Console.WriteLine("12345\n12345\n0\n{0,10}\n{1,10}\n\n{0,-10}{1,-10}\n\n{2:F2}", "abc", "abcdef", 54.87);
            #endregion

            #region Ejercicio2
            // Realiza un programa que recoja por teclado los siguientes datos
            // 5, 23.4, z y los guarde en una variable a de tipo int, en b de
            // tipo float y en c de tipo char.
            Console.Write("Escribe un número entero: ");
            int a = int.Parse(Console.ReadLine());
            Console.Write("Escribe un número decimal: ");
            float b = float.Parse(Console.ReadLine());
            Console.Write("Escribe un carácter: ");
            char c = char.Parse(Console.ReadLine());
            Console.WriteLine("{0:D}, {1:F}, {2}", a, b, c);
            #endregion

            #region Ejercicio3
            // Realiza un programa que te pida el nombre y te diga Hola nombre.
            Console.WriteLine("Escribe tu nombre: ");
            string nombre = Console.ReadLine();
            Console.WriteLine("Hola " + nombre);
            #endregion

            #region Ejercicio4
            // Realiza un programa que muestre el siguiente número 123,45678
            // primero en coma fija y luego en formato exponencial, en los
            // dos casos con 4 decimales.
            Console.WriteLine("{0:F4}", 123.45678);
            Console.WriteLine("{0:E4}", 123.45678);
            #endregion

            #region Ejercicio5
            // Programa que escriba el número 15 en decimal y hexadecimal
            int numero = 15;
            Console.WriteLine("{0:D}", numero);
            Console.WriteLine("{0:X}", numero);
            #endregion
        }
    }
}