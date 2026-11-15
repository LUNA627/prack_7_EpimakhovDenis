using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;


namespace WPFWebAPI
{
    public class CarApi
    {
        private HttpClient _httpClient = new HttpClient();
        public CarApi()
        {
            _httpClient.BaseAddress = new Uri("https://localhost:7059/");
        }

        public async Task<List<Car>?> GetAllCar()
        {
            return await _httpClient.GetFromJsonAsync<List<Car>>("api/v1/Car");
        }

        public async Task<List<Car>?> GetQueryCar(string? brand, decimal? maxPrice)
        {
            string baseAdress = "api/v1/Car?";

            if (brand != null)
            {
                baseAdress += $"brand={brand}";

                if (maxPrice != null)
                {
                    baseAdress += $"&maxPrice={maxPrice}";
                }
            }
            else if (maxPrice != null)
            {
                baseAdress += $"maxPrice={maxPrice}";
            }

            return await _httpClient.GetFromJsonAsync<List<Car>>(baseAdress);
        }


        public async Task PostAddCar(Car car)
        {
            await _httpClient.PostAsJsonAsync("api/v1/Car", car);
        }

        public async Task PutCar(Car car)
        {
            await _httpClient.PutAsJsonAsync($"api/v1/Car/{car.Id}", car);
        }

        public async Task PatchCar(int id,CarPatch patchCar)
        {
            await _httpClient.PatchAsJsonAsync($"api/v1/Car/{id}", patchCar);
        }


        public async Task DeleteCar(int id)
        {
            await _httpClient.DeleteAsync($"api/v1/Car/{id}");
        }

    }
}
