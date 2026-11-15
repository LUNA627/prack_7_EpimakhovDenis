using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFWebAPI
{
    /// <summary>
    /// Логика взаимодействия для CarMain.xaml
    /// </summary>
    public partial class CarMain : Page
    {
        private CarApi carApi = new CarApi();

        public ObservableCollection<Car> AllCars { get; set; } = new();

        public string? BrandFilter { get; set; }
        public decimal? MaxPriceFilter { get; set; }

        public Car? SelectCar { get; set; }


        public CarMain()
        {
            InitializeComponent();
            DataContext = this;
        }


        private async void Load(object sender, RoutedEventArgs e)
        {
            try
            {
                AllCars.Clear();
                var allCars = await carApi.GetAllCar();

                foreach (var car in allCars)
                {
                    AllCars.Add(car);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }



        private async void Spisok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AllCars.Clear();
                var allCars = await carApi.GetAllCar();

                foreach (var car in allCars)
                {
                    AllCars.Add(car);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }


        private async void Seatch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AllCars.Clear();
                var carF = await carApi.GetQueryCar(BrandFilter, MaxPriceFilter);
                foreach (var car in carF)
                {
                    AllCars.Add(car);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            AllCars.Clear();
        }



        private void PostAddNewCar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CRUDCar());
        }

        private void PutCar_Click(object sender, RoutedEventArgs e)
        {
            if (SelectCar == null)
            {
                MessageBox.Show("Выберите авто для полного изменения");
                return;
            }

            NavigationService.Navigate(new CRUDCar(SelectCar));
        }

        private void PatchCar_Click(object sender, RoutedEventArgs e)
        {
            if (SelectCar == null)
            {
                MessageBox.Show("Выберите авто для частичного изменения");
                return;
            }

            NavigationService.Navigate(new CRUDCar(SelectCar, true));
        }

        private async void DeleteCar_Click(object sender, RoutedEventArgs e)
        {
            if (SelectCar == null)
            {
                MessageBox.Show("Выберите авто для удаления");
                return;
            }

            try
            {
                await carApi.DeleteCar(SelectCar.Id);
                AllCars.Clear();
                var allCars = await carApi.GetAllCar();

                foreach (var car in allCars)
                {
                    AllCars.Add(car);
                }

            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

       
    }
}
