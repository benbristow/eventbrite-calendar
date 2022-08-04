using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EventbriteCalendar.Services.Eventbrite;

public class EventbriteClient
{
    private readonly string _userId;
    private readonly string _token;

    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://www.eventbriteapi.com/v3/")
    };

    public EventbriteClient(string userId, string token)
    {
        _userId = userId;
        _token = token;
    }

    public async Task<IEnumerable<Event>> GetEventsAsync()
    {
        var orderIds = await GetUpcomingEventIdsFromOrdersAsync();
        return await GetEventsAsync(orderIds);
    }

    private async Task<IEnumerable<string>> GetUpcomingEventIdsFromOrdersAsync(
        List<string>? orderIds = null,
        string? continuation = null)
    {
        while (true)
        {
            orderIds ??= new List<string>();

            var page = await GetAuthenticatedAsync<OrdersResponse>($"users/{_userId}/orders",
                continuation != null ? new Dictionary<string, string> { { "continuation", continuation } } : null);
            orderIds.AddRange(page.Orders.Select(o => o.EventId));
            if (page.Pagination.HasMoreItems)
            {
                continuation = page.Pagination.Continuation;
                continue;
            }

            break;
        }

        return orderIds;
    }

    private async Task<IEnumerable<Event>> GetEventsAsync(IEnumerable<string> eventIds)
    {
        var events = new ConcurrentBag<Event>();

        await Parallel.ForEachAsync(
            eventIds,
            new ParallelOptions { MaxDegreeOfParallelism = 5 },
            async (eventId, cancellationToken) =>
            {
                events.Add(await GetAuthenticatedAsync<Event>($"events/{eventId}", cancellationToken: cancellationToken));
            });

        return events.OrderByDescending(e => e.Start.Utc).ToImmutableList();
    }

    private async Task<T> GetAuthenticatedAsync<T>(string path, IReadOnlyDictionary<string, string>? queryParams = null, CancellationToken? cancellationToken = null)
    {
        cancellationToken ??= CancellationToken.None;
        var requestQueryParams = new Dictionary<string, string>(queryParams ?? new Dictionary<string, string>())
            { { "token", _token } };
        var responseString = await _httpClient.GetStringAsync($"{path}{QueryString.Create(requestQueryParams)}", cancellationToken.Value);
        return JsonSerializer.Deserialize<T>(responseString)!;
    }
}
