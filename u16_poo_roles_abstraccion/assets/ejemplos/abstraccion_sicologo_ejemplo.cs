using System.Text;

namespace CasoDeEstudioSicologo;

public abstract class Paciente
{
    public string Nombre { get; }
    public abstract string Respuesta { get; }

    public Paciente(string nombre)
    {
        Nombre = nombre;
    }
}

public class PacienteAlegre : Paciente
{
    public override string Respuesta
    => "Pues... ahora estoy alegre.";

    public PacienteAlegre(string nombre)
    : base(nombre) { ; }
}

public class PacienteTriste : Paciente
{
    public override string Respuesta
    => "Pues... ahora estoy triste.";

    public PacienteTriste(string nombre)
    : base(nombre) { ; }
}


public class PacienteSociopata : Paciente
{
    public override string Respuesta 
    => "Vas a morir .. muuhaaahahahaha !!";

    public PacienteSociopata(string nombre) 
    : base(nombre) { ; }
}

public class Consulta
{
    private List<Paciente> Pacientes { get; }
    public Consulta()
    {
        Pacientes = [];
    }

    public Consulta Entra(Paciente p)
    {
        Pacientes.Add(p);
        return this;
    }
    public Paciente? Siguiente
    {
        get
        {
            Paciente? p = Pacientes.FirstOrDefault();
            if (p != null)
                Pacientes.RemoveAt(0);
            return p;
        }
    }
}

public class Sicologo
{
    public Consulta Consulta { get; }
    public Sicologo(Consulta consulta)
    {
        Consulta = consulta;
    }
    private static string Diagnostico(Paciente p) => p switch
    {
        PacienteAlegre _ => $"{p.Nombre} le veo estupendamente. Enhorabuena!! no necesita más terapia.",
        PacienteTriste _ => $"{p.Nombre} tome fluoxetina 20mg y vuelva en un mes.",
        PacienteSociopata _ => $"Lo siento!. Debo aplicarte una decarga de 10000V justo ahora.",
        _ => $"{p.Nombre} déjeme que estudie un poco más su caso y vuelva la semana que viene."
    };

    private static void Atiende(Paciente p)
    {
        StringBuilder proceso = new();
        proceso.AppendLine("- Sicólogo: Buenos días!. ¿Cómo se llama?")
                .AppendLine($"- Paciente: Soy {p.Nombre}")
                .AppendLine($"- Sicólogo: Dígame {p.Nombre}!.. ¿Qué siente?")
                .AppendLine($"- Paciente: {p.Respuesta}")
                .AppendLine($"- Sicólogo: {Diagnostico(p)}")
                .AppendLine("- Sicólogo: Que pase el siguiente !!!");
        Console.WriteLine(proceso);
    }

    public void PasaConsulta()
    {
        Paciente? p;
        while ((p = Consulta.Siguiente) != null)
            Atiende(p);
    }
}

public static class Principal
{
    public static void Main()
    {
        Sicologo sicologo = new (consulta : new ());
        sicologo.Consulta
                .Entra(new PacienteAlegre("Xusa"))
                .Entra(new PacienteSociopata("Charles"))
                .Entra(new PacienteAlegre("Juanjo"))
                .Entra(new PacienteTriste("Carmen"));
        sicologo.PasaConsulta();
    }
}
