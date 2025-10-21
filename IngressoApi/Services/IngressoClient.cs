using IngressoApi.Handlers;
using IngressoApi.Models;
using Newtonsoft.Json;

namespace IngressoApi.Services;

/// <summary>
/// A client to interact with the Ingresso.com API.
/// </summary>
public class IngressoClient {
    private readonly HttpClient _httpClient;

    public IngressoClient(string apiVersion, string partnership) {
        var partnershipHandler = new PartnershipHandler(partnership);

        _httpClient = new HttpClient(partnershipHandler) {
            BaseAddress = new Uri($"https://api-content.ingresso.com/{apiVersion}/")
        };
    }

    /// <summary>
    /// Gets a list of all available states and their contained cities.
    /// Perfect for populating State and City dropdowns.
    /// </summary>
    public async Task<ICollection<State>> GetAllStatesWithCitiesAsync() {
        var responseString = await _httpClient.GetStringAsync("states");
        return JsonConvert.DeserializeObject<ICollection<State>>(responseString) ?? [];
    }

    /// <summary>
    /// Gets a list of all theaters located in a specific city.
    /// </summary>
    /// <param name="cityId">The ID of the city.</param>
    public async Task<ICollection<Theater>> GetTheatersByCityAsync(string cityId) {
        var responseString = await _httpClient.GetStringAsync($"theaters/city/{cityId}");
        var response = JsonConvert.DeserializeObject<ICollection<Theater>>(responseString);
        return response ?? [];
    }

    /// <summary>
    /// Gets all movie sessions for a specific theater on a given day.
    /// </summary>
    /// <param name="cityId">The ID of the city where the theater is located.</param>
    /// <param name="theaterId">The ID of the theater.</param>
    /// <param name="date">The date to get sessions for. If null, gets all available dates.</param>
    public async Task<ICollection<DailyShowtime>> GetSessionsByTheaterAsync(string cityId, string theaterId, DateTime? date = null) {
        var url = $"sessions/city/{cityId}/theater/{theaterId}";

        if (date.HasValue) {
            url += $"?date={date.Value:yyyy-MM-dd}";
        }

        var responseString = await _httpClient.GetStringAsync(url);
        return JsonConvert.DeserializeObject<ICollection<DailyShowtime>>(responseString) ?? [];
    }
}