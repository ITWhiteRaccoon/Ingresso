using Newtonsoft.Json;

namespace IngressoApi.Models;

// Represents a Brazilian state
public class State {
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("uf")] public string UF { get; set; }
    [JsonProperty("cities")] public List<City> Cities { get; set; }
}

// Represents a city within a state
public class City {
    [JsonProperty("id")] public string Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("uf")] public string UF { get; set; }
    [JsonProperty("states")] public string State { get; set; }
}

// Represents a movie theater
public class Theater {
    [JsonProperty("id")] public string Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("address")] public string Address { get; set; }
    [JsonProperty("cityName")] public string CityName { get; set; }
}

// Represents a movie or event
public class Movie {
    [JsonProperty("id")] public string Id { get; set; }
    [JsonProperty("title")] public string Title { get; set; }
    [JsonProperty("synopsis")] public string Synopsis { get; set; }
    [JsonProperty("director")] public string Director { get; set; }
    [JsonProperty("cast")] public string Cast { get; set; }
    [JsonProperty("genres")] public List<string> Genres { get; set; }
    [JsonProperty("duration")] public int Duration { get; set; }
    [JsonProperty("contentRating")] public string Rating { get; set; }
    [JsonProperty("images")] public List<ImageInfo> Images { get; set; }
}

// Represents image data for a movie
public class ImageInfo {
    [JsonProperty("url")] public string Url { get; set; }
    [JsonProperty("type")] public string Type { get; set; }
}