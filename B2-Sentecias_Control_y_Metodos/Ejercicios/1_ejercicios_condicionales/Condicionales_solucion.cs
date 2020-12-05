using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

// Condicionales 
// ______________________________________________________________________________________

#region Ejercicio 1

/* 
* Se piden dos n?meros, si el primero es mayor que el segundo
* se calcula la resta, 
* en caso contrario se calcula la suma
*/

namespace Ejercicio1
{
    class Program
    {
        static void Main(string[] args)
        {
            int numero1, numero2, resultado;
            string textoOperacion;

            Console.Write("Introduce el primer numero: ");
            numero1 = int.Parse(Console.ReadLine());
            Console.Write("Introduce el segundo numero: ");
            numero2 = int.Parse(Console.ReadLine());

            if (numero1 > numero2)
            {
                textoOperacion = "resta";
                resultado = numero1 - numero2;
            }
            else
            {
                textoOperacion = "suma";
                resultado = numero1 + numero2;
            }

            Console.WriteLine("El resultado de la " + textoOperacion + " es: " + resultado);
        }
    }
}
#endregion

#region Ejercicio 2

/*Pide dos n?meros enteros y di cual es el mayor. Realiza el ejercicio de dos formas
 *diferentes: utilizando un IF y utilizando el operador ternario ?: 
 */

namespace Ejercicio2
{
    class Program
    {
        static void Main(string[] args)
        {
            int numero1, numero2;

            numero1 = int.Parse(Console.ReadLine());
            numero2 = int.Parse(Console.ReadLine());

            int numeroMayor, numeroMenor;

            // Soluci?n 1 con if
            if (numero1 > numero2)
            {
                numeroMayor = numero1;
                numeroMenor = numero2;
            }
            else
            {
                numeroMayor = numero2;
                numeroMenor = numero1;
            }
            Console.Write("El n?mero {0} es mayor que el n?mero {1}\n", numeroMayor, numeroMenor);

            // Soluci?n 2 con ternarios
            numeroMayor = (numero1 > numero2) ? numero1 : numero2;
            numeroMenor = (numero1 > numero2) ? numero2 : numero1;
            Console.Write("El n?mero {0} es mayor que el n?mero {1}\n", numeroMayor, numeroMenor);
        }
    }
}

#endregion

#region Ejercicio 3

/*Una agencia de viajes utiliza para calcular el coste de unas vacaciones, dos premisas: 
 *el tipo de vacaciones y la duraci?n en d?as.
 *Las vacaciones tipo A cuestan 250 Euros por d?a y las vacaciones tipo B cuestan 150 Euros por d?a. 
 *Las vacaciones incluyen el coste por d?a m?s un plus ?nico por cargo de servicio de 50 Euros.**
 *Realiza un programa que solicite el tipo de vacaciones y el n?mero de d?as y visualice en pantalla el total a pagar.*/
namespace Ejercicio3
{
    class Program
    {
        static void Main(string[] args)
        {
            char tipoVacaciones;
            short duracionDias;
            int costeVacaciones=0;

            Console.Write("Introducir el tipo de vacaciones A|B: ");
            tipoVacaciones = char.Parse(Console.ReadLine());
            tipoVacaciones=Char.ToUpper(tipoVacaciones);
            Console.Write("Introducir los d?as de vacaciones: ");
            duracionDias = short.Parse(Console.ReadLine());

            string texto;
            if (tipoVacaciones == 'A')
			{
                costeVacaciones=duracionDias*250;
				texto=$"El precio de tus vacaciones tipo A es de {costeVacaciones} m?s el plus de servicio de 50euros, en total {costeVacaciones+50}euros";
			}
            else if (tipoVacaciones == 'B')
			{
               costeVacaciones=duracionDias*150;
			   texto=$"El precio de tus vacaciones tipo B es de {costeVacaciones} m?s el plus de servicio de 50euros, en total {costeVacaciones+50}euros";
			}
		     else texto=$"Opci?n no valida de vacaciones";

            Console.WriteLine(texto);
		}
    }
}

