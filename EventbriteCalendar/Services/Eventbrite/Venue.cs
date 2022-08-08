using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace EventbriteCalendar.Services.Eventbrite;

public class Venue
{
    [JsonPropertyName("name")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string Name { get; init; } = null!;

    [JsonPropertyName("address")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public Address Address { get; init; } = null!;
}
