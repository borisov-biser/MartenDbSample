using MartenDbSample.Common.Abstractions;

namespace MartenDbSample.WeatherForecast.WeatherForecastAggregate.Events
{
    public record WeatherForecastMonitoringStopped(string Description, DateTime Date) : IEvent;
}
