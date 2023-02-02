using MartenDbSample.Common.Abstractions;
using MartenDbSample.WeatherForecast.WeatherForecastAggregate.Models;

namespace MartenDbSample.WeatherForecast.WeatherForecastAggregate
{
    public sealed class WeatherForecast : Aggregate
    {
        public WeatherForecastStatus Status { get; } = WeatherForecastStatus.None;
        public DateTime Date { get; set; }

        public string Description { get; set; } = string.Empty;

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public WeatherForecastSummary Summary { get; set; } = WeatherForecastSummary.None;

        private WeatherForecast(Guid streamId) : base(streamId)
        {
        }

    }
}