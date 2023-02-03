using MartenDbSample.Common.Abstractions;
using MartenDbSample.WeatherForecast.WeatherForecastAggregate.Models;

namespace MartenDbSample.WeatherForecast.WeatherForecastAggregate.Events
{
    public record WeatherForecastTemeperatureUpdated(int TemperatureC,
                                                     WeatherForecastSummary Summary,
                                                     string Description,
                                                     DateTime Date) : IEvent;
}
