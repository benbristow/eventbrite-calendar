using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace EventbriteCalendar.Services.Eventbrite;

public class OrdersResponse
{
    [JsonPropertyName("pagination")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public Pagination Pagination { get; init; } = null!;

    [JsonPropertyName("orders")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public IReadOnlyList<Order> Orders { get; init; } = null!;
}
