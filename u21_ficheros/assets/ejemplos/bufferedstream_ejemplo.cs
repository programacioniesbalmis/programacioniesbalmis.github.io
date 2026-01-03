using System.Diagnostics;

public class CasoDeEstudio
{
    static void Main()
    {
        Stopwatch cronometro = new ();

        cronometro.Start();
        FileStream fichero = new ("prueba.txt", FileMode.Create, FileAccess.Write);
        // Voy escribiendo bytes y cuando se llene el buffer del FileStream que yo no controlo
        // se realizará un volcado (flush) del mismo en el disco.
        for (int i = 0; i < 100000000; i++)
            fichero.WriteByte(33);
        fichero.Close();
        cronometro.Stop();
        Console.WriteLine($"Sin BufferedStream milisegundos = {cronometro.ElapsedMilliseconds}ms");

        cronometro.Reset();
        cronometro.Start();
        fichero = new FileStream("prueba.txt", FileMode.Create, FileAccess.Write);
        // Añado un decorador que me añade la posibilidad de gestionar un buffer de forma
        // 'transparente' antes del mandar los bytes al FileStream.
        // Le añado una capacidad de almacenaje en memoria antes de volcado de 100 bytes
        // que es bastante menor que la que tiene el FileStream por defecto. Lo cual 
        // ralentizará muchísimo la escritura porque se realizarán más volcados o (flush). 
        // en el disco que es una operación extremádamente costosa.                     
        BufferedStream ficheroBuff = new (fichero, 100);

        for (int i = 0; i < 100000000; i++)
            ficheroBuff.WriteByte(33);
        ficheroBuff.Close();
        cronometro.Stop();
        Console.WriteLine($"Sin BufferedStream milisegundos = {cronometro.ElapsedMilliseconds}ms");
    }

}
