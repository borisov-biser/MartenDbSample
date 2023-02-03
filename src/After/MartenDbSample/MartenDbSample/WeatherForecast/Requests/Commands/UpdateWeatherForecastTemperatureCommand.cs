using Ardalis.Result;
using MartenDbSample.WeatherForecast.Repository;
using MartenDbSample.WeatherForecast.Requests.Extensions;
using MartenDbSample.WeatherForecast.Requests.ModelsDto;
using MediatR;

namespace MartenDbSample.WeatherForecast.Requests.Commands
{
    public static class UpdateWeatherForecastTemperature
    {
        public record Command(UpdateWeatherForecastTemperatureRequest Request) : IRequest<Result<WeatherForecastAggregate.WeatherForecast>>;

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

                aggregate.WeatherForecastUpdate(command.Request.TemperatureC, command.Request.Summary.ToDomain(), command.Request.Description, command.Request.Date);

                await _repository.UpdateAsync(aggregate, cancellationToken);

                return Result<WeatherForecastAggregate.WeatherForecast>.Success(aggregate);
            }
        }
    }
}
