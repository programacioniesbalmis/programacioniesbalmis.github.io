using System;

namespace ejercicioBuclesAvanzados
{
#region Ejercicio1

namespace Ejercicio1
{
    class Programa
    {
        static void Main()
        {
            // Muestra Tablero

            for (int i = 1; i <= 8; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write(i);
                Console.SetCursorPosition(0, i);
                Console.Write(i);
            }

            for (int fila = 1; fila <= 8; fila++)
            {
                for (int columna = 1; columna <= 8; columna++)
                {
                    bool esCasillaNegra;

                    if (fila % 2 == 0)
                        esCasillaNegra = (columna % 2 == 0) ? false : true;
                    else
                        esCasillaNegra = (columna % 2 == 0) ? true : false;

                    if (esCasillaNegra)
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    else
                        Console.BackgroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(columna, fila);
                    Console.Write(" ");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("\n");

            // Pide Posición Alfil
            int columnaAlfil;
            int filaAlfil;

            Console.SetCursorPosition(10, 4);
            Console.Write("Introduce fila: ");
            filaAlfil = int.Parse(Console.ReadLine());

            Console.SetCursorPosition(10, 5);
            Console.Write("Introduce columna: ");
            columnaAlfil = int.Parse(Console.ReadLine());

            // Mustra posición alfil
            for (int fila = 1; fila <= 8; fila++)
            {
                for (int columna = 1; columna <= 8; columna++)
                {
                    if (Math.Abs(filaAlfil - fila) == Math.Abs(columnaAlfil - columna)) // Es diagonal del alfil?
                    {
                        bool esCasillaNegra;

                        Console.SetCursorPosition(columna, fila);

                        if (fila % 2 == 0)
                            esCasillaNegra = (columna % 2 == 0) ? false : true;
                        else
                            esCasillaNegra = (columna % 2 == 0) ? true : false;

                        if (esCasillaNegra)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write("*");
                    }
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 15);
            Console.Write("\n");
        }
    }
}
#endregion
#region Ejercicio2

namespace Ejercicio2
{
    class Programa
    {
        static void Main()
        {
            // Muestra Tablero

            for (int i = 1; i <= 8; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write(i);
                Console.SetCursorPosition(0, i);
                Console.Write(i);
            }

            for (int fila = 1; fila <= 8; fila++)
            {
                for (int columna = 1; columna <= 8; columna++)
                {
                    bool esCasillaNegra;

                    if (fila % 2 == 0)
                        esCasillaNegra = (columna % 2 == 0) ? false : true;
                    else
                        esCasillaNegra = (columna % 2 == 0) ? true : false;

                    if (esCasillaNegra)
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    else
                        Console.BackgroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(columna, fila);
                    Console.Write(" ");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("\n");

            // Pide Posición Caballo
            int columnaCaballo;
            int filaCaballo;

            Console.SetCursorPosition(10, 4);
            Console.Write("Introduce fila: ");
            filaCaballo = int.Parse(Console.ReadLine());

            Console.SetCursorPosition(10, 5);
            Console.Write("Introduce columna: ");
            columnaCaballo = int.Parse(Console.ReadLine());

            // Mustra posición caballo
            for (int fila = 1; fila <= 8; fila++)
            {
                for (int columna = 1; columna <= 8; columna++)
                {
                    if (Math.Abs(fila - filaCaballo) == 2 && Math.Abs(columna - columnaCaballo) == 1 ||
                        Math.Abs(fila - filaCaballo) == 1 && Math.Abs(columna - columnaCaballo) == 2 ||
                        fila == filaCaballo && columna == columnaCaballo)
                    {
                        bool esCasillaNegra;

                        Console.SetCursorPosition(columna, fila);

                        if (fila % 2 == 0)
                            esCasillaNegra = (columna % 2 == 0) ? false : true;
                        else
                            esCasillaNegra = (columna % 2 == 0) ? true : false;

                        if (esCasillaNegra)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.Write("*");
                    }
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 15);
            Console.Write("\n");
        }
    }
}
#endregion
#region Ejercicio3

/* Construir un triángulo de n filas con números 
1
23
345
4567
56789
678901
 */
namespace Ejercicio3
{
    class Programa
    {
        static void Main()
        {
            Console.Write("Introduzca el número de filas: ");
            int filas = int.Parse(Console.ReadLine());

            for (int i = 1; i <= filas; i++)
            {
                string textoFila = "";
                for (int j = i; j < i + i; j++)
                    textoFila += (j < 10) ? j : j % 10;
                Console.WriteLine(textoFila);
            }
        }
    }
}
#endregion
#region Ejercicio4

/* Igual que el anterior pero para este otro triángulo 
3
58
703
9258
 */

namespace Ejercicio4
{
    class Program
    {
        static void Main()
        {
            Console.Write("Introduzca el número de filas: ");
            int filas = int.Parse(Console.ReadLine());

            const int INCREMETO_FILA = 3;
            const int INCREMETO_COLUMNA = 2;
            int limiteFila = 1;
            int valor = 3;
            int comienzo = 3;

            for (int i = 1; i <= filas; i++)
            {
                string textoFila = "";
                for (int j = 1; j <= limiteFila; j++)
                {
                    if (valor >= 10)
                        valor = valor - 10;

                    textoFila += valor;
                    valor = valor + INCREMETO_FILA;
                }
                Console.WriteLine(textoFila);

                comienzo = comienzo + INCREMETO_COLUMNA;
                if (comienzo >= 10)
                    comienzo = 0;
                valor = comienzo;
                limiteFila++;
            }
        }
    }
}
#endregion
    #region Ejercicio5
    // Una progresión aritmética tiene el siguiente término general an=3n-2. 
    // Se desea un programa que pida un número k y calcule los k primeros términos.
    // Ejemplo: k=5 a1=1, a2=4, a3=7, a4=10, a5=13.
    class ejercicio5
    {
        static void Main()
        {
            Console.Write("Introduce un numero: ");
            int numeroTerminos = int.Parse(Console.ReadLine());

            for (int i = 1; i <= numeroTerminos; i++)
            {
                int terminoProgresion = (3 * i) - 2;
                Console.Write("a{0} = {1} ", i, terminoProgresion);
            }
        }
    }
    #endregion

    #region Ejercicio6
    //Programa en C# la generación de una tabla de verdad con unos y ceros con un bucle for anidado. 
    //Las operaciones que mostrará la tabla serán por tanto operaciones de bit. 
    //Para ello el usuario deberá introducir por teclado un carácter con el cual nos indicará la tabla a generar (&, |, ^).
    class ejercicio6
    {
        static void Main()
        {
            char operacion;

            do
            {
                Console.Write("Introduce una operación de bit (&, |, ^): ");
                operacion = char.Parse(Console.ReadLine());
            }
            while (operacion != '&' && operacion != '|' && operacion != '^');

            for (int op1 = 0; op1 <= 1; ++op1)
            {
                for (int op2 = 0; op2 <= 1; ++op2)
                {
                    int resultado;

                    switch (operacion)
                    {
                        case '&':
                            resultado = op1 & op2;
                            break;
                        case '|':
                            resultado = op1 | op2;
                            break;
                        case '^':
                            resultado = op1 ^ op2;
                            break;
                        default:
                            resultado = -1;
                            Debug.Assert(false, "Operación no válida");
                            break;
                    }
                    Console.WriteLine("{0:D1} {1} {2:D1} = {3:D1}", op1, operacion, op2, resultado);
                }
            }
        }
    }
    #endregion

    #region Ejercicio7
    //Escribe un programa que genere la secuencia de números:
    //1, 2, 1, 2, 3, 1, 2, 3, 4, 1, 2, 3, 4, 5, ..., 1, 2, 3, ... n
    class ejercicio7
    {
        static void Main(string[] args)
        {
            Console.Write("Introduce un numero: ");
            int numero = int.Parse(Console.ReadLine());

            for (int i = 2; i <= numero; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write("{0}, ", j);
                }
            }
            Console.WriteLine();
        }
    }
    #endregion

    #region Ejercicio8
    // Un número se dice que es capicúa si leído de derecha a izquierda da el mismo resultado que leído de izquierda a derecha. 
    // Por ejemplo, los números 22, 343, 5665 o 12321 son capicúas. 
    // Elabora un programa que lea desde teclado un número entero mayor de 9 y devuelva si el número es capicúa o no.
    class ejercicio8
    {
        static void Main()
        {
            Console.Write("Introduce un número: ");
            int numero = int.Parse(Console.ReadLine());

            int numDigitos = numero.ToString().Length;

            int izq = (int)Math.Pow(10, numDigitos - 1);
            int der = 10;
            bool capicua = true;
            for (int i = 0; i < numDigitos / 2; i++)
            {
                int numeroIzq = (numero / izq);
                int numeroDer = (numero % der);
                if (i > 0)
                {
                    numeroIzq %= 10;
                    numeroDer /= (int)Math.Pow(10, i);
                }

                if (numeroIzq != numeroDer)
                {
                    capicua = false;
                    break;

                }
                izq /= 10;
                der *= 10;
            }
            Console.WriteLine(capicua ? "ES CAPICUA" : "NO ES CAPICUA");
        }
    }
    #endregion

    #region Ejercicio9
    // Crea un programa que muestre en pantalla la siguiente
    //pirámide:
    //     1
    //    232
    //   34543
    //  4567654
    // 567898765
    //67890109876
    class ejercicio9
    {
        static void Main()
        {
            int PROFUNDIDAD = 6;
            int numDatosNivel = 1;

            for (int i = 1; i <= PROFUNDIDAD; ++i)
            {
                int cuenta = i;
                for (int k = cuenta; k <= PROFUNDIDAD; ++k)
                    Console.Write(" ");
                for (int j = 1; j <= numDatosNivel; ++j)
                {
                    Console.Write(cuenta % 10);
                    if (j > numDatosNivel / 2)
                        cuenta--;
                    else
                        cuenta++;
                }
                Console.WriteLine();
                numDatosNivel += 2;
            }
        }
    }
    #endregion



    #region Ejercicio10
    // Haz programa de terminal que usando algún tipo de bucle.
    // Determine la ubicación de un número mayor que cero (leído del teclado) 
    // en una lista de números mayores que cero leída del teclado (lista creciente estrictamente y que finalizará con un 0)
    // Ejemplos de ejecución con:
    // Entrada (Número buscado) en (Lista) →
    // 2 en 3 5 6 7 8 0
    // Fuera de lista a la Izquierda
    // 8 en 1 3 5 7 0
    // Fuera de lista a la Derecha
    // 4 en 1 3 4 6 8 0
    // En la lista
    // 5 en 1 3 4 7 0
    // Fuera de lista a la Intercalado
    class ejercicio10
    {
        static void Main()
        {
            string textoEstado = "";
            int numeroEnLista;
            int numeroEnListaAnterior = 0;
            int i = 1;

            Console.Write("Introduce un número ");
            int numero = int.Parse(Console.ReadLine());
            Console.WriteLine(" en");

            do
            {
                Console.Write("Numero {0} en lista: ", i);
                numeroEnLista = int.Parse(Console.ReadLine());

                if (numeroEnLista != 0)
                {
                    if (numeroEnLista <= numeroEnListaAnterior)
                    {
                        Console.WriteLine("Los números en la lista deben ser crecientes.\nVuelve a introducir el número.");
                    }
                    else
                    {
                        if (numeroEnLista == numero)
                            textoEstado = "está en la lista.";
                        else if (textoEstado == "" && numeroEnListaAnterior == 0 && numeroEnLista > numero)
                            textoEstado = "está a la izquierda.";
                        else if (textoEstado == "" && numeroEnListaAnterior < numero && numeroEnLista > numero)
                            textoEstado = "está en intercalado.";

                        numeroEnListaAnterior = numeroEnLista;
                        i++;
                    }
                }
                else if (textoEstado == "")
                    textoEstado = "está a la derecha.";
            } while (numeroEnLista != 0);

            if (i == 1)
                Console.WriteLine("La lista está vacía.");
            else
                Console.WriteLine("El número " + numero + " " + textoEstado);
        }
    }
    #endregion
}

