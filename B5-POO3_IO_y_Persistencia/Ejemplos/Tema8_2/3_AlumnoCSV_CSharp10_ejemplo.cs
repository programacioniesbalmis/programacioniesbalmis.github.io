using System.Text;

public class Alumno
{
    readonly private ulong nia;
    readonly private string nombre;
    readonly private string apellido;

    public ulong GetNIA()
    {
        return nia;
    }
    public string GetNombre()
    {
        return nombre;
    }
    public string GetApellido()
    {
        return apellido;
    }
    public Alumno(in ulong nia, string nombre, string apellido)
    {
        this.nia = nia;
        this.nombre = nombre;
        this.apellido = apellido;
    }
    public override string ToString()
    {
        return $"{GetNIA(),-8}{GetApellido(),-12}{GetNombre(),-12}";
    }
}

public class AlumnoCSV : Alumno
{
    // Una de las posibilidades de gestionar excepciones particulares de una clase 
    // es definir un tipo anidado para ela excepción. De tal manera que cuando
    // la usemos fuera de la misma el tipo será AlumnoCSV.Excepción lo cual
    // me indicará claramente que serán excepciones derivadas de la escritura
    // y lectura del CSV por parte de AlumnoCSV
    public class Excepción : Exception
    {
        public Excepción(string message, Exception inner)
        : base(message, inner) { }
    }

    public AlumnoCSV(in ulong nia, string nombre, string apellido)
    : base(nia, nombre, apellido)
    {
        // Constructor 
    }

    // Método de clase privado que se encargará de leer la próxima línea del 
    // CSV a partir de donde se encuentre situado el descriptor de 
    // StreamReader sr (debería estar al principio de la misma).
    // EVITA QUE SE REPITAN ESTAS LÍNEAS EN EL LEE Y EN EL BUSCA.
    // Al ser privada cualquier excepción producida por la lectura o no correspondencia 
    // de de lo leído en la línea con lo que se espera para poder crear un objeto Alumno
    // será gestionada por los métodos que la usen.
    // Por ejemplo: Una excepción posible podría ser un OutOfBoundsException
    // resultado que que no hay tres elementos en el array tras el Split.
    private static Alumno Lee(StreamReader sr)
    {
        string[] campos = sr.ReadLine()?.Split(new char[] { ';' })
                          ??
                          throw new NullReferenceException("No se ha podido leer Alumno en el CSV");
        return new Alumno(ulong.Parse(campos[0]), campos[1], campos[2]);
    }

    // Recibe una ruta a un fichero CSV de alumnos y me devuelve un array con los
    // alumnos guardados en el mismo o null si no hay ninguno.
    // EL método no puede mostrar nada porque generaríamos una dependencia con
    // la consola que excedería la responsabilidad de la clase y la haría menos
    // reutilizable. Por la misma razón, tampoco devolvemos un string con todos 
    // los alumnos.
    // Quien use el método decidirá como se visualizan.
    public static Alumno[] Lee(string ruta)
    {
        // Inicializamos el array a null.
        Alumno[] alumnos = new Alumno[0];
        try
        {
            // Al usar el using, nos aseguramos que al salir del ámbito de
            // declaración se ha realizado un finally para cerrar los streams
            using FileStream stream = new(ruta, FileMode.Open, FileAccess.Read);
            using StreamReader sr = new(stream, Encoding.UTF8);

            // Leemos y saltamos cabecera de las columnas
            sr.ReadLine();

            while (!sr.EndOfStream)
            {
                Array.Resize(ref alumnos, alumnos?.Length + 1 ?? 1);
                // Añadimos el array leído al hueco que hemos hecho para el mismo.
                alumnos[^1] = Lee(sr);
            }
        }
        catch (Exception e)
        {
            // Relanzo cualquier excepción envolviéndola en un tipo
            // AlumnoCSV.Excepción e indicando el proceso que estaba haciendo
            // para poder saber mejor de donde ha venido la misma.
            // En este momento se que los streams están cerrados porque ya se
            // ha ejecutado el finally implícito por los using.
            throw new Excepción($"Leyendo alumnos de {ruta}", e);
        }
        return alumnos;
    }

