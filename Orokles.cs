using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Szemely
{
    public string Nev { get; }
    public string Lakcim { get; set; }
    private int[] szuletesiDatum;

    public Szemely(string nev, string lakcim, int[] datum)
    {
        Nev = nev;
        Lakcim = lakcim;
        szuletesiDatum = new int[3];
        if (datum.Length == 3)
        {
            szuletesiDatum[0] = datum[0];
            szuletesiDatum[1] = datum[1];
            szuletesiDatum[2] = datum[2];
        }
    }

    public string this[string index]
    {
        get
        {
            switch (index.ToLower())
            {
                case "ev":
                    return szuletesiDatum[0].ToString();
                case "honap":
                    return szuletesiDatum[1].ToString();
                case "nap":
                    return szuletesiDatum[2].ToString();
                default:
                    return "Érvénytelen!";
            }
        }
    }

    public override string ToString()
    {
        return $"Név: {Nev}, Lakcím: {Lakcim}, Születési dátum: {szuletesiDatum[0]}.{szuletesiDatum[1]}.{szuletesiDatum[2]}";
    }

    public int Kor()
    {
        int eletkor = DateTime.Now.Year - szuletesiDatum[0];
        if (DateTime.Now.Month < szuletesiDatum[1] || (DateTime.Now.Month == szuletesiDatum[1] && DateTime.Now.Day < szuletesiDatum[2]))
        {
            eletkor--;
        }
        return eletkor;
    }
}

class Alkalmazott : Szemely
{
    public string Beosztas { get; }
    public double Fizetes { get; private set; }

    public Alkalmazott(string nev, string lakcim, string beosztas, double fizetes, int[] datum) : base(nev, lakcim, datum)
    {
        Beosztas = beosztas;
        Fizetes = fizetes;
    }

    public void Fizetesemeles(double osszeg)
    {
        Fizetes += osszeg;
    }

    public override string ToString()
    {
        return base.ToString() + $", Beosztás: {Beosztas}, Fizetés: {Fizetes} Ft";
    }

    public new int Kor()
    {
        if (Fizetes > 300000)
        {
            return base.Kor() - 10;
        }
        return base.Kor();
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.Write("Kérem, adja meg a nevet: ");
            string nev = Console.ReadLine();

            Console.Write("Kérem, adja meg a lakcímet: ");
            string lakcim = Console.ReadLine();

            int[] datum = new int[3];
            Console.Write("Kérem, adja meg a születési évet: ");
            datum[0] = int.Parse(Console.ReadLine());
            Console.Write("Kérem, adja meg a születési hónapot: ");
            datum[1] = int.Parse(Console.ReadLine());
            Console.Write("Kérem, adja meg a születési napot: ");
            datum[2] = int.Parse(Console.ReadLine());

            Szemely szemely = new Szemely(nev, lakcim, datum);
            Console.WriteLine(szemely);

            Console.WriteLine($"Életkor: {szemely.Kor()} év");

            Console.WriteLine("--------------");

            Console.Write("Kérem, adja meg az alkalmazott beosztását: ");
            string beosztas = Console.ReadLine();

            Console.Write("Kérem, adja meg az alkalmazott fizetését: ");
            double fizetes = double.Parse(Console.ReadLine());

            Alkalmazott alkalmazott = new Alkalmazott(nev, lakcim, beosztas, fizetes, datum);
            Console.WriteLine(alkalmazott);

            Console.WriteLine($"Életkor: {alkalmazott.Kor()} év");

            Console.WriteLine("--------------");

            Console.Write("Kérem, adja meg a fizetésemelés összegét: ");
            double emeles = double.Parse(Console.ReadLine());
            alkalmazott.Fizetesemeles(emeles);

            Console.WriteLine(alkalmazott);
            Console.ReadKey();
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Hiba történt: {ex.Message}");
        }
    }
}
