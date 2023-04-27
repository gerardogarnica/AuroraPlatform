using Aurora.Framework.Api;
using Aurora.Framework.Logging;
using Aurora.Framework.Security;
using Aurora.Platform.Security.Application;
using Aurora.Platform.Security.Infrastructure;
using Microsoft.EntityFrameworkCore;

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

// Add Serilog configuration.
builder.Host.ConfigureSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SecurityContext>();
    dbContext.Database.Migrate();
}


app.UseMiddleware(typeof(ApiHandlerMiddleware));

app.UseAuthorization();

app.MapControllers();

app.Run();
