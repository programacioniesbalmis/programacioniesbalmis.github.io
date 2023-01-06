// En el try de Main()
// En el try de Método()
// En el try de Método2()
// En el catch de Método2()
// finally de Método2()
// finally de Método()

// finally de Main()

using System;
class MiExcepcion : Exception
{
    public MiExcepcion(string m)
        : base(m)
    {

    }
    public MiExcepcion(string m, Exception ie)
        :base(m, ie)
    {

    }
}

class Excepciones
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
            throw new MiExcepcion("Try método 2");
            //Console.WriteLine("Al final del try de Método2()");
        }
        catch (MiExcepcion e)
        {
            Console.WriteLine("En el catch de Método2()");
            throw new MiExcepcion("Paso por cacth Metodo2", e);
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
        catch (MiExcepcion Mie)
        {
            Exception e = Mie;
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