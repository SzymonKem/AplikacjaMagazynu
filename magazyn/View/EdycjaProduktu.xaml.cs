using magazyn.Models;
using magazyn.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace magazyn.View
{
    /// <summary>
    /// Logika interakcji dla klasy EdycjaProduktu.xaml
    /// </summary>
    public partial class EdycjaProduktu : Window
    {

        private MainWindow mainWindow = null;

        private bool czyNowyProdukt;

        private Laptop laptop;

        public EdycjaProduktu()
        {
            InitializeComponent();
        }

        public EdycjaProduktu(MainWindow mainWin, bool czyNowyP = false)
        {
            InitializeComponent();
            mainWindow = mainWin;
            czyNowyProdukt = czyNowyP;
            PrzygotujWiazanie();
        }

        private void PrzygotujWiazanie()
        {
            if (!czyNowyProdukt)
            {
                if (mainWindow.ListaProduktow.SelectedItem is Laptop wybranyLaptop)
                {
                    wartosciDodawania.DataContext = wybranyLaptop;
                }
            }
            else
            {
                laptop = new Laptop("", "", "", "", 0.0, 0);
                wartosciDodawania.DataContext = laptop;
            }
        }

        private void ZapiszProdukt(object sender, RoutedEventArgs e)
        {
            if (czyNowyProdukt)
            {
                MetodyBazyDanych.DodajLaptopaDoBazy(laptop);
                mainWindow.produkty.Add(laptop);
            }
            else
            {
                if (mainWindow.ListaProduktow.SelectedItem is Laptop edytowanyLaptop)
                {
                    MetodyBazyDanych.AktualizujLaptopaWBazie(edytowanyLaptop);
                }
            }

            CollectionViewSource.GetDefaultView(mainWindow.ListaProduktow.ItemsSource).Refresh();
            this.DialogResult = true;
        }

        private void AnulujDodawanie(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

    }
}