#endregion



#region Ejercicio 4
/*Se piden cuatro n?meros. Mostrar por pantalla cual es el mayor*/
namespace Ejercicio4
{
    class Program
    {
        static void Main(string[] args)
        {
            
           short numero1, numero2, numero3, numero4;

            Console.Write("Introducir el primer n?mero: ");
            numero1 = short.Parse(Console.ReadLine());
             Console.Write("Introducir el segundo n?mero: ");
            numero2 = short.Parse(Console.ReadLine());
             Console.Write("Introducir el tercer n?mero: ");
            numero3 = short.Parse(Console.ReadLine());
             Console.Write("Introducir el cuarto n?mero: ");
            numero4 = short.Parse(Console.ReadLine());

            string texto;
            if(numero1==numero2 && numero2==numero3 && numero3==numero4)
                texto=$"Los cuatro n?meros son iguales";
            else if (numero1>numero2 && numero1>numero3 && numero1>numero4)
                texto=$"El numero mayor es {numero1} que fu? el primero introducido";
            else if (numero2>numero1 && numero2>numero3 && numero2>numero4)
                texto=$"El numero mayor es {numero2} que fu? el segundo introducido";
            else if (numero3>numero1 && numero3>numero2 && numero3>numero4)
                texto=$"El numero mayor es {numero3} que fu? el tercero introducido";
            else texto=$"El numero mayor es {numero4} que fu? el cuarto introducido";
            Console.WriteLine(texto);
        }
    }
}

#endregion

#region Ejercicio 5

/* 
* Se pide una cantidad y su precio. Hay que hallar el total aplicando un tanto por ciento de
* descuento seg?n la cantidad comprada. 
*/

namespace Ejercicio5
{
    class Program
    {
        // Opción 1
        static void Main()
        {
            double cantidad_Unidades;
            double precio_Eur, precioTotal_Eur;

            Console.Write("Introduce la cantidad en unidades: ");
            cantidad_Unidades = double.Parse(Console.ReadLine());
            Console.Write("Introduce el precio en euros: ");
            precio_Eur = double.Parse(Console.ReadLine());

            precioTotal_Eur = precio_Eur * cantidad_Unidades;
            double descuento_tpu;

            if (cantidad_Unidades <= 10)
                descuento_tpu = 0f;
            else if (cantidad_Unidades <= 30)
                descuento_tpu = 0.05f;
            else if (cantidad_Unidades < 50)
                descuento_tpu = 0.1f;
            else
                descuento_tpu = 0.15f;

            precioTotal_Eur -= precioTotal_Eur * descuento_tpu;

            Console.WriteLine($"El Precio total es: {precioTotal_Eur} E");
        }

        // Opción 2
        static void Main()
        {
            double cantidad_Unidades;
            double precio_Eur, precioTotal_Eur;

            Console.Write("Introduce la cantidad en unidades: ");
            cantidad_Unidades = double.Parse(Console.ReadLine());
            Console.Write("Introduce el precio en euros: ");
            precio_Eur = double.Parse(Console.ReadLine());

            precioTotal_Eur = precio_Eur * cantidad_Unidades;

            ushort descuento_porcentaje = cantidad_Unidades switch
            {
                _ when cantidad_Unidades <= 10 => 0,
                _ when cantidad_Unidades <= 30 => 5,
                _ when cantidad_Unidades <= 50 => 10,
                _ => 15 
            };

            precioTotal_Eur -= precioTotal_Eur * descuento_porcentaje / 100d;

            Console.WriteLine($"El Precio total es: {precioTotal_Eur} E");
        }        
    }   
}
#endregion

