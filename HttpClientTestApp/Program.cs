using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HttpClientTestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string baseUrl = "http://localhost:5250";
            using HttpClient client = new HttpClient { BaseAddress = new Uri(baseUrl) };

            try
            {
                // Opret en bruger som JSON
                var newUser = new
                {
                    UserName = "testUser3",
                    Password = "password123"
                };
                var jsonContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

                // Send POST-request til /User endpointet
                HttpResponseMessage response = await client.PostAsync("/User", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Succes! Svar fra serveren: " + result);
                }
                else
                {
                    Console.WriteLine("Fejl! Statuskode: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("En fejl opstod: " + ex.Message);
            }
        }
    }
}