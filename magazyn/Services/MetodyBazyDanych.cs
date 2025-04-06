using magazyn.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magazyn.Services
{
    internal class MetodyBazyDanych
    {
        public static ObservableCollection<Laptop> PobierzLaptopyZBazy()
        {
            var baza = new LaptopyDbContext();
            return new ObservableCollection<Laptop>(baza.Laptopy.ToList());
        }

        public static void DodajLaptopaDoBazy(Laptop laptop)
        {
            try
            {
                using (var baza = new LaptopyDbContext())
                {
                    baza.Laptopy.Add(laptop);
                    int result = baza.SaveChanges();
                    Debug.WriteLine($"Zapisano {result} rekordów.");
                };
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("UNIQUE constraint failed") == true)
                {
                    throw new Exception("Laptop z takim kodem kreskowym już istnieje w bazie danych.");
                }
                throw;
            }
        }

        public static void UsunLaptopaZBazy(Laptop laptop)
        {
            using (var baza = new LaptopyDbContext())
            {
                baza.Laptopy.Remove(laptop);
                int result = baza.SaveChanges();
                Debug.WriteLine($"Zapisano {result} rekordów.");
            }
        }

        public static void AktualizujLaptopaWBazie(Laptop laptop)
        {
            using (var baza = new LaptopyDbContext())
            {
                var istniejacyLaptop = baza.Laptopy.FirstOrDefault(l => l.Id == laptop.Id);
                if (istniejacyLaptop != null)
                {
                    istniejacyLaptop.KodKreskowy = laptop.KodKreskowy;
                    istniejacyLaptop.Marka = laptop.Marka;
                    istniejacyLaptop.Model = laptop.Model;
                    istniejacyLaptop.KodKreskowy = laptop.KodKreskowy;
                    istniejacyLaptop.System = laptop.System;
                    istniejacyLaptop.RozmiarEkranu = laptop.RozmiarEkranu;
                    istniejacyLaptop.Ilosc = laptop.Ilosc;

                    baza.SaveChanges();
                }
            }
        }

    }
}
