using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SGFBackend.Models;

namespace SGFBackend.Services
{
    public class UserService
    {
        private HttpClient http;

        public UserService()
        {
            http = (new HttpClientHelper()).Client;
        }

        public async Task<bool> Autenticar(UserLogin u)
        {
            var json = JsonConvert.SerializeObject(u);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await http.PostAsync("users/login", content);
                if (response.IsSuccessStatusCode)
                {
                    var respContent = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<AccessToken>(respContent);
                    var tokenService = TokenService.GetInstance();
                    tokenService.Token = obj.Token;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CriarAluno(UserCreate user)
        {
            return await CriarUsuario(user, "users/aluno");
        }

        public async Task<bool> CriarProfessor(UserCreate user)
        {
            return await CriarUsuario(user, "users/professor");
        }

        private async Task<bool> CriarUsuario(UserCreate user, string path)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await http.PostAsync(path, content);
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
    
        public async Task<bool> AtualizarAluno(int id, AlunoUpdate a)
        {
            var tokenService = TokenService.GetInstance();
            var client = tokenService.AddToken(http);
            var json = JsonConvert.SerializeObject(a);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PutAsync($"users/aluno/{id}", content);
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

        public async Task<bool> AtualizarProfessor(ProfessorUpdate p)
        {
            var tokenService = TokenService.GetInstance();
            var client = tokenService.AddToken(http);
            var json = JsonConvert.SerializeObject(p);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PutAsync("users/professor", content);
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