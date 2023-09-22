using Aurora.Framework.Logging;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var ocelotConfigFileName = $"ocelot.{builder.Environment.EnvironmentName}.json";
builder.Configuration.AddJsonFile(ocelotConfigFileName, false, true);
builder.Services.AddOcelot(builder.Configuration).AddCacheManager(x => x.WithDictionaryHandle());

// Add Serilog configuration.
builder.Host.ConfigureSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/", () => "Hello World!");
await app.UseOcelot();

app.Run();
