using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

public record Estudiante(
    [property: JsonPropertyName("nombre")]
    string Nombre,
    [property: JsonPropertyName("apellido")]
    string Apellido,
    [property: JsonPropertyName("edad")]
    int Edad,
    [property: JsonPropertyName("direccion")]
    Direccion? Direccion = null);

public record Direccion(
    [property: JsonPropertyName("calle")]
    string Calle,
    [property: JsonPropertyName("numero")]
    int Numero,
    [property: JsonPropertyName("ciudad")]
    string Ciudad,
    [property: JsonPropertyName("pais")]
    string Pais);

public record Clase(
    [property: JsonPropertyName("nombre")]
    string Nombre,
    [property: JsonPropertyName("tutor")]
    string Tutor,
    [property: JsonPropertyName("alumnos")]
    IEnumerable<Estudiante> Estudiantes)
{
    public override string ToString() =>
        $"{Nombre} ({Tutor})\n\n" +
        string.Join("\n", Estudiantes.Select(a => $"{a}"));
}

public static class ClaseJson
{
    public static Clase? Recupera(string path)
    {
        using FileStream s = new(path, FileMode.Open, FileAccess.Read);
        return JsonSerializer.Deserialize<Clase>(s);
    }
    public static void Guarda(this Clase clase, string path)
    {
        using FileStream s = new(path, FileMode.Create, FileAccess.Write);
        JsonSerializerOptions options = new()
        {
            WriteIndented = true,
            AllowTrailingCommas = false,
            Encoder = JavaScriptEncoder.Create(
                UnicodeRanges.BasicLatin,
                UnicodeRanges.Latin1Supplement),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        JsonSerializer.Serialize(s, clase, options);
    }

}

public static class Program
{
    public static void Main()
    {
        Clase clase = new(
            Nombre: "1º DAM",
            Tutor: "Juan",
            Estudiantes: [
                new("Pepa", "Pérez", 25),
                new("María", "Peláez", 22),
                new("Rosa", "López", 26),
                new("Juan", "Gómez", 24, new("Calle Falsa", 123, "Elche", "España")),
                new("Luis", "García", 23, new("Calle Real", 456, "Alicante", "España"))
            ]
        );

        clase.Guarda("1DAM.json");
        Clase? c = ClaseJson.Recupera("1DAM.json");
        Console.WriteLine(c?.ToString() ?? "No se ha podido recuperar la clase");
    }
}
