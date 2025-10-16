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
    /// Fetches a list of all states.
    /// </summary>
    /// <returns>A list of State objects.</returns>
    public async Task<List<State>> GetStatesAsync() {
        var responseString = await _httpClient.GetStringAsync("states");
        return JsonConvert.DeserializeObject<List<State>>(responseString);
    }

    /// <summary>
    /// Fetches cities for a given state's abbreviation (UF).
    /// </summary>
    /// <param name="uf">The state abbreviation (e.g., "SP", "RJ").</param>
    /// <returns>A list of City objects.</returns>
    public async Task<List<City>> GetCitiesByStateAsync(UF uf) {
        var responseString = await _httpClient.GetStringAsync($"states/{uf}");
        var stateWithCities = JsonConvert.DeserializeObject<State>(responseString);
        return stateWithCities?.Cities;
    }

    /// <summary>
    /// Fetches theaters for a given city ID.
    /// </summary>
    /// <param name="cityId">The ID of the city.</param>
    /// <returns>A list of Theater objects.</returns>
    public async Task<List<Theater>> GetTheatersByCityAsync(string cityId) {
        if (string.IsNullOrWhiteSpace(cityId))
            throw new ArgumentNullException(nameof(cityId));

        var responseString = await _httpClient.GetStringAsync($"theaters/city/{cityId}");
        return JsonConvert.DeserializeObject<List<Theater>>(responseString);
    }

    /// <summary>
    /// Fetches details for a specific event (movie).
    /// </summary>
    /// <param name="eventId">The ID of the event.</param>
    /// <returns>A Movie object with event details.</returns>
    public async Task<Movie> GetEventDetailsAsync(string eventId) {
        if (string.IsNullOrWhiteSpace(eventId))
            throw new ArgumentNullException(nameof(eventId));

        var responseString = await _httpClient.GetStringAsync($"events/{eventId}");
        return JsonConvert.DeserializeObject<Movie>(responseString);
    }
}