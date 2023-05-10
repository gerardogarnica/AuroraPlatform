using Aurora.Framework.Api;
using Aurora.Framework.Logging;
using Aurora.Framework.Repositories;
using Aurora.Framework.Security;
using Aurora.Platform.Security.Application;
using Aurora.Platform.Security.Infrastructure;

const string apiName = "Aurora Platform Security";
const string apiDescription = "Aurora Platform security services API.";

var builder = WebApplication.CreateBuilder(args);

// Add API configuration.
builder.Services.AddSecurityApplicationServices();
builder.Services.AddSecurityInfrastructureServices(builder.Configuration);

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

app.Services.MigrateData<SecurityContext>(builder.Configuration);

app.UseMiddleware(typeof(ApiHandlerMiddleware));

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
