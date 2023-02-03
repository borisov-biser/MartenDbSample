using System.Text.Json.Serialization;
using Marten;
using Marten.Events.Daemon.Resiliency;
using Marten.Events.Projections;
using MartenDbSample.WeatherForecast.Repository;
using MartenDbSample.WeatherForecast.WeatherForecastAggregate;
using MediatR;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

const string _databaseSchemaName = "weatherforecast_eventstore";
string _connectionString = builder.Configuration.GetConnectionString("Postgre");

//TODO: Implementation
//Add MartenDb Configuration
builder.Services.AddMarten(provider =>
    {
        var opts = new StoreOptions();
        opts.Connection(_connectionString);
        opts.DatabaseSchemaName = _databaseSchemaName;
        opts.Events.DatabaseSchemaName = _databaseSchemaName;
        opts.UseDefaultSerialization(EnumStorage.AsInteger, nonPublicMembersStorage: NonPublicMembersStorage.All);

        opts.AutoCreateSchemaObjects = builder.Environment.IsDevelopment() ? AutoCreate.All : AutoCreate.None;


        opts.Projections.SelfAggregate<WeatherForecast>(ProjectionLifecycle.Async);

        return opts;
    }
).AddAsyncDaemon(DaemonMode.HotCold);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
