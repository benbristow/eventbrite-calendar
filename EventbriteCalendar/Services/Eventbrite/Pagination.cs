using System.Text.Json.Serialization;

namespace EventbriteCalendar.Services.Eventbrite;

public class Pagination
{
    [JsonPropertyName("object_count")]
    public int ObjectCount { get; init; }

    [JsonPropertyName("page_number")]
    public int PageNumber { get; init; }

    [JsonPropertyName("page_size")]
    public int PageSize { get; init; }

    [JsonPropertyName("page_count")]
    public int PageCount { get; init; }

    [JsonPropertyName("has_more_items")]
    public bool HasMoreItems { get; init; }

    [JsonPropertyName("continuation")]
    public string? Continuation { get; init; }
}