    // Método de instancia encargado de guardar el Alumno que en el fichero CSV
    // especificado en la ruta.
    public void Guarda(string ruta)
    {
        try
        {
            // Abrimos con Append que añade al final del fichero o lo crea si no existe.
            using FileStream stream = new(ruta, FileMode.Append, FileAccess.Write);
            using StreamWriter sw = new(stream, Encoding.UTF8);

            // Si el stream se acaba de crear añadiremos la fila con los nombre de
            // las columnas antes de escribir el primer registro.
            if (stream.Length == 0)
                sw.WriteLine($"NIA;Apellido;Nombre");

            // Escribimos la línea y hacemos un flush para asegurarnos de que 
            // se modifique el fichero.
            sw.WriteLine($"{GetNIA()};{GetApellido()};{GetNombre()}");
            sw.Flush();
        }
        catch (Exception e)
        {
            // Relanzo cualquier excepción envolviéndola en un tipo
            // AlumnoCSV.Excepción e indicando el proceso que estaba haciendo
            // para poder saber mejor de donde ha venido la misma.
            // En este momento se que los streams están cerrados porque ya se
            // ha ejecutado el finally implícito por los using.
            throw new Excepción($"Guardando Alumno {this} en {ruta}", e);
        }
    }
    // Método de clase que recibe un NIA y una ruta a un fichero CSV y busca
    // el primer alumno con el NIA especificado.
    // Si no lo encuentra devuelve falso y si lo encuentra devuelve cierto y 
    // una instáncia a un objeto Alumno con los datos del mismo.
    public static bool Busca(in ulong nia, string ruta, out Alumno? alumno)
    {
        alumno = default;
        try
        {
            using FileStream stream = new(ruta, FileMode.Open, FileAccess.Read);
            using StreamReader sr = new(stream, Encoding.UTF8);
            // Leemos y saltamos cabecera de las columnas
            sr.ReadLine();
            while (!sr.EndOfStream && alumno == null)
            {
                // Llamo al método de privado de clase Lee al que le paso
                // el stream apuntado a la lectura de la siguiente línea
                // con los datos de una alumno.
                Alumno a = Lee(sr);
                // Si encuentro el nía establezco el parámetro de salida
                // encontrado y finalizo el bucle.
                if (nia == a.GetNIA())
                    alumno = a;
            }
        }
        catch (Exception e)
        {
            // Relanzo cualquier excepción envolviéndola en un tipo
            // AlumnoCSV.Excepción e indicando el proceso que estaba haciendo
            // para poder saber mejor de donde ha venido la misma.
            // En este momento se que los streams están cerrados porque ya se
            // ha ejecutado el finally implícito por los using.
            throw new Excepción($"Buscando alumno con NIA {nia}", e);
        }

        return alumno != null;
    }
}

class Program
{
    // Enumeración con las opciones del programa para autodocumentar y evitar números mágicos.
    private enum OpciónPrograma { IntroduceAlumno = 1, MostrarAlumnos = 2, BuscarAlumno = 3, Salir = 4 }

    // Modularización de mostrar menú para autodocumentar y simplificar el Main.
    static void Menu()
    {
        Console.Clear();
        Console.WriteLine($"{(int)OpciónPrograma.IntroduceAlumno} - Introduce Alumno.");
        Console.WriteLine($"{(int)OpciónPrograma.MostrarAlumnos} - Mostrar Alumnos.");
        Console.WriteLine($"{(int)OpciónPrograma.BuscarAlumno} - Buscar Alumno.");
        Console.WriteLine($"{(int)OpciónPrograma.Salir} - Salir.");
    }

