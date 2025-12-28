
class Program
{
    public static IEnumerable<int> ObtieneMultiplosDeN(
                                int n, int ini, int fin)
    {
        List<int> multimplos = [];
        // Para ello, vamos añadiendo a una colección dichos números.
        for (int i = ini; i < fin; i++)
        {
            if (i % n == 0)
            {
                // Vamos generando un log del proceso.
                Console.WriteLine($"Obtenido {i}");
                multimplos.Add(i);
            }
        }
        // Hemos tenido que rellenar toda la colección y la
        // retornaremos en su forma de secuencia IEnumerable<T>
        return multimplos;
    }

    // En el programa principal, vamos a obtener el 4º múltiplo de 2 entre 320 y 335
    // pero ObtieneMultiplosDeN nos devuelve ya toda la secuencia de múltiplos cargada en memoria.
    public static void Main()
    {
        int cuartoMultObt = ObtieneMultiplosDeN(2, 320, 335).Skip(3).First();
        Console.WriteLine($"El 4to multiplo es {cuartoMultObt}");
    }
}