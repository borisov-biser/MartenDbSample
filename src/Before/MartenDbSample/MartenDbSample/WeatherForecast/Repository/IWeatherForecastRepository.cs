using MartenDbSample.Common.Abstractions;

namespace MartenDbSample.WeatherForecast.Repository
{
    public interface IWeatherForecastRepository : IRepository<WeatherForecastAggregate.WeatherForecast>
    {
    }
}
