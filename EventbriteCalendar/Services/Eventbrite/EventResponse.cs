using System;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace EventbriteCalendar.Services.Eventbrite;

public class Event
{
    [JsonPropertyName("name")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public TextContainer Name { get; init; } = null!;

    [JsonPropertyName("description")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public TextContainer Description { get; init; } = null!;

    [JsonPropertyName("url")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public Uri Url { get; init; } = null!;

    [JsonPropertyName("start")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public DateContainer Start { get; init; } = null!;

    [JsonPropertyName("end")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public DateContainer End { get; init; } = null!;
}
