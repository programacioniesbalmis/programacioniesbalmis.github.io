class Program
{
    private static void Main()
    {
        Dictionary<string, string> capitalesPorPais = new()
        {
            ["España"] = "Madrid", // Par clave país, valor capital.
            ["Portugal"] = "Lisboa",
            ["Francia"] = "Paris",
            ["Irlanda"] = "Dublin"
        };
        // Aunque hemos definido por extensión. Podemos añadir elemetos a posteriori.
        capitalesPorPais.Add("Belgica", "Bruselas");
        capitalesPorPais["Alemania"] = "Berlin";

        // Obtenemos una lista de claves indizable por un entero.
        List<string> paises = [.. capitalesPorPais.Keys];
        // Lista donde almacenaré los países ya preguntados para no repetirnos.
        List<string> paisesPreguntados = [];
        const int NUMERO_PREGUNTAS = 5;
        Random semilla = new();
        int puntos = 0;
        for (int i = 0; i < NUMERO_PREGUNTAS; i++)
        {
            string paisPreguntado;
            // Buscamos un país que ún no hayamos preguntado.
            do
            {
                paisPreguntado = paises[semilla.Next(0, paises.Count)];
            } while (paisesPreguntados.Contains(paisPreguntado));
            paisesPreguntados.Add(paisPreguntado);

            Console.Write($"¿Cual es la capital de {paisPreguntado}? > ");
            string capitalRespondida = Console.ReadLine()!.ToUpper();
            bool respuestaCorrecta = capitalRespondida == capitalesPorPais[paisPreguntado].ToUpper();
            if (respuestaCorrecta) puntos += 2;
            string mensaje = (respuestaCorrecta
                                ? "Correcto !!"
                                : $"Incorrecto !!\nLa respuesta es {capitalesPorPais[paisPreguntado]}.")
                                + $"\nLlevas {puntos} puntos.\n";
            Console.WriteLine(mensaje);
        }
        Console.WriteLine($"Tu nota final es {puntos}.");
    }
}