using System.Text;

public class AlumnoEntity
{
    public long Nia { get; }
    public string Apellidos { get; }
    public string Nombre { get; }

    public AlumnoEntity(long nia, string apellidos, string nombre)
    {
        Nia = nia;
        Apellidos = apellidos;
        Nombre = nombre;
    }

    public override string ToString() => $"{Nia,-8}{Apellidos,-32}{Nombre,-32}";
}

public static class AlumnoCSV
{
    const string SEP = ",";

    private static AlumnoEntity Lee(StreamReader sr)
    {
        string linea = sr.ReadLine() 
        ?? throw new NullReferenceException("No se ha podido leer Alumno en el CSV");
        
        // Suponemos que los campos de texto pueden ir entrecomillados
        // pero no contienen comas en su interior
        string[] campos = linea.Split(SEP);
  
        if (campos.Length < 3)
            throw new FormatException("El formato del CSV no es válido");

        for (int i = 1; i < campos.Length; i++)
            campos[i] = campos[i].Trim('"');

        return new AlumnoEntity(
                          nia: long.Parse(campos[0]),
                          apellidos: campos[1],
                          nombre: campos[2]);
    }

    public static IEnumerable<AlumnoEntity> Lee(string ruta)
    {
        using FileStream stream = new(ruta, FileMode.Open, FileAccess.Read);
        using StreamReader sr = new(stream, Encoding.UTF8);
        
        sr.ReadLine(); // Saltamos las columnas de la cabecera
        while (!sr.EndOfStream)
            yield return Lee(sr);
    }

    public static void Guarda(this AlumnoEntity alumno, string ruta)
    {
        using FileStream stream = new(ruta, FileMode.Append, FileAccess.Write);
        using StreamWriter sw = new(stream, Encoding.UTF8);

        if (stream.Length == 0)
            sw.WriteLine($"Nia{SEP}Apellidos{SEP}Nombre");

        sw.WriteLine($"{alumno.Nia}{SEP}\"{alumno.Apellidos}\"{SEP}\"{alumno.Nombre}\"");
        sw.Flush();
    }

    public static AlumnoEntity? Busca(long nia, string ruta)
    {
        AlumnoEntity? alumno = default;
        using FileStream stream = new(ruta, FileMode.Open, FileAccess.Read);
        using StreamReader sr = new(stream, Encoding.UTF8);

        sr.ReadLine();
        while (!sr.EndOfStream && alumno == null)
        {
            AlumnoEntity a = Lee(sr);
            if (nia == a.Nia)
                alumno = a;
        }
        return alumno;
    }
}

public class Program
{
    public static void Menu()
    {
        Console.Clear();
        Console.WriteLine("""
                1 - Introduce Alumno.
                2 - Mostrar Alumnos.
                3 - Buscar Alumno.
                4 - Salir.
                """);
    }

    public static int Lee()
    {
        bool válida;
        int o = 4;
        do
        {
            Console.Write("Introduce una opción: ");
            válida = int.TryParse(Console.ReadLine(), out int valor);
            if (válida)
            {
                válida = valor >= 1 && valor <= 4;
            }
            if (válida)
                o = valor;
            else
                Console.WriteLine("Opción incorrecta!!");
        }
        while (!válida);
        return o;
    }

    public static void IntroduceAlumno(string fichero)
    {
        Console.Write("NIA: ");
        long nia = long.Parse(Console.ReadLine() ?? "0");
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine() ?? "";
        Console.Write("Apellido: ");
        string apellido = Console.ReadLine() ?? "";

        AlumnoEntity alumno = new(nia, apellido,  nombre);
        alumno.Guarda(fichero);
        Console.WriteLine("Datos guardados.");
    }

    public static void BuscaAlumno(string fichero)
    {
        string salida;
        if (File.Exists(fichero))
        {
            Console.Write("NIA a buscar: ");
            long nia = long.Parse(Console.ReadLine() ?? "0");

            AlumnoEntity? a = AlumnoCSV.Busca(nia, fichero);
            if (a != null)
                salida = $"Los datos del alumno con nia {nia} son:\n{a}";
            else
                salida = $"No existe ningún alumno de nia {nia}.";
        }
        else
            salida = $"El fichero de datos {fichero} aún no se ha creado.";

        Console.WriteLine(salida);
    }

    public static void Main()
    {
        const string FICHERO = "alumnos.csv";
        int opcionMenu = 4;

        do
        {
            bool fin = true;
            try
            {
                Menu();
                opcionMenu = Lee();
                switch (opcionMenu)
                {
                    case 1:
                        IntroduceAlumno(FICHERO);
                        break;
                    case 2:
                        string salida;
                        salida = File.Exists(FICHERO) 
                                 ? string.Join("\n", AlumnoCSV.Lee(FICHERO)) 
                                 : $"El fichero de datos {FICHERO} aún no se ha creado.";
                        Console.WriteLine(salida);
                        break;
                    case 3:
                        BuscaAlumno(FICHERO);
                        break;
                    case 4:
                        fin = false;
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Se ha producido un error: {e.Message}");
            }
            if (fin)
            {
                Console.Write("Pulsa una tecla...");
                Console.ReadKey();
            }
        } while (opcionMenu != 4);
    }
}
