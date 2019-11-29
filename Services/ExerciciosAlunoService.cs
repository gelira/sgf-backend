using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SGFBackend.Models;

namespace SGFBackend.Services
{
    public class ExerciciosAlunoService
    {
        private HttpClient http;

        public ExerciciosAlunoService()
        {
            http = (new HttpClientHelper()).Client;
        }

        public async Task<bool> AddExercicio(ExercicioAlunoCreate ea)
        {
            var tokenService = TokenService.GetInstance();
            var client = tokenService.AddToken(http);
            var json = JsonConvert.SerializeObject(ea);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await http.PostAsync("exercicios-aluno", content);
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

        public async Task<List<ExercicioAlunoGet>> ListarExerciciosAluno(int id)
        {
            try
            {
                var response = await http.GetAsync($"exercicios-aluno/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var lista = JsonConvert.DeserializeObject<List<ExercicioAlunoGet>>(content);
                    return lista;
                }
                return new List<ExercicioAlunoGet>();
            }
            catch (Exception)
            {
                return new List<ExercicioAlunoGet>();
            }
        }

        public async Task<List<ExercicioAlunoGet>> ListarExerciciosAlunoCategoria(int id)
        {
            var tokenService = TokenService.GetInstance();
            var client = tokenService.AddToken(http);
            try
            {
                var response = await client.GetAsync($"exercicios-aluno/categoria/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var lista = JsonConvert.DeserializeObject<List<ExercicioAlunoGet>>(content);
                    return lista;
                }
                return new List<ExercicioAlunoGet>();
            }
            catch (Exception)
            {
                return new List<ExercicioAlunoGet>();
            }
        }
    }
}