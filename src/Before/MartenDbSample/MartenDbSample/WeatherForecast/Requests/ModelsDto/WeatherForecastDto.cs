using MartenDbSample.WeatherForecast.WeatherForecastAggregate.Models;

namespace MartenDbSample.WeatherForecast.Requests.ModelsDto
{
    public record WeatherForecastDto(int TemperatureC, WeatherForecastSummaryDto Summary, string Description, DateTime Date)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
