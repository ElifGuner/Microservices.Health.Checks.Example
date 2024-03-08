using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddRedis(redisConnectionString: "localhost:6379")
    //.AddMongoDb(
    //    mongodbConnectionString: "mongodb://localhost:27017",
    //    name: "MongoDB Check",
    //    failureStatus: HealthStatus.Degraded | HealthStatus.Unhealthy,
    //    tags: new string[] { "mongodb" }
    //)
    .AddSqlServer(connectionString: "Server=bss01; Database=Test1; User Id= sa; Password=q1w2e3r4t5*X; TrustServerCertificate=True");

var app = builder.Build();


//AspNetCore.HealthChecks.UI.Client kütüphanesi ile konfigürasyon

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
