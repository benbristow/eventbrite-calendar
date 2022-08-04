using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace EventbriteCalendar.Services.Eventbrite;

public class TextContainer
{
    [JsonPropertyName("text")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string Text { get; init; } = null!;

    [JsonPropertyName("html")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string Html { get; init; } = null!;
}
