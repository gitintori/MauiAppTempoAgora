using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null; // Inicializa a variável Tempo como nula

            string chave = "d5ac0156751d6043f23c700059c8c167"; // Chave da API
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={chave}&units=metric&lang=pt_br"; // URL da API

            using (HttpClient client = new HttpClient()) // Cria um cliente HTTP
            {
                HttpResponseMessage resp = await client.GetAsync(url); // Faz a requisição HTTP

                if (resp.IsSuccessStatusCode) // Verifica se a resposta foi bem-sucedida
                {
                    string json = await resp.Content.ReadAsStringAsync(); // Lê o conteúdo da resposta como string

                    var rascunho = JObject.Parse(json); // Converte a string JSON para um objeto JObject

                    DateTime time = new (); // Cria um novo objeto DateTime
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime(); // Converte o horário do nascer do sol para o horário local
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime(); // Converte o horário do pôr do sol para o horário local
                    t = new() // Cria um novo objeto Tempo e inicializa suas propriedades
                    {
                        // Atribui os valores do JSON às propriedades do objeto Tempo
                        // Acessa os elementos do JSON usando a biblioteca Newtonsoft.Json.Linq
                        // Exemplo: (double)rascunho["coord"]["lat"] acessa a latitude
                        // Acessa o objeto "coord" e depois a propriedade "lat" dentro dele
                        lat = (double)rascunho["coord"]["lat"], // Atribui a latitude
                        lon = (double)rascunho["coord"]["lon"], // Atribui a longitude
                        description = (string)rascunho["weather"][0]["description"], // Atribui a descrição do tempo // Acessa o primeiro elemento do array "weather" (0)
                        main = (string)rascunho["weather"][0]["main"], // Atribui a condição do tempo // Acessa o primeiro elemento do array "weather" (0)
                        temp_min = (double)rascunho["main"]["temp_min"], // Atribui a temperatura mínima // Acessa o objeto "main"
                        temp_max = (double)rascunho["main"]["temp_max"], // Atribui a temperatura máxima // Acessa o objeto "main"
                        visibility = (int)rascunho["visibility"], // Atribui a visibilidade // Acessa o objeto "visibility"
                        speed = (double)rascunho["wind"]["speed"], // Atribui a velocidade do vento // Acessa o objeto "wind"
                        sunrise = sunrise.ToString(),// Atribui o horário do nascer do sol // Converte para string
                        sunset = sunset.ToString() // Atribui o horário do pôr do sol // Converte para string

                    }; // Fecha objeto Tempo

                }
                else
                {
                    throw new Exception("Erro ao obter dados da API: " + resp.StatusCode); // Lança uma exceção se a resposta não for bem-sucedida
                } // Fecha if (resp.IsSuccessStatusCode) // Verifica se a resposta foi bem-sucedida
            } // Fecha using HttpClient

            return t; // Retorna o objeto Tempo (nulo se não foi inicializado)
        } // Fecha método GetPrevisao
    }
}
