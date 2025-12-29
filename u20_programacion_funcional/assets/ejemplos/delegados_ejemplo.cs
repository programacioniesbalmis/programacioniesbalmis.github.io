class Principal
{
    // Definición del tipo delegado Operacion
    public delegate double Operacion(double op1, double op2);

    // Definición de métodos de clase con la misma signatura.
    public static double Suma(double op1, double op2) => op1 + op2;
    public static double Multiplica(double op1, double op2) => op1 * op2; 

    // Recibe un objeto delegado del tipo Operación
    // Esto es, la 'estrategia' aseguir para operar con los arrays.
    public static double[] OperaArrays(
                    double[] ops1, double[] ops2,
                    Operacion operacion)
    {
        double[] resultados = new double[ops1.Length];
        for (int i = 0; i < resultados.Length; ++i)
            resultados[i] = operacion(ops1[i], ops2[i]);
        return resultados;
    }

    public static void Main()
    {
        double[] ops1 = [ 5, 4, 3, 2, 1 ];
        double[] ops2 = [ 1, 2, 3, 4, 5 ];

        // Pasamos el nombre (id) la de función ya que Suma es de tipo Operacion
        Operacion operacion = Suma;
        double[] sumas = OperaArrays(ops1, ops2, operacion);
        Console.WriteLine($"Sumas: {string.Join(" ", sumas)}");

        // También podemos pasar el nombre de la función como parámetro real.
        double[] multiplicaciones = OperaArrays(ops1, ops2, Multiplica);
        Console.WriteLine($"Multiplicaciones: {string.Join(" ", multiplicaciones)}");
    }
}