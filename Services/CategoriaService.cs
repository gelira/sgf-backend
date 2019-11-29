using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SGFBackend.Models;

namespace SGFBackend.Services
{
    public class CategoriaService
    {
        private HttpClient http;

        public CategoriaService()
        {
            http = (new HttpClientHelper()).Client;
        }

        public async Task<List<CategoriaGet>> GetCategorias()
        {
            try
            {
                var response = await http.GetAsync("categorias");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var lista = JsonConvert.DeserializeObject<List<CategoriaGet>>(content);
                    return lista;
                }
                return new List<CategoriaGet>();
            }
            catch (Exception)
            {
                return new List<CategoriaGet>();
            }
        }

        public async Task<bool> CreateCategoria(CategoriaCreate c)
        {
            var json = JsonConvert.SerializeObject(c);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await http.PostAsync("categorias", content);
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
    }
}