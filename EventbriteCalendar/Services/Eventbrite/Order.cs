using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace EventbriteCalendar.Services.Eventbrite;

public class Order
{
    [JsonPropertyName("event")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public Event Event { get; init; } = null!;
}
