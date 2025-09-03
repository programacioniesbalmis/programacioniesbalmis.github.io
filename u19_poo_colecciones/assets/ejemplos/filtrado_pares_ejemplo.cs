using System.Diagnostics;

class Ejemplo
{
    static int[] ArrayNumeros()
    {
        const int LIMITE = 50000;
        int[] numeros = new int[LIMITE];
        Random seed = new ();
        for (int i = 0; i < numeros.Length; i++)
            numeros[i] = seed.Next(1, LIMITE + 1);
        return numeros;
    }

    static int[] ObtenerParesArraysYResize(in int[] numeros)
    {
        int[] pares = [];
        for (int i = 0; i < numeros.Length; i++)
            if (numeros[i] % 2 == 0)
            {                
                pares = [..numeros, i];
            }
        return pares;
    }
    static int[] ObtenerParesUsandoListYAdd(in int[] numeros)
    {
        List<int> pares = new();
        for (int i = 0; i < numeros.Length; i++)
            if (numeros[i] % 2 == 0)
               pares.Add(numeros[i]);
        return [.. pares];
    }

    static int[] ObtenerParesUsandoListYRemoveAt(in int[] numeros)
    {
        List<int> lpares = [.. numeros];
        var i = 0;
        while(i < lpares.Count)
        {
            if (lpares[i] % 2 != 0)
                lpares.RemoveAt(i);
            else
                i++;
        }
        return [.. lpares];
    }
    static int[] ObtenerParesUsandoLinkedListYRemove(in int[] numeros)
    {
        LinkedList<int> llpares = new(numeros);
        var it = llpares.First;
        while(it != null)
        {
            var s = it.Next;
            if (it.Value % 2 != 0)
                llpares.Remove(it);
            it = s;
        }
        int[] pares  = new int[llpares.Count];
        llpares.CopyTo(pares, 0);
        return pares;
    }
    public static void Main()
    {
        Stopwatch c = new ();

        int[] numeros = ArrayNumeros();

        c.Start();
        Console.Write("Usando Arrays y Resize, t -> ");
        _ = ObtenerParesArraysYResize(numeros);
        c.Stop();
        Console.WriteLine($"{c.ElapsedMilliseconds} ms");

        c.Restart();
        Console.Write("Usando List y Add, t -> ");
        _ = ObtenerParesUsandoListYAdd(numeros);
        c.Stop();
        Console.WriteLine($"{c.ElapsedMilliseconds} ms");

        c.Restart();
        Console.Write("Usando List y RemoveAt, t -> ");
        _ = ObtenerParesUsandoListYRemoveAt(numeros);
        c.Stop();
        Console.WriteLine($"{c.ElapsedMilliseconds} ms");

        c.Restart();
        Console.Write("Usando LinkedList y Remove, t -> ");
        _ = ObtenerParesUsandoLinkedListYRemove(numeros);
        c.Stop();
        Console.WriteLine($"{c.ElapsedMilliseconds} ms");
    }
}