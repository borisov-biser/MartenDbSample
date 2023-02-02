namespace MartenDbSample.WeatherForecast.Requests.ModelsDto
{
    public record CreateWeatherForecastRequest(int TemperatureC,
                                               WeatherForecastSummaryDto Summary,
                                               string Description, 
                                               DateTime Date);
}
