using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EventbriteCalendar.Services.Eventbrite;

public class OrdersResponse
{
    [JsonPropertyName("pagination")]
    public Pagination Pagination { get; init; }

    [JsonPropertyName("orders")]
    public IReadOnlyList<Order> Orders { get; init; }
}
