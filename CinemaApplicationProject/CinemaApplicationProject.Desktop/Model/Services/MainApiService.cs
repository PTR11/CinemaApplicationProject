using CinemaApplicationProject.Desktop.Model.Errors;
using CinemaApplicationProject.Model.Database;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
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


        public async Task CreateAsync<T>(T entity) where T : RespondDTO
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Actors/", entity);
            entity.Id = (await response.Content.ReadAsAsync<T>()).Id;

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }
    }
}