#region Ejercicio 6
/*Se pide una letra, si la letra es d o D, se escribir? en la pantalla DESCUENTO, 
 *si la letra es I o i, se escribir? IVA en la pantalla, si la letra es P o p, se escribir?
 *PORCENTAJE  en otro caso se escribir? DATO ERR?NEO. Realiza el ejercicio con switch.*/
namespace Ejercicio6
{
    class Program
    {
        static void Main(string[] args)
        {
            char letra;

            Console.Write("Introducir una letra ");
            letra = char.Parse(Console.ReadLine());        
            string texto;
            switch(letra)
            {
                case 'd': 
                case 'D':
                texto="DESCUENTO";
                break;
                case 'i':
                case 'I':
                texto="IVA";
                break;
                case 'p':
                case 'P':
                texto="PORCENTAJE";
                break;
                default:
                texto="ERRONEO";
                break;
            }
            Console.WriteLine(texto);
        }
    }
}
#endregion

#region Ejercicio 7
/*Gestionamos un hotel. Se pide el n?mero de noches y si quieren habitaci?n 
 *individual (I) o habitaci?n doble (D).  Si el n?mero de noches es mayor de 2
 *y la habitaci?n es individual cobraremos 25? pero si la habitaci?n es doble 
 *cobraremos 40?. Si el n?mero de noches es menor o igual a 2 y la habitaci?n 
 *individual cobraremos 27?, pero si la habitaci?n es doble cobraremos 44?. 
 *Realiza el ejercicio con switch-when
 */
namespace Ejercicio7
{
    class Program
    {
        static void Main()
        {
            char tipoHabitacion;
            short numeroNoches;
            int? precioNoche_Eur;

            Console.Write("Introducir el número de noches:");
            numeroNoches = short.Parse(Console.ReadLine());
            Console.Write("Introducir tipo de habitación individual o doble [I|D]:");
            tipoHabitacion = char.ToUpper(char.Parse(Console.ReadLine()));

            switch (tipoHabitacion)
            {
                case 'I' when numeroNoches > 2:
                    precioNoche_Eur = 25;
                    break;
                case 'I':
                    precioNoche_Eur = 27;
                    break;
                case 'D' when numeroNoches > 2:
                    precioNoche_Eur = 40;
                    break;
                case 'D':
                    precioNoche_Eur = 44;
                    break;
                default:
                    precioNoche_Eur = null;
                    break;
            }
            Console.WriteLine (precioNoche_Eur != null ? $"El precio de su estancia en el hotel es: {precioNoche_Eur * numeroNoches}" : "Opción incorrecta");
        }
    }
}
#region Ejercicio 8

/*          
* Una compa??a de videojuegos te ha contratado para escribir el programa de un videojuego
* nuevo. Deber?s crear la parte del programa que calcula el n?mero total de puntos que un
* jugador gana en el juego Galaxy. Los jugadores acumulan puntos mediante la recolecci?n
* de objetos. Los objetos tienen asignados los siguientes puntos: estrella = 10 puntos,
* planeta = 20 puntos, asteroide = 50 puntos y cometa = 100 puntos. Si un jugador
* acumula m?s de 5.000 puntos, en una misma partida, ganar? un bono de 500 puntos.
* Hacer con switch. 
*/

namespace Ejercicio8
{
    class Programa
    {
        static void Main()
        {
            int puntosAcomulados = 0;

            Console.Write("Introduce el objeto: ");
            string objeto = Console.ReadLine();
            Console.Write("N?mero de objetos acomulados: ");
            int n?meroObjetosAcomunlados = int.Parse(Console.ReadLine());

            int puntosObjeto;
            switch (objeto)
            {
                case "estrella":
                    {
                        puntosObjeto = 10;
                        break;
                    }
                case "planeta":
                    {
                        puntosObjeto = 20;
                        break;
                    }
                case "asteroide":
                    {
                        puntosObjeto = 50;
                        break;
                    }
                case "cometa":
                    {
                        puntosObjeto = 100;
                        break;
                    }
                default:
                    {
                        puntosObjeto = 0;
                        break;
                    }
            }

            int puntosAcomunladosEnEstajugada = puntosObjeto * n?meroObjetosAcomunlados;

            if (puntosAcomunladosEnEstajugada > 5000)
                puntosAcomunladosEnEstajugada += 500;

            puntosAcomulados += puntosAcomunladosEnEstajugada;

            Console.WriteLine("Tus puntos son " + puntosAcomulados);
        }

    }
}
#endregion

