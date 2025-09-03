using System.Text;

public static class StringExtension
{
    public static string Capitaliza(this string s)
    {
        string sCapitalizada;
        if (!string.IsNullOrEmpty(s))
        {
            StringBuilder sb = new (s);
            sb[0] = char.ToUpper(sb[0]);
            for (int i = 1; i < s.Length; i++)
                sb[i] = char.IsWhiteSpace(sb[i - 1]) ? char.ToUpper(sb[i]) : sb[i];
            sCapitalizada = sb.ToString();
        }
        else
            sCapitalizada = s;
        return sCapitalizada;
    }

    public static int CuentaPalabras(this string s) 
    => s.Split(
                [ ' ', '.', '?' ],
                StringSplitOptions.RemoveEmptyEntries
              ).Length;
}

class Ejemplo
{
    static void Main()
    {
        string s = "hola caracola";
        // Si no hacemos el using StringExtensions; los métodos Capitaliza 
        // y CuentaPalabras no nos los ofrecerá.
        Console.WriteLine($"{s.Capitaliza()} tiene { s.CuentaPalabras()} palabras.");
    }
}