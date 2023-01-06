public class Departamento
{
    public class NominaGestionadaException : Exception
    {
        public NominaGestionadaException(string message) : base(message) { }
    }
    enum Departamentos { Contable, Desarrollo, Marketing };
    static void ImprimeNominas(Departamentos departamento)
    {
        string datosNominas = departamento switch
        {
            Departamentos.Contable => "Imprimiendo nóminas contabilidad.",
            Departamentos.Desarrollo => "Imprimiendo nóminas Desarrollo.",
            // El tipo de la excepción es más especifico
            // y su definición está dentro de departamento.
            _ => throw new Departamento.NominaGestionadaException(
                        $"No se pueden imprimir nóminas de este departamento de {departamento}.")
        };
        Console.WriteLine(datosNominas);
    }
}