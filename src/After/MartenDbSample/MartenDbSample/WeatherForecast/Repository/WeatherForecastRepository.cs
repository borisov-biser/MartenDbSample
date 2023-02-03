using Marten;
using MartenDbSample.Common.Abstractions;

namespace MartenDbSample.WeatherForecast.Repository
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly IDocumentSession _session;

        public WeatherForecastRepository(IDocumentSession session)
        {
            _session = session;
        }


        public async Task CreateAsync(WeatherForecastAggregate.WeatherForecast aggregate, CancellationToken ctx = default)
        {
            _session.Events.StartStream(aggregate.Id, aggregate.GetUncommittedEvents());
            await _session.SaveChangesAsync(ctx); 

            aggregate.ClearUncommittedEvents(); 
        }

        public async Task UpdateAsync(WeatherForecastAggregate.WeatherForecast aggregate, CancellationToken ctx = default)
        {
            _session.Events.Append(aggregate.Id, aggregate.GetUncommittedEvents());
            await _session.SaveChangesAsync(ctx);

            aggregate.ClearUncommittedEvents();

        }

        public async Task<WeatherForecastAggregate.WeatherForecast?> GetByIdAsync(Guid streamId, CancellationToken ctx = default)
        {
            var aggregate = WeatherForecastAggregate.WeatherForecast.CreateWeatherForecast(streamId);

            var events = await _session.Events.FetchStreamAsync(streamId, token: ctx);
            foreach (var @event in events)
            {
                aggregate.When(@event.Data as IEvent);
            }




            return await _session.Events.AggregateStreamAsync<WeatherForecastAggregate.WeatherForecast>(streamId, token: ctx);

        }

        public async Task<T?> GetSelfAggregateProjectionAsync<T>(Guid streamId, CancellationToken ctx = default)
        {
          return await _session.LoadAsync<T>(streamId, token: ctx);
        }
    }
}
