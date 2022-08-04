using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace EventbriteCalendar.Services.Eventbrite;

public class Pagination
{
    [JsonPropertyName("has_more_items")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public bool HasMoreItems { get; init; }

    [JsonPropertyName("continuation")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string? Continuation { get; init; }
}
