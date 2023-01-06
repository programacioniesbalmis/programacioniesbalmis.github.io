class DepartamentoException : Exception
{
    public DepartamentoException(string message) : base(message) { }
}

public class Departamento
{
    enum Departamentos { Contable, Desarrollo, Marketing };
    static void ImprimeNominas(Departamentos departamento)
    {
        string datosNominas = departamento switch
        {
            Departamentos.Contable => "Datos nóminas contabilidad.",
            Departamentos.Desarrollo => "Datos nóminas Desarrollo.",
            // En el default lanzamos muestra propia excepción. 
            _ => throw new DepartamentoException(
                        $"No se pueden imprimir nóminas de este departamento de {departamento}.")
        };
        Console.WriteLine(datosNominas);
    }
}

class Pinrcipal
{
    static void Main()
    {
        ;
    }
}