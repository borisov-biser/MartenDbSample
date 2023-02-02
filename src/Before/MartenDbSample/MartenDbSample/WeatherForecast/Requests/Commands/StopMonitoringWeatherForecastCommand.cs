using Ardalis.Result;
using MartenDbSample.WeatherForecast.Repository;
using MartenDbSample.WeatherForecast.Requests.ModelsDto;
using MediatR;

namespace MartenDbSample.WeatherForecast.Requests.Commands
{
    public static class StopMonitoringWeatherForecast
    {
        public record Command(StopMonitoringWeatherForecastRequest Request) : IRequest<Result<WeatherForecastAggregate.WeatherForecast>>;

        public class Handler : IRequestHandler<Command, Result<WeatherForecastAggregate.WeatherForecast>>
        {
            private readonly IWeatherForecastRepository _repository;

            public Handler(IWeatherForecastRepository repository)
            {
                _repository = repository;
            }

            public async Task<Result<WeatherForecastAggregate.WeatherForecast>> Handle(Command command, CancellationToken cancellationToken)
            {
                var aggregate = await _repository.GetByIdAsync(command.Request.Id, cancellationToken);
                if (aggregate == null)
                    return Result<WeatherForecastAggregate.WeatherForecast>.Error($"Aggregate with Id: {command.Request.Id} doesn't exists.");

                //TODO: Implement Aggregate
                //aggregate.UpdateForecast(new WeatherForecastTemeperatureUpdated());

                await _repository.UpdateAsync(aggregate, cancellationToken);

                return Result<WeatherForecastAggregate.WeatherForecast>.Success(aggregate);
            }
        }
    }
}
