using System;
using System.Text.Json.Serialization;

namespace EventbriteCalendar.Services.Eventbrite;

public class Event
{
    [JsonPropertyName("name")]
    public TextContainer Name { get; init; }

    [JsonPropertyName("description")]
    public TextContainer Description { get; init; }

    [JsonPropertyName("url")]
    public Uri Url { get; init; }

    [JsonPropertyName("start")]
    public DateContainer Start { get; init; }

    [JsonPropertyName("end")]
    public DateContainer End { get; init; }
}

