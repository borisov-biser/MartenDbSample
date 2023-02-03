using MartenDbSample.Common.Abstractions;
using MartenDbSample.WeatherForecast.WeatherForecastAggregate.Events;
using MartenDbSample.WeatherForecast.WeatherForecastAggregate.Models;

namespace MartenDbSample.WeatherForecast.WeatherForecastAggregate
{
    public sealed class WeatherForecast : Aggregate
    {
        public WeatherForecastStatus Status { get; private set; } = WeatherForecastStatus.None;
        public DateTime Date { get; set; }

        public string Description { get; set; } = string.Empty;

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public WeatherForecastSummary Summary { get; set; } = WeatherForecastSummary.None;

        private WeatherForecast() : base()
        {
        }

        private WeatherForecast(Guid streamId) : base(streamId)
        {
        }

        private WeatherForecast(int TemperatureC,
            WeatherForecastSummary Summary,
            string Description,
            DateTime Date) : base(Guid.NewGuid())
        {
            var @event = new WeatherForecastCreated(TemperatureC, Summary, Description, Date);
            AddUncommittedEvent(@event);
            Apply(@event);
        }

        public static WeatherForecast CreateWeatherForecast(Guid id)
            => new WeatherForecast(id);

        public static WeatherForecast CreateWeatherForecast(int TemperatureC,
                                            WeatherForecastSummary Summary,
                                            string Description,
                                            DateTime Date)
        {
            return new WeatherForecast(TemperatureC, Summary, Description, Date);
        }


        public void WeatherForecastUpdate(int TemperatureC,
            WeatherForecastSummary Summary,
            string Description,
            DateTime Date)
        {

            var @event = new WeatherForecastTemeperatureUpdated(TemperatureC, Summary, Description, Date);
            AddUncommittedEvent(@event);
            Apply(@event);
        }

        public void WeatherForecastMonitoringStopped(string Description, DateTime Date)
        {
            if (Status is not WeatherForecastStatus.Closed)
            {
                var @event = new WeatherForecastMonitoringStopped(Description, Date);
                AddUncommittedEvent(@event);
                Apply(@event);
            }
        }


        private void Apply(WeatherForecastCreated @event)
        {
            Status = WeatherForecastStatus.Create;
        }


        private void Apply(WeatherForecastTemeperatureUpdated @event)
        {
            Status = WeatherForecastStatus.Update;
        }


        private void Apply(WeatherForecastMonitoringStopped @event)
        {
            Status = WeatherForecastStatus.Closed;
        }

        public override void When(IEvent @event)
        {
            switch (@event)
            {
                case WeatherForecastCreated created:
                    Apply(created);
                    break;
                case WeatherForecastTemeperatureUpdated updated:
                    Apply(updated);
                    break;

                case WeatherForecastMonitoringStopped stopped:
                    Apply(stopped);
                    break;
            }

        }
    }
}