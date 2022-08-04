using System.Text.Json.Serialization;

namespace EventbriteCalendar.Services.Eventbrite;

public class TextContainer
{
    [JsonPropertyName("text")]
    public string Text { get; init; }

    [JsonPropertyName("html")]
    public string Html { get; init; }
}