#region Ejercicio 9

/* Se pide una nota exacta. Si la nota es 5 se visualizar? el texto APROBADO,
 * Si la nota es 6 se visualizar? el texto BIEN, si la nota es 7 u 8 se visualizar? 
 * el texto NOTABLE, si la nota es 9 o 10 se visualizar? el texto SOBRESALIENTE, 
 * si la nota es 4 o menor se visualizar? el texto SUSPENSO, en otro caso visualizar? 
 * el texto NOTA INCORRECTA.
 * Nota: Usaras el switch de C#8 para conseguir la cadena de salida.
*/

namespace Ejercicio9
{
    class Program
    {
        static void Main()
        {
            double nota;
            int notaRedondeada;
			string textoNota = "";

            Console.Write("Introduce la nota: ");
            nota = double.Parse(Console.ReadLine());
            notaRedondeada = (int)Math.Round(nota);

            textoNota = notaRedondeada switch
            {
                5 => "APROBADO",
                6 => "BIEN",
                7 => "NOTABLE",
                8 => "NOTABLE",
                _ when notaRedondeada>=9 && notaRedondeada<11 => "SOBRESALIENTE",
                _ when notaRedondeada<=4 && notaRedondeada>=0 => "SUSPENSO",
                _ =>  "NOTA INCORRECTA"      
            };
			
			Console.WriteLine("Tienes un " + textoNota);
        }
    }
}

#endregion

#region Ejercicio 10

/*Modifica el programa anterior de forma que ahora adem?s, se deber? de tener en
* cuenta la nota de pr?cticas para realizar la m?dia, siendo ambas exactas. Aunque 
* ahora el resultado ser? una nota num?rica que puede tener decimales, adem?s tanto en 
* las pr?cticas como en los examenes solo se podr? evaluar con tres notas (4, 7, 10). 
* Con todo esto y las siguientes valoraciones, calcula la nota num?rica final:
* (Para hacer este ejercicio deber?s usar switch c#8 de tupla)
* ? Si la nota del examen  es 4, la nota ser? la misma que la del examen independientemente de la de las pr?cticas.
* ? Si la nota del examen es 7 y la de pr?cticas es mayor o igual a 7 la nota ser? la media entre ambas
* ? Si la nota del examen es 7 y la de pr?cticas es 4 la nota final ser? 5
* ? Si la nota del examen es 10 y la de pr?cticas menor o igual a 7 la nota final ser? 9
* ? Si la nota del examen es 10 y la de pr?cticas es 10, la nota final ser? 11
* Se indicar? nota incorrecta en caso de introducir una nota no permitida. Podemos usar una ternaria y la variable notaFinal nulable.
*/
namespace Ejercicio10
{
    class Program
    {
        static void Main()
        {
             double nota;
            int notaRedondeadaExamen, notaRedondeadaPracticas;
            double? notaFinal;

            Console.Write("Introduce la nota del examen: ");
            nota = double.Parse(Console.ReadLine());
            notaRedondeadaExamen = (int)Math.Round(nota);
            Console.Write("Introduce la nota de pr?ctica: ");
            nota = double.Parse(Console.ReadLine());
            notaRedondeadaPracticas = (int)Math.Round(nota);
            if (notaRedondeadaPracticas != 4 && notaRedondeadaPracticas != 7 && notaRedondeadaPracticas != 10
            || notaRedondeadaExamen != 4 && notaRedondeadaExamen != 7 && notaRedondeadaExamen != 10) notaFinal = null;
            else
            {
                notaFinal = (notaRedondeadaExamen, notaRedondeadaPracticas) switch
                {
                    (4, _) => 4,
                    (7, _) when notaRedondeadaPracticas >= 7 => (notaRedondeadaExamen + notaRedondeadaPracticas) / 2,
                    (7, _) => 5,
                    (10, _) when notaRedondeadaPracticas <= 7 => 9,
                    (10, _) => 11,
                    _ => null

                };
            }

            Console.WriteLine(notaFinal != null ? "Tienes un " + notaFinal : "Nota incorrecta");
        }
    }
}

