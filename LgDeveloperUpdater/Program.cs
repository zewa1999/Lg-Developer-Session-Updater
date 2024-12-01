using System.Text.Json;

namespace LgDeveloperUpdater;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // 10 days
        int timeToSleep = 864_000;

        try
        {
            string token = "9f692715d03c3f0c2f350fa9ccb8cbbe5b590c04b547ad0619f1ea589a6c170d";//Environment.GetEnvironmentVariable("LG_DEVELOPER_TOKEN")!;
            Console.WriteLine($"The token is:{token}");
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Token not found. Application shutting down.");
                return;
            }

            Console.WriteLine("Welcome to Lg Developer Updater");

            while (true)
            {
                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri("https://developer.lge.com")
                };

                var response = await httpClient.GetAsync($"secure/ResetDevModeSession.dev?sessionToken={token}");
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonSerializer.Deserialize<LgResponse>(jsonString);

                if (response.IsSuccessStatusCode && jsonResponse!.Result == "success" &&
                    jsonResponse.ErrorCode == "200")
                {
                    Console.WriteLine($"{DateTime.Now:dd/MM/yyyy HH:mm:ss}--Developer mode extended successfully!");
                }
                else
                {
                    Console.WriteLine(
                        $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}--The request didn't worked: {response.StatusCode}: {jsonString}");
                }

                Thread.Sleep(timeToSleep);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}