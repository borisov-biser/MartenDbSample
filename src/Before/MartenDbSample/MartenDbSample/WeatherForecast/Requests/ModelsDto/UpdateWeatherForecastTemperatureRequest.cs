namespace MartenDbSample.WeatherForecast.Requests.ModelsDto
{
    public record UpdateWeatherForecastTemperatureRequest(Guid Id, 
                                                          int TemperatureC,
                                                          WeatherForecastSummaryDto Summary,
                                                          string Description,
                                                          DateTime Date);
}
