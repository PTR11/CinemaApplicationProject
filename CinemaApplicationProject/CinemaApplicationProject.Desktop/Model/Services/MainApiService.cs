using CinemaApplicationProject.Desktop.Model.Errors;
using CinemaApplicationProject.Model.Database;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Model.Services
{
    public class MainApiService
    {

        protected readonly HttpClient _client;

        public MainApiService(string baseAddress)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        public async Task<int> LoginAsync(String userName, String password)
        {
            LoginDTO user = new LoginDTO
            {
                UserName = userName,
                Password = password
            };
            var response = await _client.PostAsJsonAsync("api/Employee/Login", user);
            if (response.IsSuccessStatusCode)
            {
                return Int32.Parse(response.Content.ReadAsStringAsync().Result);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return 0;
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task<IEnumerable<T>> LoadingAsync<T>(String route)
        {
            var response = await _client.GetAsync(route);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<T>>();
            }
            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task<T> GetAsync<T>(String route)
        {
            var response = await _client.GetAsync(route);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task CreateRent(String route, RentFromGuestDTO entity)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync(route, entity);


            if (!response.IsSuccessStatusCode)
            {
                var foo = response.Content.ReadAsStringAsync().Result;
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
            
        }


        public async Task CreateAsync<T>(String route,T entity) where T : RespondDTO
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync(route, entity);

            
            if (!response.IsSuccessStatusCode)
            {
                var foo = response.Content.ReadAsStringAsync().Result;
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
            else
            {
                entity.Id = (await response.Content.ReadAsAsync<T>()).Id;
            }
        }

        public async Task UpdateAsync<T>(String route,T entity) where T : RespondDTO
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(route+"/"+ entity.Id, entity);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }
    }
}
