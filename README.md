# Eventbrite ICS Calendar Generator

## Summary

Azure Function which generates an ICS (iCalendar) compatible event calendar for all of your Eventbrite orders and
uploads it to an Azure Blob Storage container.

Runs by default hourly, on the hour.

Third party - not affiliated/associated with Eventbrite in any way

## Required configuration variables

| Name                            | Description                                                                                                                        |
|---------------------------------|------------------------------------------------------------------------------------------------------------------------------------|
| `EVENTBRITE_USER_ID`              | Your Eventbrite User ID - Can be found in the URL from your ticket   listing on the EventBrite website                             |
| `EVENTBRITE_API_TOKEN`            | Your personal Eventbrite API token - can be retrieved from the Eventbrite   developer portal - https://www.eventbrite.com/platform |
| `AZURE_STORAGE_CONNECTION_STRING` | Azure Blob Storage connection string - can be retrieved from the Azure   Portal                                                    |
| `AZURE_STORAGE_CONTAINER_NAME`    | Azure Blob Storage container name                                                                                                  |

## Attributions

* ICal.NET - .NET iCal library for .NET - https://github.com/rianjs/ical.net
