using Aurora.Framework.Api;
using Aurora.Framework.Logging;
using Aurora.Platform.Settings.API.Extensions;
using Aurora.Platform.Settings.Application;
using Aurora.Platform.Settings.Infrastructure;

const string apiName = "Aurora Platform Settings";
const string apiDescription = "Aurora Platform common settings services API.";

var builder = WebApplication.CreateBuilder(args);

// Add API configuration.
builder.Services.AddSettingsInfrastructureServices(builder.Configuration);
builder.Services.AddSettingsApplicationServices();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(apiName, apiDescription, 1);
builder.Services.AddStringEnumConverter();

// Add Serilog configuration.
builder.Host.ConfigureSerilog();

var app = builder.Build();

app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware(typeof(ExceptionHandlerMiddleware));

app.UseAuthorization();

app.MapControllers();

app.Run();
