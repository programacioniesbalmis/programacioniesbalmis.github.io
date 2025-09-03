using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace CasoDeEstudio;

public enum Color { Blanco, Morado, Cian, Azul, Rojo, Verde, Negro, Naranja, Gris }
public enum Marca { DESCONOCIDA, BMW, SEAT, AUDI, RENAULT, MAN, DAF, CITROEN, TOYOTA, SUZUKI, YAMAHA, MERCEDES, PEGASO }
public enum TipoCoche { SinIdentificar, Berlina, Coupe, Sedan, Cabrio, TodoTerreno, MonoVolumen, Crossover }
public enum TipoMoto { SinIdentificar, Scooter, Motocross, Naked, Trail, Supermotard }
public enum TipoCamion { SinIdentificar, Articulado, Frigorífico, Cisterna, Trailer }
public enum TipoAutobus { SinIdentificar, Articulado, UnPiso, DosPisos, Microbus, Eléctrico }

// TODO: Añade el código de las definiciones aquí.

public static class Principal
{
    static void Entra(CampaVehiculos campa, Vehiculo v)
    {
        string t1 = "Entrando " + v.GetType().Name + " " + v.Tipo + " " + v.Matricula;
        var (puedeEntrar, plaza, problema) = campa.PuedeEntrar(v);
        if (puedeEntrar)
        {
            campa.Entra(v);
            Console.WriteLine(t1.PadRight(42) + "-> Aparcado en la plaza " + plaza);
        }
        else
        {
            Console.WriteLine(t1.PadRight(42) + "-> " + (problema ?? ""));
        }
    }

    static void Sale(CampaVehiculos campa, Vehiculo v)
    {
        string t1 = "Saliendo " + v.GetType().Name + " " + v.Tipo + " " + v.Matricula;
        var (puedeSalir, plaza, problema) = campa.PuedeSalir(v);
        if (puedeSalir)
        {
            campa.Sale(v);
            Console.WriteLine(t1.PadRight(42) + "-> Deja libre la plaza " + plaza);
        }
        else
        {
            Console.WriteLine(t1.PadRight(42) + "-> " + (problema ?? ""));
        }
    }

    public static void Main()
    {
        CampaVehiculos campa = new(capacidad: 5);

        Entra(
            campa: campa,
            v: new Coche(
                matricula: new Matricula(matricula: "1020 DRG"),
                color: Color.Azul,
                marca: Marca.SEAT,
                ocupantes: 3,
                tipo: TipoCoche.Coupe));

        Entra(
            campa: campa,
            v: new Camion(
                matricula: new Matricula(matricula: "8798 JWR"),
                color: Color.Blanco,
                marca: Marca.DAF,
                ocupantes: 1,
                ejes: 2,
                cargaMaximaKg: 6000,
                tipo: TipoCamion.Frigorífico));

        Entra(
            campa: campa,
            v: new Coche(
                matricula: new Matricula(matricula: "7643 LRF"),
                color: Color.Rojo,
                marca: Marca.BMW,
                ocupantes: 4,
                tipo: TipoCoche.TodoTerreno));

        Entra(
            campa: campa,
            v: new Coche(
                matricula: new Matricula(matricula: "1020 DRG"),
                color: Color.Azul,
                marca: Marca.SEAT,
                ocupantes: 3,
                tipo: TipoCoche.Coupe));

        Entra(
            campa: campa,
            v: new Vehiculo(
                matricula: new Matricula(matricula: "0000 DGP"),
                color: Color.Negro,
                marca: Marca.DESCONOCIDA,
                ocupantes: 2));

        Entra(
            campa: campa,
            v: new Moto(
                matricula: new Matricula(matricula: "1111 GRF"),
                color: Color.Rojo,
                marca: Marca.YAMAHA,
                ocupantes: 2,
                tipo: TipoMoto.Naked));

        Entra(
            campa: campa,
            v: new Coche(
                matricula: new Matricula(matricula: "1020 DRG"),
                color: Color.Azul,
                marca: Marca.SEAT,
                ocupantes: 3,
                tipo: TipoCoche.Coupe));

        Sale(
            campa: campa,
            v: new Camion(
                matricula: new Matricula(matricula: "8798 JWR"),
                color: Color.Blanco,
                marca: Marca.DAF,
                ocupantes: 1,
                ejes: 2,
                cargaMaximaKg: 6000,
                tipo: TipoCamion.Frigorífico));

        Sale(
            campa: campa,
            v: new Camion(
                matricula: new Matricula(matricula: "8798 JWR"),
                color: Color.Blanco,
                marca: Marca.DAF,
                ocupantes: 1,
                ejes: 2,
                cargaMaximaKg: 6000,
                tipo: TipoCamion.Frigorífico));

        Sale(
            campa: campa,
            v: new Moto(
                matricula: new Matricula(matricula: "1111 GRF"),
                color: Color.Rojo,
                marca: Marca.YAMAHA,
                ocupantes: 2,
                tipo: TipoMoto.Naked));

        Entra(
            campa: campa,
            v: new Moto(
                matricula: new Matricula(matricula: "1111 GRF"),
                color: Color.Rojo,
                marca: Marca.YAMAHA,
                ocupantes: 2,
                tipo: TipoMoto.Naked));

        Entra(
            campa: campa,
            v: new Camion(
                matricula: new Matricula(matricula: "8798 JWR"),
                color: Color.Blanco,
                marca: Marca.DAF,
                ocupantes: 1,
                ejes: 2,
                cargaMaximaKg: 6000,
                tipo: TipoCamion.SinIdentificar));

        Console.WriteLine(campa);
    }
}
