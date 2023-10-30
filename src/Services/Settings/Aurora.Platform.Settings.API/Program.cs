using Aurora.Framework.Api;
using Aurora.Framework.Logging;
using Aurora.Framework.Repositories;
using Aurora.Platform.Settings.Application;
using Aurora.Platform.Settings.Infrastructure;

const string apiName = "Aurora Platform Settings";
const string apiDescription = "Aurora Platform common settings services API.";

var builder = WebApplication.CreateBuilder(args);

// Add API configuration.
builder.Services.AddSettingsApplicationServices();
builder.Services.AddSettingsInfrastructureServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithApiKey(apiName, apiDescription, 1);
builder.Services.AddStringEnumConverter();
builder.Services.AddAuthenticationServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();

// Add Serilog configuration.
builder.Host.ConfigureSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.MigrateData<SettingsContext>(builder.Configuration);

app.UseMiddleware(typeof(ApiHandlerMiddleware));

app.UseAuthorization();

app.MapControllers();

app.Run();
