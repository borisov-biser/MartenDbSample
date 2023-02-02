namespace MartenDbSample.WeatherForecast.Repository
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {

        public Task CreateAsync(WeatherForecastAggregate.WeatherForecast aggregate, CancellationToken ctx = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(WeatherForecastAggregate.WeatherForecast aggregate, CancellationToken ctx = default)
        {
            throw new NotImplementedException();
        }

        public Task<WeatherForecastAggregate.WeatherForecast?> GetByIdAsync(Guid streamId, CancellationToken ctx = default)
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetSelfAggregateProjectionAsync<T>(Guid streamId, CancellationToken ctx = default)
        {
            throw new NotImplementedException();
        }
    }
}
