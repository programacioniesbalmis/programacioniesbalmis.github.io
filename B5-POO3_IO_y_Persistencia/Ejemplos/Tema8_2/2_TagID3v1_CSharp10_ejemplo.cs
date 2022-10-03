public class TagId3v1Exception : Exception
{
    public TagId3v1Exception(string message) : base(message) {; }
    public TagId3v1Exception(string message, Exception innerException)
    : base(message, innerException) {; }
}

class TagId3V1
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

    private static readonly string[] generosTagID3v1 = new string[]
    {
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
    };
    private string titulo;
    private string artista;
    private string album;
    private ushort año;
    private byte genero;
    private string comentario;

    public string GetTitulo()
    {
        return titulo;
    }
    public string GetArtista()
    {
        return artista;
    }
    public ushort GetAño()
    {
        return año;
    }
    public string GetComentario()
    {
        return comentario;
    }
    public string GetAlbum()
    {
        return album;
    }
    public string GetGenero()
    {
        if (genero >= 80)
            return "OTHER";
        else
            return generosTagID3v1[genero];
    }
    public byte GetByteGenero()
    {
        return genero;
    }

    private TagId3V1(
        string titulo, string artista, string album,
        in ushort año, in byte genero, string comentario)
    {
        this.titulo = titulo ?? throw new ArgumentNullException(nameof(titulo));
        this.artista = artista ?? throw new ArgumentNullException(nameof(artista));
        this.album = album ?? throw new ArgumentNullException(nameof(album));
        this.año = año;
        this.genero = genero;
        this.comentario = comentario ?? throw new ArgumentNullException(nameof(comentario));
    }

    public override string ToString()
    {
        return $"{"Titulo:",11} {GetTitulo()}\n"
                + $"{"Artista:",11} {GetArtista()}\n"
                + $"{"Album:",11} {GetAlbum()}\n"
                + $"{"Año:",11} {GetAño()}\n"
                + $"{"Genero:",11} {GetGenero()}\n"
                + $"{"Comentario:",11} {GetComentario()}";
    }

    public static bool HayTAG(string rutaFichero)
    {
        bool hayTAG = false;
        using (FileStream stream = new FileStream(rutaFichero, FileMode.Open, FileAccess.Read, FileShare.Read))
        using (BinaryReader streamRB = new BinaryReader(stream))
        {
            // Habrá TAG si como mínimo el fichero mide más de los 128 bytes que ocupa el mismo.
            hayTAG = stream.Length > TOTAL_BYTES;
            if (hayTAG)
            {
                // Se que ocupa esos bytes y me desplazo donde en teoría está el principio del TAG
                stream.Seek(-TOTAL_BYTES, SeekOrigin.End);
                // Leo los 3 caracteres del TAG y los transformo a cadena.
                string tag = new string(streamRB.ReadChars(BYTES_TAG_ID));
                hayTAG = tag == "TAG";
            }
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

        using (FileStream stream = new FileStream(rutaFichero, FileMode.Open, FileAccess.Read))
        using (BinaryReader streamBR = new BinaryReader(stream))
        {
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
}


class Program
{
    static void Main()
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
