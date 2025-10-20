using Newtonsoft.Json;

namespace IngressoApi.Models;

/// <summary>
/// Represents a State, containing its name, abbreviation (UF), and a list of its cities.
/// </summary>
public class State : IEquatable<State> {
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("uf")] public string Uf { get; set; }
    [JsonProperty("cities")] public List<City> Cities { get; set; }

    public bool Equals(State? other) {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name.Equals(other.Name) && Uf.Equals(other.Uf);
    }

    public override bool Equals(object? obj) {
        return obj is State state && Equals(state);
    }

    public override int GetHashCode() {
        return HashCode.Combine(Name, Uf);
    }

    public override string ToString() => Name;
}

/// <summary>
/// Represents a City, containing its unique ID and name.
/// </summary>
public class City : IEquatable<City> {
    [JsonProperty("id")] public string Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; }

    public bool Equals(City? other) {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && Name == other.Name;
    }

    public override bool Equals(object? obj) {
        return obj is City city && Equals(city);
    }

    public override int GetHashCode() {
        return HashCode.Combine(Id, Name);
    }

    public override string ToString() => Name;
}

/// <summary>
/// Represents the full showtime information for a specific day at a theater.
/// </summary>
public class DailyShowtime {
    [JsonProperty("date")] public string Date { get; set; }
    [JsonProperty("dateFormatted")] public string DateFormatted { get; set; }
    [JsonProperty("dayOfWeek")] public string DayOfWeek { get; set; }
    [JsonProperty("isToday")] public bool IsToday { get; set; }
    [JsonProperty("movies")] public List<MovieShowtime> Movies { get; set; }
    [JsonProperty("sessionTypes")] public List<string> SessionTypes { get; set; } // Can be null
}

/// <summary>
/// Represents a single movie playing at the theater, including its details and session times.
/// </summary>
public class MovieShowtime {
    [JsonProperty("id")] public string Id { get; set; }
    [JsonProperty("b2BId")] public int? B2BId { get; set; } // Nullable int
    [JsonProperty("type")] public string Type { get; set; } // e.g., "Filme"
    [JsonProperty("title")] public string Title { get; set; }
    [JsonProperty("originalTitle")] public string OriginalTitle { get; set; }
    [JsonProperty("movieIdUrl")] public string MovieIdUrl { get; set; }
    [JsonProperty("inPreSale")] public bool InPreSale { get; set; }
    [JsonProperty("isReexhibition")] public bool IsReexhibition { get; set; }
    [JsonProperty("duration")] public string Duration { get; set; } // Duration in minutes, as a string
    [JsonProperty("contentRating")] public string ContentRating { get; set; }
    [JsonProperty("distributor")] public string Distributor { get; set; }
    [JsonProperty("urlKey")] public string UrlKey { get; set; }
    [JsonProperty("siteURL")] public string SiteURL { get; set; }
    [JsonProperty("nationalSiteURL")] public string NationalSiteURL { get; set; }
    [JsonProperty("siteURLByTheater")] public string SiteURLByTheater { get; set; }

    [JsonProperty("nationalSiteURLByTheater")]
    public string NationalSiteURLByTheater { get; set; }

    [JsonProperty("ancineId")] public string AncineId { get; set; }
    [JsonProperty("images")] public List<Image> Images { get; set; }
    [JsonProperty("trailers")] public List<Trailer> Trailers { get; set; }
    [JsonProperty("genres")] public List<string> Genres { get; set; }
    [JsonProperty("ratingDescriptors")] public List<string> RatingDescriptors { get; set; }
    [JsonProperty("accessibilityHubs")] public List<AccessibilityHub> AccessibilityHubs { get; set; }
    [JsonProperty("tags")] public List<string> Tags { get; set; }
    [JsonProperty("completeTags")] public List<TagDetails> CompleteTags { get; set; }
    [JsonProperty("rooms")] public List<RoomShowtime> Rooms { get; set; }
    [JsonProperty("sessionTypes")] public List<SessionTypeDetails> SessionTypes { get; set; } // Can be null
}

/// <summary>
/// Represents details for a trailer.
/// </summary>
public class Trailer {
    [JsonProperty("type")] public string Type { get; set; }
    [JsonProperty("url")] public string Url { get; set; }
    [JsonProperty("embeddedUrl")] public string EmbeddedUrl { get; set; }
}

