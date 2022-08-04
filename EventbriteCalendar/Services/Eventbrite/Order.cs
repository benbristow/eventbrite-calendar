using System.Text.Json.Serialization;

namespace EventbriteCalendar.Services.Eventbrite;

public class Order
{
    [JsonPropertyName("event_id")]
    public string EventId { get; init; }
}
