public class EjemploModularizacion
{
    const string PIEDRA = "PIEDRA";
    const string PAPEL = "PAPEL";
    const string TIJERA = "TIJERA";

    static string PideJugada(string jugador)
    {
        // Jugada a retornar por el jugador.
        string jugada;

        // Flag que me indicará si el Jugador N ha realizado una jugada correcta.
        bool jugadaCorrecta;

        // Establezco el texto de las jugadas para no tener que repetirlo.
        string opciones = $"{PIEDRA}, {PAPEL}, {TIJERA}";
        // Bucle que me irá pidiendo una jugada mientras no sea correcta.
        do
        {
            // Indico el jugador que tiene que jugar y que me llega como parámetro.
            Console.WriteLine($"Jugando {jugador} ...");
            Console.Write($"\tIntroduce tú jugada ({opciones}): ");
            jugada = Console.ReadLine()!.ToUpper();
            jugadaCorrecta = jugada == PIEDRA || jugada == PAPEL || jugada == TIJERA;

            // Si voy a volver a pedir la entrada le indico al jugador su error.
            if (!jugadaCorrecta)
                Console.WriteLine($"\t{jugada} no es una jugada correcta. Debe ser {opciones}");
        } while (!jugadaCorrecta);

        return jugada;
    }

    static string GeneraJugadaMaquina()
    {
        // Habrá muchas formas correctas de implementarlo. Pero por usar la nueva sintaxis de C#8
        // Podemos retornar el resultado de evaluar una expresión switch.
        return new Random().Next(0, 3) switch
        {
            0 => PIEDRA,
            1 => PAPEL,
            2 => TIJERA,
            _ => "Jugada no válida" // Este caso no se podrá dar, aquí deberíamos generar un error.
        };
    }

    static string Resultado(string jugadaUsuario, string jugadaMaquina)
    {
        string resultado;
        if (jugadaMaquina == jugadaUsuario)
        {
            resultado = "Empate";
        }
        else switch (jugadaMaquina)
            {
                case PIEDRA when jugadaUsuario == TIJERA:
                case PAPEL when jugadaUsuario == PIEDRA:
                case TIJERA when jugadaUsuario == PAPEL:
                    resultado = "El ordenador gana";
                    break;
                default:
                    resultado = "El jugador gana";
                    break;
            }

        return resultado;
    }

    static int PideNumeroJugadores()
    {
        bool numeroCorrecto;
        int jugadores;
        do
        {
            Console.Write("Introduce cuantos jugadores van a participar (1 a 4): ");
            string entrada = Console.ReadLine() ?? "1";
            numeroCorrecto = int.TryParse(entrada, out jugadores);
            numeroCorrecto = numeroCorrecto && jugadores >= 1 && jugadores <= 4;
            if (!numeroCorrecto)
                Console.WriteLine($"{entrada} no es correcto. Debe ser un valor entre 1 y 4.");
        } while (!numeroCorrecto);
        return jugadores;
    }

    static void Juega(string jugador)
    {
        // Juega transfiere el control a los 4 módulos en los que lo hemos subdividido
        // en el orden correcto (Izquierda a Derecha)
        // 1.- Pide Jugada a jugador N
        // 2.- Renera Jugada Máquina
        // 3.- Obtén Resultado Jugadas
        // 4.- Muestra el resultado.

        // Al modularizar el módulo queda legible, autodocumentado y ocupa menos de 10 líneas.
        string jugadaUsuario = PideJugada(jugador);
        string jugadaMaquina = GeneraJugadaMaquina();
        string resultado = Resultado(jugadaUsuario, jugadaMaquina);
        Console.WriteLine($"\tEl ordenador ha jugado {jugadaMaquina}\n"
                        + $"\t{jugador} ha jugado {jugadaUsuario}\n"
                        + $"\t{resultado}\n");
    }

    static void JuegaRonda()
    {
        // JuegaRonda transfiere el control a los 2 módulos en los que lo hemos subdividido...
        // 1.- Pide Numero Jugadores
        // 2.- Juega Jugador N

        int jugadores = PideNumeroJugadores();
        for (int i = 0; i < jugadores; i++)
            Juega($"Jugador_{i+1}");
    }
    public static void Main()
    {
        do
        {
            Console.Clear();

            // Podríamos pensar que si incluimos el código de JuegaRonda aquí dentro tampoco
            // quedaría un método muy complejo.
            // Pero tendríamos un bucle dentro de un bucle y eso nos está indicando que ese
            // segundo bucle está haciendo un proceso que a su ves se puede encapsular en 
            // un módulo.
            JuegaRonda();

            Console.WriteLine("¡¡¡ FIN PARTIDA !!!.");
            Console.WriteLine("Pulsa una tecla para jugar otra ronda. ESC para salir.");
        } while (Console.ReadKey().Key != ConsoleKey.Escape);
    }
}
