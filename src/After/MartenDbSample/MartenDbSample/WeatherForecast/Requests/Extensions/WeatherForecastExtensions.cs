using MartenDbSample.WeatherForecast.Requests.ModelsDto;
using MartenDbSample.WeatherForecast.WeatherForecastAggregate.Models;

namespace MartenDbSample.WeatherForecast.Requests.Extensions
{
    public static class WeatherForecastExtensions
    {
        public static WeatherForecastDto ToDto(this WeatherForecastAggregate.WeatherForecast weatherForecast)
        {
            return new WeatherForecastDto(weatherForecast.TemperatureC,
                                          weatherForecast.Summary.ToDto(),
                                          weatherForecast.Description,
                                          weatherForecast.Date);
        }

        public static WeatherForecastSummaryDto ToDto(this WeatherForecastSummary weatherForecastSummary)
        {
            return (WeatherForecastSummaryDto)Enum.Parse(typeof(WeatherForecastSummaryDto), weatherForecastSummary.ToString());
        }

        public static WeatherForecastSummary ToDto(this WeatherForecastSummaryDto weatherForecastSummary)
        {
            return (WeatherForecastSummary)Enum.Parse(typeof(WeatherForecastSummary), weatherForecastSummary.ToString());
        }
    }
}
