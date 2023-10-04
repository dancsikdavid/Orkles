using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orokles
{
    class Szemely
    {
        private string nev;
        private string lakcim;
        private int[] szuletesiDatum;

        public Szemely(string nev)
        {
            this.nev = nev;
        }

        public Szemely(string nev, string lakcim)
        {
            this.nev = nev;
            this.lakcim = lakcim;
        }

        public Szemely(string nev, int[] szuletesiDatum)
        {
            this.nev = nev;
            this.szuletesiDatum = szuletesiDatum;
        }

        public Szemely(string nev, string lakcim, int[] szuletesiDatum)
        {
            this.nev = nev;
            this.lakcim = lakcim;
            this.szuletesiDatum = szuletesiDatum;
        }

        public string Nev
        {
            get { return nev; }
        }

        public string Lakcim
        {
            get { return lakcim; }
            set { lakcim = value; }
        }

        public string this[string index]
        {
            get
            {
                switch (index)
                {
                    case "ev":
                        return szuletesiDatum[0].ToString();
                    case "honap":
                        return szuletesiDatum[1].ToString();
                    case "nap":
                        return szuletesiDatum[2].ToString();
                    default:
                        throw new ArgumentOutOfRangeException("Érvénytelen index.");
                }
            }
        }

        public override string ToString()
        {
            return $"Név: {nev}, Lakcím: {lakcim}, Születési dátum: {szuletesiDatum[0]}/{szuletesiDatum[1]}/{szuletesiDatum[2]}";
        }

        public int Kor()
        {
            DateTime szuletesiIdo = new DateTime(szuletesiDatum[0], szuletesiDatum[1], szuletesiDatum[2]);
            DateTime maiDatum = DateTime.Now;
            int kor = maiDatum.Year - szuletesiIdo.Year;

            if (maiDatum.Month < szuletesiIdo.Month || (maiDatum.Month == szuletesiIdo.Month && maiDatum.Day < szuletesiIdo.Day))
            {
                kor--;
            }

            return kor;
        }
    }
}
