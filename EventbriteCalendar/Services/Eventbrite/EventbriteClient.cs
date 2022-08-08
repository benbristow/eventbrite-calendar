using System;
using System.Collections.Generic;
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

    public async Task<IEnumerable<Order>> GetOrdersAsync(
        List<Order>? orders = null,
        string? continuation = null)
    {
        while (true)
        {
            orders ??= new List<Order>();

            var parms = new Dictionary<string, string>
            {
                { "expand", "event.venue" }
            };
            if (continuation != null)
            {
                parms.Add("continuation", continuation);
            }

            var page = await GetAuthenticatedAsync<OrdersResponse>($"users/{_userId}/orders", parms);

            orders.AddRange(page.Orders);

            if (page.Pagination.HasMoreItems)
            {
                continuation = page.Pagination.Continuation;
                continue;
            }

            break;
        }

        return orders;
    }

    private async Task<T> GetAuthenticatedAsync<T>(string path, IReadOnlyDictionary<string, string>? parms = null, CancellationToken? cancellationToken = null)
    {
        cancellationToken ??= CancellationToken.None;
        var requestQueryParams = new Dictionary<string, string>(parms ?? new Dictionary<string, string>())
            { { "token", _token } };
        var responseString = await _httpClient.GetStringAsync($"{path}{QueryString.Create(requestQueryParams)}", cancellationToken.Value);
        return JsonSerializer.Deserialize<T>(responseString)!;
    }
}
