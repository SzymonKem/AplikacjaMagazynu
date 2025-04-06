using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magazyn.Models
{
    public class Laptop
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string KodKreskowy { get; set; }

        [Required]
        public string Marka { get; set; }

        [Required]
        public string Model { get; set; }

        public string? System { get; set; }

        [Required]
        public double RozmiarEkranu { get; set; }

        [Required]
        public int Ilosc { get; set; }

        public Laptop() { }
        public Laptop(string kod, string marka, string model, string system, double ekran, int ilosc) 
        {
            KodKreskowy = kod;
            Marka = marka;
            Model = model;
            System = system;
            RozmiarEkranu = ekran;
            Ilosc = ilosc;
        }

        public override string ToString()
        {
            return $"{KodKreskowy},{Marka},{Model},{System},{RozmiarEkranu},{Ilosc}";
        }
    }

}
