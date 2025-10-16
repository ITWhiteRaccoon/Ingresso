using IngressoApi.Models;
using IngressoApi.Services;

namespace IngressoConsumer;

class Program {
    static async Task Main(string[] args) {
        // Create an instance of our API client
        var client = new IngressoClient("v0", "test");

        Console.WriteLine("--- Ingresso.com API Example ---");

        try {
            // --- Example 1: Get all states ---
            Console.WriteLine("\nFetching all states...");
            var states = await client.GetStatesAsync();
            if (states.Count > 0) {
                Console.WriteLine($"Found {states.Count} states. The first one is: {states[0].Name} ({states[0].UF})");
            }

            // --- Example 2: Get cities for a specific state (e.g., São Paulo) ---
            var stateUf = UF.SP;
            Console.WriteLine($"\nFetching cities for the state of {stateUf}...");
            var cities = await client.GetCitiesByStateAsync(stateUf);
            if (cities.Count > 0) {
                // Assuming the first city in the list is the capital for this example
                var firstCity = cities[0];
                Console.WriteLine($"Found {cities.Count} cities. The first is: {firstCity.Name}");

                // --- Example 3: Get theaters for that city ---
                Console.WriteLine($"\nFetching theaters for the city of {firstCity.Name} (ID: {firstCity.Id})...");
                var theaters = await client.GetTheatersByCityAsync(firstCity.Id);
                Console.WriteLine(theaters.Count > 0 ? $"Found {theaters.Count} theaters. The first one is: {theaters[0].Name}" : $"No theaters found for {firstCity.Name}.");
            }
            else {
                Console.WriteLine($"No cities found for {stateUf}.");
            }
        }
        catch (HttpRequestException e) {
            Console.WriteLine($"An error occurred while calling the API: {e.Message}");
        }
        catch (Exception e) {
            Console.WriteLine($"An unexpected error occurred: {e.Message}");
        }
    }
}