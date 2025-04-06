using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using magazyn.Models;
using magazyn.Services;
using magazyn.View;
using Microsoft.Win32;


namespace magazyn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal ObservableCollection<Laptop> produkty = new ObservableCollection<Laptop>();

        private bool czyKliknieto = false;


        public MainWindow()
        {
            InitializeComponent();
            using var db = new LaptopyDbContext();
            produkty = MetodyBazyDanych.PobierzLaptopyZBazy();
            Debug.WriteLine(produkty.ToString());
            ListaProduktow.ItemsSource = produkty;
        }

        private void SzybkieDodawanie_Click(object sender, RoutedEventArgs e)
        {
            czyKliknieto = !czyKliknieto;
            if (czyKliknieto)
            {
                PrzyciskSzybkieDodawanie.Background = Brushes.Green;
                PrzyciskSzybkieDodawanie.Foreground = Brushes.White;
            }
            else
            {
                PrzyciskSzybkieDodawanie.Background = Brushes.White;
                PrzyciskSzybkieDodawanie.Foreground = Brushes.Black;
            }
        }

        private void FiltrowanieKodem_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FiltrowanieKodem.Text == "Podaj kod...")
            {
                FiltrowanieKodem.Text = "";
                FiltrowanieKodem.Foreground = Brushes.Black;
            }
        }

        private void FiltrowanieKodem_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FiltrowanieKodem.Text))
            {
                FiltrowanieKodem.Text = "Podaj kod...";
                FiltrowanieKodem.Foreground = Brushes.Silver;
            }
        }

        private void FiltrowanieMarka_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FiltrowanieMarka.Text == "Podaj marke...")
            {
                FiltrowanieMarka.Text = "";
                FiltrowanieMarka.Foreground = Brushes.Black;
            }
        }

        private void FiltrowanieMarka_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FiltrowanieMarka.Text))
            {
                FiltrowanieMarka.Text = "Podaj marke...";
                FiltrowanieMarka.Foreground = Brushes.Silver;
            }
        }

        private void FiltrowanieModelem_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FiltrowanieModelem.Text == "Podaj model...")
            {
                FiltrowanieModelem.Text = "";
                FiltrowanieModelem.Foreground = Brushes.Black;
            }
        }

        private void FiltrowanieModelem_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FiltrowanieModelem.Text))
            {
                FiltrowanieModelem.Text = "Podaj model...";
                FiltrowanieModelem.Foreground = Brushes.Silver;
            }
        }

        private void PrzyciskDodaj_Click(object sender, RoutedEventArgs e)
        {
            DodawanieProduktu dodawanie = new DodawanieProduktu(this, czyNowyP: true);
            dodawanie.ShowDialog();
        }

        private void PrzyciskUsun_Click(object sender, RoutedEventArgs e)
        {
            if (ListaProduktow.SelectedItem != null)
            {
                Laptop wybranyLaptop = ListaProduktow.SelectedItem as Laptop;
                MessageBoxResult potweirdzenie = MessageBox.Show("Czy na pewno chcesz usunąć produkt: " + wybranyLaptop.Marka + " " + wybranyLaptop.Model, "Potwierdzenie usunięcia", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (potweirdzenie == MessageBoxResult.Yes)
                {
                    MetodyBazyDanych.UsunLaptopaZBazy(wybranyLaptop);
                    produkty.Remove(wybranyLaptop);
                }
                CollectionViewSource.GetDefaultView(ListaProduktow.ItemsSource).Refresh();
            }
            else
            {
                MessageBox.Show("No item selected");
            }
        }

        private void PrzyciskEdytuj_Click(object sender, RoutedEventArgs e)
        {
            EdytujLaptop();
        }

        private void ListaProduktow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject source = (DependencyObject)e.OriginalSource;
            while (source != null && !(source is ListViewItem))
            {
                if (source is GridViewColumnHeader)
                {
                    return;
                }
                source = VisualTreeHelper.GetParent(source);
            }
            if (source is ListViewItem)
            {
                EdytujLaptop();
            }
        }

        private void EdytujLaptop()
        {
            DodawanieProduktu edycja = new DodawanieProduktu(this);
            edycja.ShowDialog();
        }

        private void PrzyciskOdswiez_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListaProduktow.ItemsSource).Refresh();
        }

        private void PrzyciskImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog otworzPlik = new OpenFileDialog();
            otworzPlik.Filter = "Pliki CSV (*.csv)|*.csv";
            if (otworzPlik.ShowDialog() != true)
            {
                return;
            }
            ObservableCollection<Laptop> dane = ImportExportCSV.ImportujDaneZPliku(otworzPlik.FileName);
            if (dane == null)
            {
                return;
            }
            foreach (Laptop laptop in dane)
            {
                produkty.Add(laptop);
            }
        }

        private void PrzyciskEksport_Click(object sender, RoutedEventArgs e)
        {
            if (produkty.Count != 0)
            {
                SaveFileDialog oknoZapisu = new SaveFileDialog
                {
                    FileName = "Laptopy",
                    DefaultExt = ".csv",
                    Filter = "Pliki CSV (*.csv)|*.csv"
                };
                if (oknoZapisu.ShowDialog() == true)
                {
                    ImportExportCSV.EksportujDaneDoPliku(produkty, oknoZapisu.FileName);
                    MessageBox.Show("Pomyślnie wyeksportowano");
                }
            }
            else
            {
                MessageBox.Show("Brak danych do eksportu", "Brak Danych", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Filtrowanie_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltrujDane();
        }

        private void FiltrujDane()
        {
            if (ListaProduktow != null)
            {
                Debug.WriteLine(FiltrowanieKodem.Text == "Podaj kod...");
                string kod = ((FiltrowanieKodem.Text == "Podaj kod...") ? "" : FiltrowanieKodem.Text);
                Debug.WriteLine(kod);
                string marka = ((FiltrowanieMarka.Text == "Podaj marke...") ? "" : FiltrowanieMarka.Text);
                Debug.WriteLine(marka);
                string model = ((FiltrowanieModelem.Text == "Podaj model...") ? "" : FiltrowanieModelem.Text);
                Debug.WriteLine(model);
                IEnumerable<Laptop> filtr = produkty.Where((Laptop laptop) => laptop.KodKreskowy.Contains(kod) && laptop.Marka.Contains(marka) && laptop.Model.Contains(model));
                ListaProduktow.ItemsSource = filtr;
            }
        }

        private void btnSortowania_Click(object sender, RoutedEventArgs e)
        {
            if (CmbSortowanmia.Visibility == Visibility.Collapsed)
            {
                CmbSortowanmia.Visibility = Visibility.Visible;
            }
            else
            {
                CmbSortowanmia.Visibility = Visibility.Collapsed;
            }
        }

        private void CmbSortowanmia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortujDane();
        }
        private void SortujDane()
        {
            if (ListaProduktow.ItemsSource == null)
            {
                return;
            }

            ComboBoxItem selectedItem = CmbSortowanmia.SelectedItem as ComboBoxItem;
            string opcje = selectedItem.Content.ToString();

            ICollectionView widok = CollectionViewSource.GetDefaultView(ListaProduktow.ItemsSource);
            widok.SortDescriptions.Clear();

            switch (opcje)
            {
                case "Sortowanie kodem":
                    widok.SortDescriptions.Add(new SortDescription("KodKreskowy", ListSortDirection.Ascending));
                    break;

                case "Sortowanie marką":
                    widok.SortDescriptions.Add(new SortDescription("Marka", ListSortDirection.Ascending));
                    break;

                case "Sortowanie modelem":
                    widok.SortDescriptions.Add(new SortDescription("Model", ListSortDirection.Ascending));
                    break;

                case "Sortowanie systemem":
                    widok.SortDescriptions.Add(new SortDescription("System", ListSortDirection.Ascending));
                    break;

                case "Sortowanie ekranem":
                    widok.SortDescriptions.Add(new SortDescription("Ekran", ListSortDirection.Ascending));
                    break;

                case "Sortowanie ilością":
                    widok.SortDescriptions.Add(new SortDescription("Ilosc", ListSortDirection.Ascending));
                    break;





            }
            widok.Refresh();
        }
    }
}

