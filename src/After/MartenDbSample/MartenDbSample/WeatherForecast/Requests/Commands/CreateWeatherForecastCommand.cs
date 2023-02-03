using Ardalis.Result;
using MartenDbSample.WeatherForecast.Repository;
using MartenDbSample.WeatherForecast.Requests.Extensions;
using MartenDbSample.WeatherForecast.Requests.ModelsDto;
using MediatR;

namespace MartenDbSample.WeatherForecast.Requests.Commands
{
    public static class CreateWeatherForecast
    {
        public record Command(CreateWeatherForecastRequest Request) : IRequest<Result<WeatherForecastAggregate.WeatherForecast>>;

        public class Handler : IRequestHandler<Command, Result<WeatherForecastAggregate.WeatherForecast>>
        {
            private readonly IWeatherForecastRepository _repository;

            public Handler(IWeatherForecastRepository repository)
            {
                _repository = repository;
            }

            public async Task<Result<WeatherForecastAggregate.WeatherForecast>> Handle(Command command, CancellationToken cancellationToken)
            {

               // //TODO: Implement Aggregate
               var aggregate = WeatherForecastAggregate.WeatherForecast.CreateWeatherForecast(
                   command.Request.TemperatureC,
                   command.Request.Summary.ToDomain(),
                   command.Request.Description, 
                   command.Request.Date);

               await _repository.CreateAsync(aggregate, cancellationToken);

               return Result<WeatherForecastAggregate.WeatherForecast>.Success(aggregate);
            }
        }
    }
}
