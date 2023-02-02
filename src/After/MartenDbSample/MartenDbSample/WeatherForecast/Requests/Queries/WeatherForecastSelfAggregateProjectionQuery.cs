using Ardalis.Result;
using MartenDbSample.WeatherForecast.Repository;
using MartenDbSample.WeatherForecast.Requests.ModelsDto;
using MediatR;

namespace MartenDbSample.WeatherForecast.Requests.Queries
{
    public class WeatherForecastSelfAggregateProjection
    {
        public record Query(WeatherForecastRequest request) : IRequest<Result<WeatherForecastAggregate.WeatherForecast>>;

        public class Handler : IRequestHandler<Query, Result<WeatherForecastAggregate.WeatherForecast>>
        {
            private readonly IWeatherForecastRepository _repository;

            public Handler(IWeatherForecastRepository repository)
            {
                _repository = repository;
            }

            public async Task<Result<WeatherForecastAggregate.WeatherForecast>> Handle(Query query, CancellationToken cancellationToken)
            {
                var aggregate = await _repository.GetSelfAggregateProjectionAsync<WeatherForecastAggregate.WeatherForecast>(
                                                                              query.request.Id, cancellationToken);
                if (aggregate == null)
                    return Result<WeatherForecastAggregate.WeatherForecast>.Error($"Aggregate with Id: {query.request.Id} doesn't exists.");
                
                return Result.Success<WeatherForecastAggregate.WeatherForecast>(aggregate);
            }
        }
    }
}
