public class TagId3v1Exception : Exception
{
    public TagId3v1Exception(string message) : base(message) {; }
    public TagId3v1Exception(string message, Exception innerException)
    : base(message, innerException) {; }
}

public record class TagId3V1
{
    const int BYTES_TAG_ID = 3;
    const int BYTES_TITULO = 30;
    const int BYTES_ARTISTA = 30;
    const int BYTES_ALBUM = 30;
    const int BYTES_AÑO = 4;
    const int BYTES_COMENTARIO = 30;
    const int BYTES_GENERO = 1;
    const int TOTAL_BYTES = BYTES_TAG_ID + BYTES_TITULO + BYTES_ARTISTA +
                            BYTES_ALBUM + BYTES_AÑO + BYTES_COMENTARIO + BYTES_GENERO;

    private static readonly string[] generosTagID3v1 =
    [
        "BLUES", "CLASSIC ROCK", "COUNTRY", "DANCE", "DISCO", "FUNK",
        "GRUNGE", "HIP-HOP", "JAZZ", "METAL", "NEW AGE", "OLDIES", "OTHER",
        "POP", "R&B", "RAP", "REGGAE", "ROCK", "TECHNO", "INDUSTRIAL",
        "ALTERNATIVE", "SKA", "DEATH METAL", "PRANKS", "SOUNDTRACK",
        "EURO-TECHNO", "AMBIENT", "TRIP-HOP", "VOCAL", "JAZZ+FUNK", "FUSION",
        "TRANCE", "CLASSICAL", "INSTRUMENTAL", "ACID", "HOUSE", "GAME",
        "SOUND CLIP", "GOSPEL", "NOISE", "ALTERN ROCK", "BASS", "SOUL",
        "PUNK", "SPACE", "MEDITATIVE", "INSTRUMENTAL POP", "INSTRUMENTAL ROCK",
        "ETHNIC", "GOTHIC", "DARKWAVE", "TECHNO-INDUSTRIAL", "ELECTRONIC",
        "POP-FOLK", "EURODANCE", "DREAM", "SOUTHERN ROCK", "COMEDY", "CULT",
        "GANGSTA", "TOP 40", "CHRISTIAN POP", "POP/FUNK", "JUNGLE", "NATIVE AMERICAN",
        "CABARET", "NEW WAVE", "PSYCHADELIC", "RAVE", "SHOWTUNES", "TRAILER", "LO-FI",
        "TRIBAL", "ACID PUNK", "ACID JAZZ", "POLKA", "RETRO", "MUSICAL", "ROCK & ROLL",
        "HARD ROCK"
    ];

    public string Titulo { get; }
    public string Artista { get; }
    public string Album { get; }
    public ushort Año { get; }
    public byte Genero { get; }
    public string Comentario { get; }
    public string EtiquetaGenero => Genero >= 80 ? "OTHER" : generosTagID3v1[Genero];

    private TagId3V1(
                string titulo, string artista, string album,
                ushort año, byte genero, string comentario)
    {
        Titulo = titulo;
        Artista = artista;
        Album = album;
        Año = año;
        Genero = genero;
        Comentario = comentario;
    }


    public override string ToString() => $"""
                                        {"Titulo:",11} {Titulo}
                                        {"Artista:",11} {Artista}
                                        {"Album:",11} {Album}
                                        {"Año:",11} {Año}
                                        {"Genero:",11} {EtiquetaGenero}
                                        {"Comentario:",11} {Comentario}
                                        """;

    public static bool HayTAG(string rutaFichero)
    {
        using FileStream stream = new(rutaFichero, FileMode.Open, FileAccess.Read, FileShare.Read);
        using BinaryReader streamRB = new(stream);

        bool hayTAG = false;
        // Habrá TAG si como mínimo el fichero mide más de los 128 bytes que ocupa el mismo.
        hayTAG = stream.Length > TOTAL_BYTES;
        if (hayTAG)
        {
            // Se que ocupa esos bytes y me desplazo donde en teoría está el principio del TAG
            stream.Seek(-TOTAL_BYTES, SeekOrigin.End);
            // Leo los 3 caracteres del TAG y los transformo a cadena.
            string tag = new(streamRB.ReadChars(BYTES_TAG_ID));
            hayTAG = tag == "TAG";
        }
        return hayTAG;
    }

    public static TagId3V1 LeeTAG(string rutaFichero)
    {
        string error = $"El fichero {rutaFichero} no tiene información válida de TagId3v1";
        // Si llamo a este método es porque estoy seguro de que hay 'tag' para leer
        // en caso contrario generaré una excepción.
        if (!HayTAG(rutaFichero))
            throw new TagId3v1Exception(error);

        using FileStream stream = new(rutaFichero, FileMode.Open, FileAccess.Read);
        using BinaryReader streamBR = new(stream);
        try
        {
            // Me sitúo al principio del título justo después de los bytes ['T']['A']['G']
            stream.Seek(BYTES_TAG_ID - TOTAL_BYTES, SeekOrigin.End);

            // Como pude haber espácios hasta rellenar lo que ocupa cada valor 
            // los recorto con el método Trim
            string titulo = new string(streamBR.ReadChars(BYTES_TITULO)).Trim();
            string artista = new string(streamBR.ReadChars(BYTES_ARTISTA)).Trim();
            string album = new string(streamBR.ReadChars(BYTES_ALBUM)).Trim();
            ushort año = ushort.Parse(new string(streamBR.ReadChars(BYTES_AÑO)).Trim());
            string comentario = new string(streamBR.ReadChars(BYTES_COMENTARIO)).Trim();
            byte genero = streamBR.ReadByte();

            return new TagId3V1(titulo, artista, album, año, genero, comentario);
        }
        catch (Exception e)
        {
            throw new TagId3v1Exception(error, e);
        }
    }
}


public class Program
{
    public static void Main()
    {
        string[] ficheros = Directory.GetFiles(@"C:\Musica", "*.mp3");
        foreach (var fichero in ficheros)
        {
            try
            {
                Console.WriteLine($"Información del fichero {fichero}...");
                string informacion = (TagId3V1.HayTAG(fichero)
                                    ? TagId3V1.LeeTAG(fichero).ToString()
                                    : "\tNo contiene información."
                                     ) + "\n";
                Console.WriteLine(informacion);
            }
            catch (TagId3v1Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
