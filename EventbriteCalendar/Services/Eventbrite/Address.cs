using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace EventbriteCalendar.Services.Eventbrite;

public class Address
{
    [JsonPropertyName("localized_address_display")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string LocalizedAddressDisplay { get; init; } = null!;

    [JsonPropertyName("latitude")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string Latitude { get; init; } = null!;

    [JsonPropertyName("longitude")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string Longitude { get; init; } = null!;
}
