using Ardalis.Result;
using MartenDbSample.WeatherForecast.Repository;
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
               //// aggregate. WeatherForecastCreated();

               //await _repository.CreateAsync(aggregate, cancellationToken);

               //return Result<WeatherForecastDto>.Success(aggregate.ToDto());

               return Result<WeatherForecastAggregate.WeatherForecast>.Error();
            }
        }
    }
}
