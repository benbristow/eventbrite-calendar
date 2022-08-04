using System;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace EventbriteCalendar.Services.Eventbrite;

public class DateContainer
{
    [JsonPropertyName("utc")]
    [UsedImplicitly(ImplicitUseKindFlags.Assign)]
    public DateTime Utc { get; set; }
}
