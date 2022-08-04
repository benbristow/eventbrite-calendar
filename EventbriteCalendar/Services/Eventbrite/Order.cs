using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace EventbriteCalendar.Services.Eventbrite;

public class Order
{
    [JsonPropertyName("event_id")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public string EventId { get; init; } = null!;
}
