using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region Ejercicio1
/* Programa que calcula y escribe la suma y el producto de los 10
 * primeros números naturales. Se deben usar acumuladores para resolverlo*/

namespace Ejercicio1
{
    class Programa
    {
        static void Main()
        {
            int suma = 0;
            int producto = 1;

            for (int i = 1; i <= 10; i++)
            {
                suma = suma + i;
                producto = producto * i;
            }

            Console.WriteLine("SUMA: {0}\nPRODUCTO: {1}\n", suma, producto);
        }
    }
}
#endregion
#region Ejercicio2
/* Programa que lee 10 números y cuenta cuántos de ellos
 * son positivos (mayores que 0). Se deben usar contadores para resolverlo.*/

namespace Ejercicio2
{
    class Programa
    {
        static void Main()
        {
            int valor;
            int positivos = 0;

            for (int cont = 1; cont <= 10; cont++)
            {
                Console.Write("Introduzca valor {0}: ", cont);
                valor = int.Parse(Console.ReadLine());

                if (valor > 0) 
                    positivos++;
            }

            Console.WriteLine("\nNúmeros positivos introducidos: {0}\n", positivos);
        }
    }
}
#endregion
#region Ejercicio3
/* Programa que lea números hasta que se introduzca un cero y escriba la media de los
 * números leídos, sin incluir el 0 en el conteo de números.
 * Se deben usar contadores y acumuladores
 */

namespace Ejercicio3
{
    class programa
    {
        static void Main()
        {
            float valor;
            int contador = 0;
            float sumaNumeros = 0;
            float media;

            do
            {
                Console.Write("Introduzca un valor: ");
                valor = int.Parse(Console.ReadLine());
                if (valor != 0)
                {
                    sumaNumeros = sumaNumeros + valor;
                    contador++;
                }
            } while (valor != 0);

            media = (float)(sumaNumeros / contador);

            Console.WriteLine("\nMEDIA: {0:F2}\n", media);
        }
    }
}
#endregion
#region Ejercicio4
/* Programa que lea notas y que termine con el valor -1. Las notas deben estar incluidas
 * En el rango que va de 0 al 10, descartando y avisando del error si no es una nota
 * permitida. La salida nos mostrará la cantidad de dieces que se han introducido.*/

namespace Ejercicio4
{
    class Program
    {
        static void Main()
        {
            int nota;
            int i = 1;
            int cuentaDieces = 0;

            do
            {
                Console.Write("Introduzca la nota número {0}: ", i);
                nota = int.Parse(Console.ReadLine());
                if (nota == 10)
                {
                    cuentaDieces++;
                    i++;
                }
                else if (nota < -1 || nota > 10)
                    Console.WriteLine("\aNota incorrecta");
                else
                    i++;
            }
            while (nota != -1);

            if (cuentaDieces > 0)
                Console.WriteLine("\nHa(n) habido {0} sobresaliente(s)\n", cuentaDieces);
            else
                Console.WriteLine("\nNo ha habido ningún sobresaliente\n");
        }
    }
}
#endregion
#region Ejercicio5
/* Programa que lee una secuencia de 100 números y nos dice cuántos hay positivos,
 * cuántos negativos y cuantos ceros (Para hacer las pruebas puedes reducir el número
 * de entradas).*/

namespace Ejercicio5
{
    class programa
    {
        static void Main()
        {
            int valor;
            int positivos = 0;
            int negativos = 0;

            for (int cont = 1; cont <= 10; cont++)
            {
                Console.Write("Introduzca valor {0}: ", cont);
                valor = int.Parse(Console.ReadLine());

                if (valor > 0)
                    positivos++;
                else if (valor < 0)
                    negativos++;
            }

            Console.WriteLine("\nNúmeros positivos introducidos: {0}\nNúmeros negativos introducidos: {1}\n", positivos, negativos);
        }
    }
}
#endregion
#region Ejercicio6
/* Programa que lee una secuencia de números no nulos, 
 * terminada con la introducción de un 0, y obtiene e imprime el mayor.*/

