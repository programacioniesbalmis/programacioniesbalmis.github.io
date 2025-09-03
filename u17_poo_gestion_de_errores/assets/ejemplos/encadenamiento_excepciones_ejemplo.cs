// En el try de Main()
// En el try de Método()
// En el try de Método2()
// En el catch de Método2()
// finally de Método2()
// finally de Método()

// finally de Main()


public class MiException : Exception
{
    public MiException(string m)
        : base(m)
    {

    }
    public MiException(string m, Exception ie)
        :base(m, ie)
    {

    }
}

public static class Excepciones
{
    public static void Metodo()
    {
        try
        {            
            Console.WriteLine("En el try de Método()");
            Método2();
            Console.WriteLine("Al final del try de Método()");
        }
        catch (OverflowException)
        {
            Console.WriteLine("En el catch de Método()");
        }
        finally
        {
            Console.WriteLine("finally de Método()");
        }
    }
    public static void Método2()
    {
        try
        {
            Console.WriteLine("En el try de Método2()");
            throw new MiException("Try método 2");
            //Console.WriteLine("Al final del try de Método2()");
        }
        catch (MiException e)
        {
            Console.WriteLine("En el catch de Método2()");
            throw new MiException("Paso por cacth Metodo2", e);
        }
        finally
        {
            Console.WriteLine("finally de Método2()");
        }
    }



    public static void Main()
    {
        try
        {
            Console.WriteLine("En el try de Main()");
            Metodo();
            Console.WriteLine("Al final del try de Main()");
        }
        catch (MiException mie)
        {
            Exception e = mie;
            string m = e.Message + "\n\tEn el catch de Main()";
            while(e.InnerException != null)
            {
                m = e.InnerException.Message + "\n\t" + m;
                e = e.InnerException;
            }
            Console.WriteLine(m);
        }
        finally
        {
            Console.WriteLine("finally de Main()");
        }
    }
}