
public record class Jugador(string Nombre, string Apellido, DateOnly Nacimiento);
public record class Alumno(string Nombre, DateTime Nacimiento);

public static class AlumnoExtension
{
    public static Jugador ToJugador(this Alumno alumno) => new(
        Nombre: alumno.Nombre.Split(' ')[0],
        Apellido: alumno.Nombre.Split(' ')[^1],
        Nacimiento: DateOnly.FromDateTime(alumno.Nacimiento));
}

public static class JugadorExtension
{
    public static Alumno ToAlumno(this Jugador jugador) => new(
        Nombre: $"{jugador.Nombre} {jugador.Apellido}",
        Nacimiento: jugador.Nacimiento.ToDateTime(new TimeOnly(0, 0)));
}


class Ejemplo
{
    static void Main()
    {
        Jugador jugador1 = new(
            Nombre: "Lionel",
            Apellido: "Messi",
            Nacimiento: new DateOnly(1987, 6, 24));
        Alumno alumno1 = jugador1.ToAlumno();
        Console.WriteLine(jugador1);
        Console.WriteLine(alumno1);

        Console.WriteLine();

        Alumno alumno2 = new(
            Nombre: "Cristiano Ronaldo",
            Nacimiento: new DateTime(1985, 2, 5));
        Jugador jugador2 = alumno2.ToJugador();
        Console.WriteLine(alumno2);
        Console.WriteLine(jugador2);
    }
}