namespace Ehercicio6
{
    class Programa
    {
        static void Main()
        {
            int mayor;

            Console.Write("Introduzca valor: ");
            int valor = int.Parse(Console.ReadLine());
            mayor = valor;

            do
            {
                Console.Write("Introduzca valor: ");
                valor = int.Parse(Console.ReadLine());

                if (valor != 0)
                {
                    if (valor > mayor)
                        mayor = valor;
                }

            }
            while (valor != 0);

            Console.WriteLine("\nEl mayor de los valores introducidos es: {0}\n", mayor);
        }
    }
}
#endregion

#endregion
#region Ejercicio7
/* Programa que obtenga el producto de dos números
 * enteros positivos mediante sumas sucesivas. */

namespace Ejercicio7
{
    class Programa
    {
        static void Main()
        {
            int operador1, operador2;
            int total = 0;

            do
            {
                Console.Write("Introduzca operador 1: ");
                operador1 = int.Parse(Console.ReadLine());
                Console.Write("Introduzca operador 2: ");
                operador2 = int.Parse(Console.ReadLine());

                if (operador1 < 0 || operador2 < 0) Console.WriteLine("\aERROR: Sólo se permiten números positivos\n");
            }
            while (operador1 <= 0 || operador2 <= 0);

            Console.WriteLine("\nSumando...");

            for (int i = 1; i <= operador2; i++)
            {
                total = total + operador1;
            }

            Console.WriteLine("\n{0} x {1} = {2}\n", operador1, operador2, total);
        }
    }
}
#endregion
#region Ejercicio8

/* Programa que obtenga el cociente y el resto de
   dos números enteros positivos mediante restas. */

namespace Ejercicio8
{
    class Programa
    {
        static void Main()
        {

            int dividendo, divisor, resto;
            int cociente = 0;

            do
            {
                Console.Write("Introduzca dividendo: ");
                dividendo = int.Parse(Console.ReadLine());
                Console.Write("Introduzca divisor: ");
                divisor = int.Parse(Console.ReadLine());

                if (dividendo < 0 || divisor < 0) Console.WriteLine("\aERROR: Sólo se permiten números positivos\n");
            }
            while (dividendo <= 0 || divisor <= 0);

            resto = dividendo;

            do
            {
                resto = resto - divisor;
                cociente++;
            }
            while (resto >= divisor);

            Console.WriteLine("\n{0} / {1} = {2}\nResto: {3}\n", dividendo, divisor, cociente, resto);
        }
    }
}
#endregion
#region Ejercicio9

/* Haz un programa que muestre en
 * pantalla la tabla de códigos UTF-8*/

namespace Ejercicio9
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("HEX\tDEC\tCHAR");

            int pause = 20;

            for (int i = 1; i <= 255; i++)
            {
                Console.WriteLine("{0:X4}\t{1}\t{2}", i, i, (char)i);

                if (i == pause)
                {
                    Console.Write("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                    pause = pause * 2;
                }
            }
        }
    }
}
#endregion

#region Ejercicio10

/* Programa que determina si dos números enteros positivos
 * son amigos. (Dos números son amigos si la suma de los
 * divisores del primero, excepto él mismo, es igual al
 * segundo, y viceversa).*/

namespace Ejercicio10
{
    class Programa
    {
        static void Main()
        {
            int valor1, valor2;
            int sumaDivisores1 = 0;
            int sumaDivisores2 = 0;
            do
            {
                Console.Write("Introduzca valor 1: ");
                valor1 = int.Parse(Console.ReadLine());
                Console.Write("Introduzca valor 2: ");
                valor2 = int.Parse(Console.ReadLine());

                if (valor1 < 0 || valor2 < 0)
                    Console.WriteLine("\aERROR: Sólo se permiten números positivos\n");
            }
            while (valor1 <= 0 || valor2 <= 0);

            for (int i = 1; i < valor1; i++)
            {
                if (valor1 % i == 0) 
                    sumaDivisores1 += i;
            }

            for (int i = 1; i < valor2; i++)
            {
                if (valor2 % i == 0) 
                    sumaDivisores2 += i;
            }

            bool amigos = sumaDivisores1 == valor2 && sumaDivisores2 == valor1;
            string salida = $"\nLos valores {(!amigos ? "no":"")} son amigos\n";
            Console.WriteLine(salida);
        }
    }
}
#endregion

#region Ejercicio11

/* Simulación de una calculadora. Realiza un programa
 * que sea capaz de sumar, restar, multiplicar y dividir. 
 * El programa presentará un menú, en un bucle para salir
 * con ESC, con las cuatro operaciones que puede realizar.*/

