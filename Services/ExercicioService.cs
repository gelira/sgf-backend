using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SGFBackend.Models;

namespace SGFBackend.Services
{
    public class ExercicioService
    {
        private HttpClient http;

        public ExercicioService()
        {
            http = (new HttpClientHelper()).Client;
        }

        public async Task<List<ExercicioGet>> GetExercicios()
        {
            try
            {
                var response = await http.GetAsync("exercicios");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var lista = JsonConvert.DeserializeObject<List<ExercicioGet>>(content);
                    return lista;
                }
                return new List<ExercicioGet>();
            }
            catch (Exception)
            {
                return new List<ExercicioGet>();
            }
        }

        public async Task<bool> CreateExercicio(ExercicioCreate e)
        {
            var json = JsonConvert.SerializeObject(e);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await http.PostAsync("exercicios", content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<ExercicioGet>> GetExerciciosPorCategoria(int id)
        {
            try
            {
                var response = await http.GetAsync($"exercicios/categoria/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var lista = JsonConvert.DeserializeObject<List<ExercicioGet>>(content);
                    return lista;
                }
                return new List<ExercicioGet>();
            }
            catch (Exception)
            {
                return new List<ExercicioGet>();
            }
        }
    }
}