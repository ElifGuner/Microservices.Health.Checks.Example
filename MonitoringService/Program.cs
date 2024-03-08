var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecksUI(settings =>
{
    settings.AddHealthCheckEndpoint("Service A", "https://localhost:7155/health");
    settings.AddHealthCheckEndpoint("Service B", "https://localhost:7012/health");
}).AddSqlServerStorage("Server=bss01; Database=HealthCheckUIDB; User Id= sa; Password=q1w2e3r4t5*X; TrustServerCertificate=True");
//bu konfig�rasyonla HealthCheckUIDB yoksa olu�turur, varsa migrate eder. Extra bir �al��ma yapmaya gerek yok.

var app = builder.Build();

app.UseHealthChecksUI(settings => settings.UIPath = "/health-ui");

app.Run();
