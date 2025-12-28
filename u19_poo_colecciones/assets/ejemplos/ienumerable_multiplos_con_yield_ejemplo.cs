
class Program
{
    static IEnumerable<int> ObtieneMultiplosDeN(int n, int ini, int fin)
    {
        for (int i = ini; i < fin; i++)
        {
            if (i % n == 0)
            {
                Console.WriteLine($"Producido {i}");
                yield return i;
            }
        }
    }

    public static void Main()
    {
        int cuartoMultObt = ObtieneMultiplosDeN(2, 320, 335).Skip(3).First();
        Console.WriteLine($"El 4to multiplo es {cuartoMultObt}");
    }
}