namespace Ejercicio11
{
    class Programa
    {
        static void Main()
        {
            int operador1, operador2;
            int solucion = 0;
            ConsoleKeyInfo opcion;

            do
            {
                Console.WriteLine("MENU\n----\n");
                Console.WriteLine("1. SUMAR\n2. RESTAR\n3. MULTIPLICAR\n4. DIVIDIR\n");
                Console.Write("\nSeleccione opción: ");
                opcion = Console.ReadKey();

                if (opcion.Key != ConsoleKey.Escape)
                {
                    bool error;

                    Console.Write("\n\nIntroduzca operador 1: ");
                    operador1 = int.Parse(Console.ReadLine());
                    Console.Write("Introduzca operador 2: ");
                    operador2 = int.Parse(Console.ReadLine());

                    error = false;

                    switch (opcion.KeyChar)
                    {
                        case '1':
                            solucion = operador1 + operador2;
                            break;
                        case '2':
                            solucion = operador1 - operador2;
                            break;
                        case '3':
                            solucion = operador1 * operador2;
                            break;
                        case '4':
                            if (operador2 == 0)
                                Console.WriteLine("\aERROR: No se permite división por 0\n");
                            else
                                solucion = operador1 / operador2;
                            break;
                        default:
                            Console.WriteLine("\aERROR: Opción no reconocida\n");
                            error = true;
                            break;
                    }

                    if (!error)
                        Console.WriteLine("\nTOTAL: " + solucion + "\n");
                }
            } while (opcion.Key != ConsoleKey.Escape);

            Console.WriteLine("\n");
        }
    }
}
#endregion
#region Ejercicio12

/* Imprimir los múltiplos de 7 que hay entre 7 y 112. */

namespace Ejercicio12
{
    class Programa
    {
        static void Main()
        {
            const int MULTIPLO = 7;
            const int LIMITE = 112;

            string texto = "";
            for (int i = MULTIPLO; i < LIMITE; i++)
            {
                if (i % MULTIPLO == 0)
                    texto += " " + i;
            }
            Console.WriteLine(texto);
        }
    }
}
#endregion
#region Ejercicio13

/* Pide un número, por ejemplo el 4,
 * y saca en pantalla 1223334444. Hazlo con bucles for.*/

namespace Ejercicio13
{
    class Programa
    {
        static void Main()
        {
            Console.Write("Introduzca un numero entero: ");
            int numero = int.Parse(Console.ReadLine());

            for (int i = 1; i <= numero; i++)
                for (int j = 1; j <= i; j++)
                    Console.Write(i);

            Console.WriteLine("\n");
        }
    }
}
#endregion
#region Ejercicio14

/* Lee un número y escribe la suma de sus dígitos.*/

namespace Ejercicio14
{
    class Programa
    {
        static void Main()
        {
            int numeroDeDigitos = 1;
            int sumaDigitos = 0;
            int numero, numeroAuxiliar, numeroDivisionePor10;

            Console.Write("Introduzca un numero: ");
            numero = int.Parse(Console.ReadLine());

            //Averiguar cuantos digitos tiene el numero
            numeroAuxiliar = numero;
            while (numeroAuxiliar / 10 != 0)
            {
                numeroAuxiliar = numeroAuxiliar / 10;
                numeroDeDigitos++;
            }

            //Si tiene 1 digito, numeroDivisionePor10 por 10; si tiene 2 digitos, numeroDivisionePor10 por 100...
            numeroDivisionePor10 = 1;
            for (int i = 1; i < numeroDeDigitos; i++)
            {
                numeroDivisionePor10 = numeroDivisionePor10 * 10;
            }

            //Coger cada digito y sumarlo a sumaDigitos
            while (numeroDivisionePor10 > 0)
            {
                sumaDigitos = sumaDigitos + (numero / numeroDivisionePor10);
                numero = numero % numeroDivisionePor10;
                numeroDivisionePor10 = numeroDivisionePor10 / 10;
            }

            Console.WriteLine("\nNúmero de dígitos: " + numeroDeDigitos);
            Console.WriteLine("\nSuma de dígitos: " + sumaDigitos);
            Console.WriteLine("\n");

        }
    }
}
#endregion
