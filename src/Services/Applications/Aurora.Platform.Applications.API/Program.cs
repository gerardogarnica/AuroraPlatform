using Aurora.Framework.Api;
using Aurora.Framework.Logging;
using Aurora.Platform.Applications.API.Data;
using Aurora.Platform.Applications.API.Repositories;

const string apiName = "Aurora Platform Applications";
const string apiDescription = "Aurora Platform applications services API.";

var builder = WebApplication.CreateBuilder(args);

// Add dependency injection
builder.Services.AddScoped<IApplicationContext, ApplicationContext>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithApiKey(apiName, apiDescription, 1);
builder.Services.AddStringEnumConverter();
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

app.UseMiddleware(typeof(ApiHandlerMiddleware));

app.UseAuthorization();

app.MapControllers();

app.Run();