#endregion

#region Ejercicio 11
/* Una empresa factura a sus clientes el ?ltimo d?a de cada mes. Si el cliente
 * paga del 1 al 10 del mes siguiente se le hace un descuento de 50centimos o
 * del 10%, el que sea mayor ; si paga entre los d?as 11 y 20 no se le aplica
 * ning?n descuento, y si paga despu?s del d?a 20 se le penaliza con 1euro o el
 * 5%, lo que sea mayor. Se desea un programa que lea los datos del cliente:
 * nombre, direcci?n y CIF y el importe de la factura y confeccione una carta
 * dirigida al cliente inform?ndole que tiene una factura pendiente de ... euros
 * y lo que deber? pagar seg?n realice el pago del 1 al 10, del 11 al 20 o
 * despu?s del 20. 
 */

namespace Ejercicio11
{
    class Program
    {
        static void Main()
        {
            string nombre, direccion, Cif;
            double importeFactura;
            double descuentoFinalPrimerTercio_Euros, penalizacionFinalTercerTercio_Euros;
            double DESCUENTO_FIJO_PRIMER_TERCIO_EUROS = 0.50d;
            double PENALIZACION_FIJA_TERCER_TERCIO_EUROS = 1d;
            double DESCUENTO_VARIABLE_PRIMER_TERCIO_TPU = 0.10d;
            double PENALIZACION_VARIABLE_TERCER_TERCIO_TPU = 0.05d;

            Console.Write("Introduzca nombre: ");
            nombre = Console.ReadLine();
            Console.Write("Introduzca direccion: ");
            direccion = Console.ReadLine();
            Console.Write("Introduzca CIF: ");
            Cif = Console.ReadLine();
            Console.Write("Introduzca importe de la factura: ");
            importeFactura = double.Parse(Console.ReadLine());

            if (importeFactura * DESCUENTO_VARIABLE_PRIMER_TERCIO_TPU > DESCUENTO_FIJO_PRIMER_TERCIO_EUROS)
                descuentoFinalPrimerTercio_Euros = importeFactura * DESCUENTO_VARIABLE_PRIMER_TERCIO_TPU;
            else
                descuentoFinalPrimerTercio_Euros = DESCUENTO_FIJO_PRIMER_TERCIO_EUROS;

            if (importeFactura * PENALIZACION_VARIABLE_TERCER_TERCIO_TPU > PENALIZACION_FIJA_TERCER_TERCIO_EUROS)
                penalizacionFinalTercerTercio_Euros = importeFactura * PENALIZACION_VARIABLE_TERCER_TERCIO_TPU;
            else
                penalizacionFinalTercerTercio_Euros = PENALIZACION_FIJA_TERCER_TERCIO_EUROS;

            Console.WriteLine(nombre);
            Console.WriteLine(direccion);
            Console.WriteLine("CIF: " + Cif);
            Console.WriteLine(
                            "Le informamos de que tiene un pago pendiente de {0} euros que debera abonar a lo largo del proximo mes.\n" +
                            "Dependiendo de la rapidez en el pago, recibira una bonificacion o penalizacion en el importe final:\n" +
                            "Del 1 al 10 debera abonar un total de {1} Euros\n" +
                            "Del 11 al 20 debera abonar {2} Euros\n" +
                            "A partir del 20 debera abonar {3} Euros.",
                            importeFactura,
                            importeFactura - descuentoFinalPrimerTercio_Euros,
                            importeFactura,
                            importeFactura + penalizacionFinalTercerTercio_Euros);
        }
    }
}
#endregion


