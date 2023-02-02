using MartenDbSample.WeatherForecast.Requests.Commands;
using MartenDbSample.WeatherForecast.Requests.Extensions;
using MartenDbSample.WeatherForecast.Requests.ModelsDto;
using MartenDbSample.WeatherForecast.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MartenDbSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ISender _mediatR;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ISender mediatR, ILogger<WeatherForecastController> logger)
        {
            _mediatR = mediatR;
            _logger = logger;
        }

        [HttpPost(Name = "Create")]
        public async Task<ActionResult<WeatherForecastDto>> Create(CreateWeatherForecastRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediatR.Send(new CreateWeatherForecast.Command(request), cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            
            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value);
        }
        
        [HttpPut(Name = "Update")]
        public async Task<IActionResult> Update(UpdateWeatherForecastTemperatureRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediatR.Send(new UpdateWeatherForecastTemperature.Command(request), cancellationToken);
            return result.IsSuccess ? Ok(result.Value.ToDto()) : BadRequest(result.Errors);
        }
        
        [HttpDelete(Name = "Stop")]
        public async Task<IActionResult> Stop(StopMonitoringWeatherForecastRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediatR.Send(new StopMonitoringWeatherForecast.Command(request), cancellationToken);
            return result.IsSuccess ? NoContent() : BadRequest(result.Errors);

        }

        [HttpGet(Name="Get")]
        public async Task<ActionResult<WeatherForecastDto>> Get(WeatherForecastRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _mediatR.Send(new WeatherForecastQuery.Query(request), cancellationToken);
            return result.IsSuccess ? Ok(result.Value.ToDto()) : NotFound();
        }


        [HttpGet( "Projection", Name ="Projection")]
        public async Task<ActionResult<WeatherForecastDto>> GetProjection(WeatherForecastRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _mediatR.Send(new WeatherForecastSelfAggregateProjection.Query(request), cancellationToken);
            return result.IsSuccess ? Ok(result.Value.ToDto()) : NotFound();
        }
    }
}