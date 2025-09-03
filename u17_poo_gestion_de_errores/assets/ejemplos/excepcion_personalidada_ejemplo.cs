public class EmpresaException : Exception
{
    public EmpresaException(string message) : base(message) { }
}

public enum Departamento { Contable, Desarrollo, Marketing };

public class Empresa
{
    public static void ImprimeNominas(Departamento departamento)
    {
        string datosNominas = departamento switch
        {
            Departamento.Contable => "Datos nóminas contabilidad.",
            Departamento.Desarrollo => "Datos nóminas desarrollo.",
            _ => throw new EmpresaException(
                        $"No se pueden imprimir nóminas de este departamento de {departamento}.")
        };
        Console.WriteLine(datosNominas);
    }
}

public class Pinrcipal
{
    public static void Main()
    {
        foreach (Departamento d in Enum.GetValues<Departamento>())
        {
            try
            {
                Empresa.ImprimeNominas(d);
            }
            catch (EmpresaException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}