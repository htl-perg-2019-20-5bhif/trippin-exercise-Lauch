using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace trippin
{
    class Program
    {
        static HttpClient HttpClient = new HttpClient() { BaseAddress = new Uri("https://services.odata.org/TripPinRESTierService/(S(go543zq1iannbhdghz2zlvga))/") };
        static async Task Main(string[] args)
        {
            IEnumerable<UserJSON> users = await readJsonFileAsync();

            await userExistsAsync(users);
        }

        private static async Task postUserAsync(UserJSON user)
        {
            var content = new StringContent(JsonSerializer.Serialize(new User(user)), Encoding.UTF8, "application/json");
            var userPostResponse = await HttpClient.PostAsync("People", content);

            try
            {
                userPostResponse.EnsureSuccessStatusCode();
                Console.WriteLine("User " + user.UserName + " created successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Creating user " + user.UserName + "did not work");
            }
        }

        private static async Task userExistsAsync(IEnumerable<UserJSON> users)
        {
            foreach (var user in users)
            {
                var userResponse = await HttpClient.GetAsync("People('" + user.UserName + "')");

                if (!userResponse.IsSuccessStatusCode)
                {
                    await postUserAsync(user);
                }
            }
        }

        private static async Task<IEnumerable<UserJSON>> readJsonFileAsync()
        {
            var file = await File.ReadAllTextAsync("users.json");
            IEnumerable<UserJSON> users = JsonSerializer.Deserialize<IEnumerable<UserJSON>>(file);
            return users;
        }
    }
}