/// <summary>
/// Represents accessibility hub information.
/// </summary>
public class AccessibilityHub {
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("description")] public string Description { get; set; }
    [JsonProperty("url")] public string Url { get; set; }
    [JsonProperty("resources")] public List<AccessibilityResource> Resources { get; set; }
}

/// <summary>
/// Represents accessibility resource information.
/// </summary>
public class AccessibilityResource {
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("imageUrl")] public string ImageUrl { get; set; }
    [JsonProperty("imageAltText")] public string ImageAltText { get; set; }
}

/// <summary>
/// Represents detailed tag information.
/// </summary>
public class TagDetails {
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("background")] public string Background { get; set; }
    [JsonProperty("color")] public string Color { get; set; }
}

/// <summary>
/// Represents detailed session type information.
/// </summary>
public class SessionTypeDetails {
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("alias")] public string Alias { get; set; }
    [JsonProperty("display")] public bool Display { get; set; }
    [JsonProperty("typeDescriptions")] public TypeDescription TypeDescriptions { get; set; }
}

/// <summary>
/// Represents type description details for session types.
/// </summary>
public class TypeDescription {
    [JsonProperty("summaryDescription")] public string SummaryDescription { get; set; }
    [JsonProperty("summaryImage")] public string SummaryImage { get; set; }
    [JsonProperty("detailedDescription")] public string DetailedDescription { get; set; }
    [JsonProperty("detailedImage")] public string DetailedImage { get; set; }
}

/// <summary>
/// Represents a specific screening room and the sessions within it.
/// </summary>
public class RoomShowtime {
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("type")] public List<string>? Type { get; set; }
    [JsonProperty("sessions")] public List<Session> Sessions { get; set; }
}

/// <summary>
/// Represents an individual screening time for a movie.
/// </summary>
public class Session {
    [JsonProperty("id")] public string Id { get; set; }
    [JsonProperty("time")] public string Time { get; set; }
    [JsonProperty("type")] public List<string> Type { get; set; } // e.g., ["3D", "VIP"]
}

// --- Corrected Theater Models ---

/// <summary>
/// Represents a detailed Theater object, matching the API response.
/// </summary>
public class Theater {
    [JsonProperty("id")] public string Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("urlKey")] public string UrlKey { get; set; }
    [JsonProperty("address")] public string Address { get; set; }
    [JsonProperty("cityId")] public string CityId { get; set; }
    [JsonProperty("cityName")] public string CityName { get; set; }
    [JsonProperty("uf")] public string Uf { get; set; }
    [JsonProperty("properties")] public TheaterProperties Properties { get; set; }
    [JsonProperty("functionalities")] public TheaterFunctionalities Functionalities { get; set; }
    [JsonProperty("geolocation")] public Geolocation Geolocation { get; set; }
    [JsonProperty("rooms")] public List<TheaterRoom> Rooms { get; set; }
}

public class TheaterProperties {
    [JsonProperty("hasBomboniere")] public bool HasBomboniere { get; set; }

    [JsonProperty("hasContactlessWithdrawal")]
    public bool HasContactlessWithdrawal { get; set; }

    [JsonProperty("hasSession")] public bool HasSession { get; set; }

    [JsonProperty("hasSeatDistancePolicy")]
    public bool HasSeatDistancePolicy { get; set; }

    [JsonProperty("hasSeatDistancePolicyArena")]
    public bool HasSeatDistancePolicyArena { get; set; }
}

public class TheaterFunctionalities {
    [JsonProperty("operationPolicyEnabled")]
    public bool OperationPolicyEnabled { get; set; }
}

public class Geolocation {
    [JsonProperty("lat")] public double Lat { get; set; }
    [JsonProperty("lng")] public double Lng { get; set; }
}

public class TheaterRoom {
    [JsonProperty("id")] public string Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("capacity")] public int Capacity { get; set; }
}

/// <summary>
/// Represents an image with a URL.
/// </summary>
public class Image {
    [JsonProperty("url")] public string Url { get; set; }
    [JsonProperty("type")] public string Type { get; set; } // e.g., "PosterPortrait", "PosterHorizontal"
}

/// <summary>
/// A helper class to deserialize the API's list response for theaters.
/// </summary>
internal class TheaterListResponse {
    [JsonProperty("items")] public List<Theater> Items { get; set; }
    [JsonProperty("count")] public int Count { get; set; }
}