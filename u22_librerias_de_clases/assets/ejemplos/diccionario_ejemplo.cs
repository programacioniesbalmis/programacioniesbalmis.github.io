using System;
using System.Linq;
using System.Text.RegularExpressions;
using WeCantSpell.Hunspell;

public class Program 
{
    static void Main() 
    {
        Console.Write("Cargando diccionario...\n");
        string rutaEjecucion = Regex.Match(
                                        input: Directory.GetCurrentDirectory(),
                                        pattern: @"^(?<ruta>.*?)(?=\\bin)",
                                        options: RegexOptions.IgnoreCase
                                    ).Groups["ruta"].Value;       
        WordList diccionario = WordList.CreateFromFiles(Path.Combine(rutaEjecucion, "Spanish.dic"));
        string palabra;
        do
        {
            Console.Write("Introduce una palabra: ");
            palabra = Console.ReadLine()!;         

            string mensaje;
            // Si la palabra no está en el diccionario en español.
            if (!diccionario.Check(palabra))
            {
                mensaje = $"{palabra} no es correcta.\n";
                // Le pido al objeto diccionario en castellano que me devuelva
                // un array de sugerencias posibles a partir de la palabra introducida.
                string[] sugerencias = [.. diccionario.Suggest(palabra)];
                if (sugerencias.Length > 0)
                    mensaje += $"¿Quisiste decir {string.Join(", ", sugerencias)}?\n";
            }
            else
                mensaje = $"{palabra} es correcta.\n";
            Console.WriteLine(mensaje);

        } while (palabra != "adios");
    }
}