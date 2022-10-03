using System;

namespace Ejercicio1
{
    class Programa
    {
        static bool EsCasillaNegra(in int columna, in int fila)
        {
             return (fila % 2 == 0) ? columna % 2 != 0 : columna % 2 == 0;
        }

        static void DibujaNumeracion(in int columna, in int fila, in int dimensionTablero)
        {
            for (int i = 1; i <= dimensionTablero; i++)
            {
                Console.SetCursorPosition(i + columna, fila);
                Console.Write(i);
                Console.SetCursorPosition(columna, i + fila);
                Console.Write(i);
            }
        }

        static void DibujaCuadricula(in int columna, in int fila, in int dimensionTablero)
        {
            ConsoleColor copiaRespaldoDeColorDeFondo = Console.BackgroundColor;
            for (int fRelativa = 1; fRelativa <= dimensionTablero; fRelativa++)
            {
                for (int cRelativa = 1; cRelativa <= dimensionTablero; cRelativa++)
                {
                    Console.BackgroundColor = EsCasillaNegra(cRelativa, fRelativa)
                                                ? ConsoleColor.Black
                                                : ConsoleColor.White;
                    Console.SetCursorPosition(cRelativa + columna, fRelativa + fila);
                    Console.Write(" ");
                }
            }
            Console.BackgroundColor = copiaRespaldoDeColorDeFondo;
        }

        static void DibujaTablero(in int columna, in int fila, in int dimensionTablero)
        {
            DibujaNumeracion(columna, fila, dimensionTablero);
            DibujaCuadricula(columna, fila, dimensionTablero);
        }

        static (int columna, int fila) PideCasillaPieza(in int columna, in int fila)
        {
            Console.SetCursorPosition(columna, fila);
            Console.Write("Introduce fila: ");
            int f = int.Parse(Console.ReadLine());

            Console.SetCursorPosition(columna, fila + 1);
            Console.Write("Introduce columna: ");
            int c = int.Parse(Console.ReadLine());
            return (c, f);
        }

        static void DibujaDiagonalesAlfil(
                        in int columnaTablero, in int filaTablero,
                        in int columnaPieza, in int filaPieza, 
                        int dimensionTablero)
        {
            for (int fila = 1; fila <= dimensionTablero; fila++)
            {
                for (int columna = 1; columna <= dimensionTablero; columna++)
                {
                    // Asignamos la expresióin a un identificados booleano para autodocumentarla.
                    bool estanEnLaMismaDiagonal = Math.Abs(filaPieza - fila) == Math.Abs(columnaPieza - columna);
                    if (estanEnLaMismaDiagonal)
                    {
                        if (EsCasillaNegra(columna, fila))
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.SetCursorPosition(columna + columnaTablero, fila + filaTablero);
                        Console.Write("*");
                    }
                }
            }
        }

        static void Main()
        {
            // Guardo los colores de la consola y la limpio.
            ConsoleColor fondo = Console.BackgroundColor;
            ConsoleColor primerPlano = Console.ForegroundColor;
            Console.Clear();

            const int DIMENSION_TABLERO = 8;

            const int COLUMNAS_CONSOLA = 80;
            const int FILAS_CONSOLA = 24;

            Console.SetBufferSize(COLUMNAS_CONSOLA, FILAS_CONSOLA);
            Console.SetWindowSize(COLUMNAS_CONSOLA, FILAS_CONSOLA);

            int columnaSuperiorIzquierdaTablero = COLUMNAS_CONSOLA / 2 - DIMENSION_TABLERO / 2;
            int filaSuperiorIzquierdaTablero = FILAS_CONSOLA / 2 - DIMENSION_TABLERO / 2;
            DibujaTablero(
                columnaSuperiorIzquierdaTablero, filaSuperiorIzquierdaTablero, 
                DIMENSION_TABLERO);

            (int columnaAlfil, int filaAlfil) = PideCasillaPieza(
                columnaSuperiorIzquierdaTablero + DIMENSION_TABLERO + 2, 
                filaSuperiorIzquierdaTablero + DIMENSION_TABLERO / 2);

            Console.CursorVisible = false; // Ocultamos el cursor para que no se vea.

            DibujaDiagonalesAlfil(
                columnaSuperiorIzquierdaTablero, filaSuperiorIzquierdaTablero,
                columnaAlfil, filaAlfil, DIMENSION_TABLERO);

            Console.ReadKey(true); // Leemos una tecla sin eco para finalizar
            Console.CursorVisible = true; // Restauro el cursor.

            // Restauro los colores de la consola y la limpio.
            Console.BackgroundColor = fondo;
            Console.ForegroundColor = primerPlano;
            Console.Clear();
        }
    }
}
