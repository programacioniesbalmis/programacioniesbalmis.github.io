using System.Text;

public class Ejemplo
{
    static void Main()
    {
        FileStream fs = File.Create("ejemplo.txt");
        // Paso la cadena a un array de 17 bytes para poder
        // escribirla con un FileStream.
        byte[] buffer = Encoding.UTF8.GetBytes("Lorem ipsum dolor");
        fs.Write(buffer, 0, buffer.Length);
        fs.Close();

        fs = new FileStream("ejemplo.txt", FileMode.Open, FileAccess.Read);
        Console.WriteLine("Longitud Fichero: " + fs.Length); // Mostrará 17
        Console.WriteLine("Posicion descriptor lectura: " + fs.Position); // Devolverá un 0

        char c = (char)fs.ReadByte(); // c = 'L'
        Console.WriteLine("Posición descriptor lectura: " + fs.Position); // Devolverá un 1

        c = (char)fs.ReadByte(); // c = 'o'
        c = (char)fs.ReadByte(); // c = 'r'
        c = (char)fs.ReadByte(); // c = 'e'
        Console.WriteLine("Posición descriptor lectura: " + fs.Position); // Devolverá un 4
        fs.Close();
    }
}