using Aurora.Framework.Api;
using Aurora.Framework.Logging;

const string apiName = "Aurora Platform Identity";
const string apiDescription = "Aurora Platform identity services API.";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithApiKey(apiName, apiDescription, 1);
builder.Services.AddStringEnumConverter();

// Add Serilog configuration.
builder.Host.ConfigureSerilog();

var app = builder.Build();

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