    // Lee una opción válida de programa.
    static OpciónPrograma Lee()
    {
        bool válida;
        OpciónPrograma o = OpciónPrograma.Salir;
        do
        {
            Console.Write("Introduce una opción: ");
            válida = int.TryParse(Console.ReadLine(), out int valor);
            if (válida)
                válida = Array.IndexOf(Enum.GetValues(typeof(OpciónPrograma)), valor) > 0;
            if (válida)
                o = (OpciónPrograma)valor;
            else
                Console.WriteLine("Opción incorrecta!!");
        }
        while (!válida);
        return o;
    }

    // Modularización del proceso de introducción de un alumno para 
    // autodocumentar y simplificar el Main.
    // Cualquier excepción producida se gestionará en el Main
    static void IntroduceAlumno(string fichero)
    {
        Console.Write("NIA: ");
        ulong nia = ulong.Parse(Console.ReadLine() ?? "0");
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine() ?? "";
        Console.Write("Apellido: ");
        string apellido = Console.ReadLine() ?? "";

        // Creo una instáncia de AlumnoCSV y guardo sus datos en el
        // fichero que me indican por parámetro.
        new AlumnoCSV(nia, nombre, apellido).Guarda(fichero);
        Console.WriteLine("Datos guardados.");
    }

    // Modularización del proceso de buscar y mostrar los datos de un alumno 
    // para autodocumentar y simplificar el Main.
    // Cualquier excepción producida se gestionará en el Main
    static void BuscaAlumno(string fichero)
    {
        // Hago un control preliminar para evitar excepciones obvias que se
        // pueden controlar fácilmente con la lógica del programa.
        if (File.Exists(fichero))
        {
            Console.Write("NIA a buscar: ");
            ulong nia = ulong.Parse(Console.ReadLine() ?? "0");

            // Una ves leído el NIA uso el método de clase definido en AlumnoCSV 
            if (AlumnoCSV.Busca(nia, fichero, out Alumno? a))
                Console.WriteLine($"Los datos del alumno con nia {nia} son:\n{a}");
            else
                Console.WriteLine($"No existe ningún alumno de nia {nia}.");
        }
        else
            Console.WriteLine($"El fichero de datos {fichero} aún no se ha creado.");
    }

    static void Main()
    {
        const string FICHERO = "alumnos.csv";
        OpciónPrograma opción = OpciónPrograma.Salir;

        do
        {
            bool pausaTrasOpción = true;
            try
            {
                Menu();
                // El usar un enum para las opciones de menú. Además de darnos legibilidad
                // Nos asegura que no se va a poder dar el caso default si hemos
                // añadido todas los valores en el switch.
                opción = Lee();
                switch (opción)
                {
                    case OpciónPrograma.IntroduceAlumno:
                        IntroduceAlumno(FICHERO);
                        break;
                    case OpciónPrograma.MostrarAlumnos:
                        if (File.Exists(FICHERO))
                        {
                            var alumnos = AlumnoCSV.Lee(FICHERO);
                            Console.WriteLine(alumnos != null
                            ? string.Join<Alumno>("\n", alumnos)
                            : $"El fichero {FICHERO} está vacío.");
                        }
                        else
                            Console.WriteLine($"El fichero de datos {FICHERO} aún no se ha creado.");
                        break;
                    case OpciónPrograma.BuscarAlumno:
                        BuscaAlumno(FICHERO);
                        break;
                    case OpciónPrograma.Salir:
                        pausaTrasOpción = false;
                        break;
                }
            }
            catch (Exception? e)
            {
                while (e != null)
                {
                    Console.WriteLine(e?.Message);
                    e = e?.InnerException;
                }
            }
            if (pausaTrasOpción)
            {
                Console.Write("Pulsa una tecla...");
                Console.ReadKey();
            }
        } while (opción != OpciónPrograma.Salir);
    }
} // Cierre de la definición de la clase Program
