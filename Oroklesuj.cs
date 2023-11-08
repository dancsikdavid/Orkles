using System;
using System.Collections.Generic;

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
    public string Beosztas { get; set; }
    public double Fizetes { get; set; }

    public Alkalmazott(string nev, string lakcim, string beosztas, double fizetes, int[] datum)
        : base(nev, lakcim, datum)
    {
        Beosztas = beosztas;
        Fizetes = fizetes;
    }

    public void Belep()
    {
        Console.WriteLine($"{Nev} belépett a vállalatba.");
    }

    public void Kilep()
    {
        Console.WriteLine($"{Nev} kilépett a vállalatból.");
    }

    public void Modosit(string ujBeosztas, double ujFizetes)
    {
        Beosztas = ujBeosztas;
        Fizetes = ujFizetes;
        Console.WriteLine($"{Nev} adatai frissítve: Beosztás: {Beosztas}, Fizetés: {Fizetes} Ft");
    }

    public void NyugdijbaMegy()
    {
        Console.WriteLine($"{Nev} nyugdíjba ment.");
    }

    public override string ToString()
    {
        return base.ToString() + $", Beosztás: {Beosztas}, Fizetés: {Fizetes} Ft";
    }
}

class Osztaly
{
    public string OsztalyKod { get; set; }
    public string OsztalyNev { get; set; }
    public List<Alkalmazott> Alkalmazottak { get; } = new List<Alkalmazott>();

    public Osztaly(string osztalyKod, string osztalyNev)
    {
        OsztalyKod = osztalyKod;
        OsztalyNev = osztalyNev;
    }
}

class Vallalat
{
    public List<Osztaly> Osztalyok { get; } = new List<Osztaly>();

    public void HozzaadOsztalyt(string osztalyKod, string osztalyNev)
    {
        Osztaly osztaly = new Osztaly(osztalyKod, osztalyNev);
        Osztalyok.Add(osztaly);
    }

    public void HozzaadAlkalmazottat(string osztalyKod, Alkalmazott alkalmazott)
    {
        Osztaly osztaly = Osztalyok.Find(o => o.OsztalyKod == osztalyKod);
        if (osztaly != null)
        {
            osztaly.Alkalmazottak.Add(alkalmazott);
        }
    }

    public void KiirSzotarat()
    {
        Console.WriteLine("Osztályok:");
        foreach (var osztaly in Osztalyok)
        {
            Console.WriteLine($"{osztaly.OsztalyKod} - {osztaly.OsztalyNev}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Vallalat vallalat = new Vallalat();

            while (true)
            {
                Console.WriteLine("Válasszon egy műveletet:");
                Console.WriteLine("1. Új osztály hozzáadása");
                Console.WriteLine("2. Új alkalmazott felvétele egy osztályhoz");
                Console.WriteLine("3. Osztályok kiírása");
                Console.WriteLine("4. Kilépés");

                int valasztas = int.Parse(Console.ReadLine());

                if (valasztas == 1)
                {
                    Console.Write("Kérem, adja meg az osztály kódját: ");
                    string osztalyKod = Console.ReadLine();
                    Console.Write("Kérem, adja meg az osztály nevét: ");
                    string osztalyNev = Console.ReadLine();

                    vallalat.HozzaadOsztalyt(osztalyKod, osztalyNev);
                }
                else if (valasztas == 2)
                {
                    Console.Write("Kérem, adja meg az osztály kódját, ahova felvenni kívánja az alkalmazottat: ");
                    string osztalyKod = Console.ReadLine();

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

                    Console.Write("Kérem, adja meg az alkalmazott beosztását: ");
                    string beosztas = Console.ReadLine();

                    Console.Write("Kérem, adja meg az alkalmazott fizetését: ");
                    double fizetes = double.Parse(Console.ReadLine());

                    Alkalmazott alkalmazott = new Alkalmazott(nev, lakcim, beosztas, fizetes, datum);

                    vallalat.HozzaadAlkalmazottat(osztalyKod, alkalmazott);

                    Console.WriteLine(alkalmazott);
                    Console.WriteLine($"Életkor: {alkalmazott.Kor()} év");
                  
                }
                else if (valasztas == 3)
                {
                    vallalat.KiirSzotarat();
                }
                else if (valasztas == 4)
                {
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba történt: {ex.Message}");
        }
    }
}