#region Ejercicio 12

/* Prepara un programa que lea el cantidadEntregada de la venta y la cantidad entregada
*  por el comprador y calcule los billetes de 100, 50, 20, 10 , 5 y las
*  monedas de 2 y 1 euros y las de 50, 20, 10, 5, 2 y 1 centimo que se necesitan
*  para efectuar la devoluci?n. 
*/

namespace Ejercicio12
{
    class Program
    {
        static void Main()
        {
            double cantidadEntregada, importe;
            double devolucion, devolucion_Euros;
            uint devolucion_Centimos;
            uint numeroBilletesDe100 = 0;
            uint numeroBilletesDe50 = 0;
            uint numeroBilletesDe20 = 0;
            uint numeroBilletesDe10 = 0;
            uint numeroBilletesDe5 = 0;
            uint numeroMonedasDe2euro = 0;
            uint numeroMonedasDe1euro = 0;
            uint numeroMonedasDe50cent = 0;
            uint numeroMonedasDe20cent = 0;
            uint numeroMonedasDe10cent = 0;
            uint numeroMonedasDe5cent = 0;
            uint numeroMonedasDe2cent = 0;
            uint numeroMonedasDe1cent = 0;
            string textoDevolucion;

            Console.Write("Introduce el importe: ");
            importe = double.Parse(Console.ReadLine());
            Console.Write("Introduce la cantidad entregada: ");
            cantidadEntregada = double.Parse(Console.ReadLine());

            bool hayDevolucion = (importe < cantidadEntregada);

            if (hayDevolucion == true)
                devolucion = cantidadEntregada - importe;
            else
                devolucion = importe - cantidadEntregada;

            if (hayDevolucion == true)
                textoDevolucion = "Tienes que devolver en total " + devolucion + " Euros: \n";
            else
                textoDevolucion = "Te faltan por pagar " + devolucion + " Euros: \n";

            devolucion_Euros = devolucion;

            if (devolucion_Euros >= 100)
            {
                numeroBilletesDe100 = (uint)(devolucion_Euros / 100);
                devolucion_Euros -= 100 * numeroBilletesDe100;
            }
            if (devolucion_Euros >= 50)
            {
                numeroBilletesDe50 = (uint)(devolucion_Euros / 50);
                devolucion_Euros -= 50 * numeroBilletesDe50;
            }
            if (devolucion_Euros >= 20)
            {
                numeroBilletesDe20 = (uint)(devolucion_Euros / 20);
                devolucion_Euros -= 20 * numeroBilletesDe20;
            }
            if (devolucion_Euros >= 10)
            {
                numeroBilletesDe10 = (uint)(devolucion_Euros / 10);
                devolucion_Euros -= 10 * numeroBilletesDe10;
            }
            if (devolucion_Euros >= 5)
            {
                numeroBilletesDe5 = (uint)(devolucion_Euros / 5);
                devolucion_Euros -= 5 * numeroBilletesDe5;
            }
            if (devolucion_Euros >= 2)
            {
                numeroMonedasDe2euro = (uint)(devolucion_Euros / 2);
                devolucion_Euros -= 2 * numeroMonedasDe2euro;
            }
            if (devolucion_Euros >= 1)
            {
                numeroMonedasDe1euro = 1;
                devolucion_Euros--;
            }

            devolucion_Centimos = Convert.ToUInt32(devolucion_Euros * 100);

            if (devolucion_Centimos >= 50)
            {
                numeroMonedasDe50cent = (uint)(devolucion_Centimos / 50);
                devolucion_Centimos -= 50 * numeroMonedasDe50cent;
            }
            if (devolucion_Centimos >= 20)
            {
                numeroMonedasDe20cent = (uint)(devolucion_Centimos / 20);
                devolucion_Centimos -= 20 * numeroMonedasDe20cent;
            }
            if (devolucion_Centimos >= 10)
            {
                numeroMonedasDe10cent = (uint)(devolucion_Centimos / 10);
                devolucion_Centimos -= 10 * numeroMonedasDe10cent;
            }
            if (devolucion_Centimos >= 5)
            {
                numeroMonedasDe5cent = (uint)(devolucion_Centimos / 5);
                devolucion_Centimos -= 5 * numeroMonedasDe5cent;
            }
            if (devolucion_Centimos >= 2)
            {
                numeroMonedasDe2cent = (uint)(devolucion_Centimos / 2);
                devolucion_Centimos -= 2 * numeroMonedasDe2cent;
            }
            if (devolucion_Centimos >= 1)
            {
                numeroMonedasDe1cent = 1;
                devolucion_Centimos--;
            }

            //Billetes
            if (numeroBilletesDe100 > 0)
                textoDevolucion += numeroBilletesDe100 + (numeroBilletesDe100 > 1 ? " billetes" : " billete") + " de 100 Euros\n";
            if (numeroBilletesDe50 > 0)
                textoDevolucion += numeroBilletesDe50 + (numeroBilletesDe50 > 1 ? " billetes" : " billete") + " de 50 Euros\n";
            if (numeroBilletesDe20 > 0)
                textoDevolucion += numeroBilletesDe20 + (numeroBilletesDe20 > 1 ? " billetes" : " billete") + " de 20 Euros\n";
            if (numeroBilletesDe10 > 0)
                textoDevolucion += numeroBilletesDe10 + (numeroBilletesDe10 > 1 ? " billetes" : " billete") + " de 10 Euros\n";
            if (numeroBilletesDe5 > 0)
                textoDevolucion += numeroBilletesDe5 + (numeroBilletesDe5 > 1 ? " billetes" : " billete") + " de 5 Euros\n";

            //Monedas de Euro
            if (numeroMonedasDe2euro > 0)
                textoDevolucion += numeroMonedasDe2euro + (numeroMonedasDe2euro > 1 ? " monedas" : " moneda") + " de 2 Euros\n";
            if (numeroMonedasDe1euro > 0)
                textoDevolucion += numeroMonedasDe1euro + (numeroMonedasDe1euro > 1 ? " monedas" : " moneda") + " de 1 Euros\n";

            //C?ntimos
            if (numeroMonedasDe50cent > 0)
                textoDevolucion += numeroMonedasDe50cent + (numeroMonedasDe50cent > 1 ? " monedas" : " moneda") + " de 50 c?ntimos\n";
            if (numeroMonedasDe20cent > 0)
                textoDevolucion += numeroMonedasDe20cent + (numeroMonedasDe20cent > 1 ? " monedas" : " moneda") + " de 20 c?ntimos\n";
            if (numeroMonedasDe10cent > 0)
                textoDevolucion += numeroMonedasDe10cent + (numeroMonedasDe10cent > 1 ? " monedas" : " moneda") + " de 10 c?ntimos\n";
            if (numeroMonedasDe5cent > 0)
                textoDevolucion += numeroMonedasDe5cent + (numeroMonedasDe5cent > 1 ? " monedas" : " moneda") + " de 5 c?ntimos\n";
            if (numeroMonedasDe2cent > 0)
                textoDevolucion += numeroMonedasDe2cent + (numeroMonedasDe2cent > 1 ? " monedas" : " moneda") + " de 2 c?ntimos\n";
            if (numeroMonedasDe1cent > 0)
                textoDevolucion += numeroMonedasDe1cent + (numeroMonedasDe1cent > 1 ? " monedas" : " moneda") + " de 1 c?ntimo\n";

            Console.WriteLine(textoDevolucion);
        }

    }
}
#endregion

