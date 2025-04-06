using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magazyn.Models
{
    internal class ImportExportCSV
    {
        public static ObservableCollection<Laptop> ImportujDaneZPliku(string sciezka)
        {
            ObservableCollection<Laptop> dane = new ObservableCollection<Laptop>();
            TextFieldParser parser = new TextFieldParser(sciezka);
            parser.SetDelimiters(",");
            if (!parser.EndOfData)
            {
                parser.ReadFields();
            }
            while (!parser.EndOfData)
            {
                string[] rzad = parser.ReadFields();
                string ekran = rzad[4].Trim();
                string ilosc = rzad[5].Trim();
                double ekranDouble = 0.0;
                int iloscInt = 0;
                double.TryParse(ekran, NumberStyles.Any, CultureInfo.InvariantCulture, out ekranDouble);
                int.TryParse(ilosc, out iloscInt);
                Laptop pobranyLaptop = new Laptop(rzad[0], rzad[1], rzad[2], rzad[3], ekranDouble, iloscInt);
                dane.Add(pobranyLaptop);
            }
            return dane;
        }

        public static void EksportujDaneDoPliku(ObservableCollection<Laptop> laptopy, string sciezka)
        {
            List<string> rzedy = new List<string> { "KodKreskowy, Marka, Model, System, Ekran, Ilosc" };
            foreach (Laptop laptop in laptopy)
            {
                string rzad = laptop.ToString();
                rzedy.Add(rzad);
            }
            File.WriteAllLines(sciezka, rzedy);
        }
    }
}
