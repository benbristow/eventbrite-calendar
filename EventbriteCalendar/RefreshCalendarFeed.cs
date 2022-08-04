using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using EventbriteCalendar.Services.Eventbrite;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace EventbriteCalendar;

public static class RefreshCalendarFeed
{
    [FunctionName(nameof(RefreshCalendarFeed))]
    public static async Task RunAsync(
        [TimerTrigger(
            "0 * * * *" // Every hour
#if DEBUG
            , RunOnStartup = true
#endif
        )]
        TimerInfo myTimer,
        ILogger logger)
    {
        logger.LogInformation("Starting Eventbrite calendar feed refresh");

        var eventbriteUserId = GetEnvironmentVariable("EVENTBRITE_USER_ID");
        var eventbriteApiToken = GetEnvironmentVariable("EVENTBRITE_API_TOKEN");
        var azureStorageConnectionString = GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
        var azureStorageContainerName = GetEnvironmentVariable("AZURE_STORAGE_CONTAINER_NAME");

        var client = new EventbriteClient(eventbriteUserId, eventbriteApiToken);
        var events = await client.GetEventsAsync();
        var calendar = BuildCalendarFromEvents(events);
        await UpdateBlobStorageWithCalendarAsync(azureStorageConnectionString, azureStorageContainerName, eventbriteUserId, calendar);

        logger.LogInformation("Finished Eventbrite calendar feed refresh");
    }

    private static string GetEnvironmentVariable(string name)
        => Environment.GetEnvironmentVariable(name) ?? throw new NullReferenceException($"{name} environment variable not set");

    private static Calendar BuildCalendarFromEvents(IEnumerable<Event> events)
    {
        var calendar = new Calendar();
        calendar.Events.AddRange(events
            .Select(e => new CalendarEvent
            {
                Summary = e.Name.Text,
                Description = e.Description.Html,
                Start = new CalDateTime(e.Start.Utc, "UTC"),
                End = new CalDateTime(e.End.Utc, "UTC"),
                Url = e.Url
            }));
        return calendar;
    }

    private static async Task UpdateBlobStorageWithCalendarAsync(
        string azureStorageConnectionString,
        string azureStorageContainerName,
        string eventbriteUserId,
        Calendar calendar)
    {
        var blobContainerClient = new BlobContainerClient(azureStorageConnectionString, azureStorageContainerName);
        var blobClient = blobContainerClient.GetBlobClient($"eventbriteCalendar-{eventbriteUserId}.ics");

        await blobClient.UploadAsync(
            BinaryData.FromString(new CalendarSerializer().SerializeToString(calendar)),
            overwrite: true);
    }
}
