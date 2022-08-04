using System;
using System.Text.Json.Serialization;

namespace EventbriteCalendar.Services.Eventbrite;

public class DateContainer
{
    [JsonPropertyName("timezone")]
    public string Timezone { get; set; }

    [JsonPropertyName("local")]
    public DateTime Local { get; set; }

    [JsonPropertyName("utc")]
    public DateTime Utc { get; set; }
}
