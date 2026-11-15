using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WPFWebAPI
{
    /// <summary>
    /// Логика взаимодействия для CRUDCar.xaml
    /// </summary>
    public partial class CRUDCar : Page
    {

        private CarApi carApi = new CarApi();

        private Car newCar = new Car();


        private bool put = false;
        private bool patch = false;

        private Car originalSelectCar;


        public CRUDCar()
        {
            InitializeComponent();
            DataContext = newCar;
        }

        public CRUDCar(Car selectCar)
        {
            InitializeComponent();

            put = true;
            
            newCar = selectCar;
            DataContext = newCar;
        }

        public CRUDCar(Car selectCar, bool patch)
        {
            InitializeComponent();

            this.patch = patch;

            originalSelectCar = selectCar;

            newCar = new Car
            {
                Id = selectCar.Id,
                Brand = selectCar.Brand,
                Model = selectCar.Model,
                ReleaseYear = selectCar.ReleaseYear,
                Price = selectCar.Price,
                IsAvailable = selectCar.IsAvailable,
            };


            DataContext = newCar;
        }



        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (put)
                {
                    if (string.IsNullOrEmpty(newCar.Brand) || string.IsNullOrEmpty(newCar.Model) || newCar.ReleaseYear <= 0 || newCar.Price <= 0)
                    {
                        MessageBox.Show($"Введите корректные значения");
                        return;
                    }

                    await carApi.PutCar(newCar);
                    NavigationService.GoBack();
                }
                else if (patch)
                {
                    if (string.IsNullOrEmpty(newCar.Brand) || string.IsNullOrEmpty(newCar.Model) || newCar.ReleaseYear <= 0 || newCar.Price <= 0)
                    {
                        MessageBox.Show($"Введите корректные значения");
                        return;
                    }

                    CarPatch carPatch = new CarPatch();


                    if (newCar.Brand != originalSelectCar.Brand)
                    {
                        carPatch.Brand = newCar.Brand;
                    }

                    if (newCar.Model != originalSelectCar.Model)
                    {
                        carPatch.Model = newCar.Model;
                    }

                    if (newCar.ReleaseYear != originalSelectCar.ReleaseYear)
                    {
                        carPatch.ReleaseYear = newCar.ReleaseYear;
                    }

                    if (newCar.Price != originalSelectCar.Price)
                    {
                        carPatch.Price = newCar.Price;
                    }

                    if (newCar.IsAvailable != originalSelectCar.IsAvailable)
                    {
                        carPatch.IsAvailable = newCar.IsAvailable;
                    }

                    await carApi.PatchCar(originalSelectCar.Id, carPatch);
                    NavigationService.GoBack();
                }
                else
                {
                    if (string.IsNullOrEmpty(newCar.Brand) || string.IsNullOrEmpty(newCar.Model) || newCar.ReleaseYear <= 0 || newCar.Price <= 0)
                    {
                        MessageBox.Show($"Введите корректные значения");
                        return;
                    }

                    await carApi.PostAddCar(newCar);
                    NavigationService.GoBack();
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

        }
    }
}
