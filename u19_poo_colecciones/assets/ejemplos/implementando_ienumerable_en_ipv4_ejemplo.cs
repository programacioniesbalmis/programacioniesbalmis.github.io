using System.Text;

public record class IPv4Address(byte Byte1, byte Byte2, byte Byte3, byte Byte4) : IEnumerable<byte>
{
    public IEnumerator<byte> GetEnumerator()
    {
        yield return Byte1;
        yield return Byte2;
        yield return Byte3;
        yield return Byte4;
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    public IPv4Address(string ip)
        : this(
            byte.Parse(ip.Split('.')[0]), byte.Parse(ip.Split('.')[1]),
            byte.Parse(ip.Split('.')[2]), byte.Parse(ip.Split('.')[3]))
    { }
    public override string ToString() => $"{Byte1}.{Byte2}.{Byte3}.{Byte4}";
    public string ToBinaryString()
    {
        StringBuilder ipBinario = new();
        foreach (byte b in this) ipBinario.Append($"{b:b8}.");
        return ipBinario.ToString().TrimEnd('.');
    }
}

class Program
{
    static void Main()
    {
        IPv4Address ip = new(192, 168, 1, 1);
        IPv4Address mascara = new("255.255.255.0");

        Console.WriteLine($"    IP1: {ip,-15} {ip.ToBinaryString()}");
        Console.WriteLine($"Mascara: {mascara,-15} {mascara.ToBinaryString()}");
    }
}
