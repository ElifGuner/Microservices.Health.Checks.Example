using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddHealthChecks(); // https://localhost:7155/health URL'inden health durumu kontrol edilebilir.

//ba�l� uygulamalar�n health check'i
/*
builder.Services.AddHealthChecks()
    .AddRedis("localhost:6379") //connection strinler yaz�l�r.
    .AddMongoDb("mongodb://localhost:27017")
    //.AddSqlServer("Server=.; Database=SagaStateMachine_StockAPIDB; User Id= sa; Password=AEG200520+; TrustServerCertificate=True"); 
    .AddSqlServer("Server=bss01; Database=Test1; User Id= sa; Password=q1w2e3r4t5*X; TrustServerCertificate=True");
//bu konfig�rasyonu yap�nca https://localhost:7155/health URL'inden health durumu kontrol edersek, e�er bu uygulamalardan birinde sorun varsa ya da eri�emezse uhhealthy diyecek.
*/

//AspNetCore.HealthChecks.UI.Client k�t�phanesi ile konfig�rasyon
builder.Services.AddHealthChecks()
    .AddRedis(
        redisConnectionString : "localhost:6379",
        name : "Redis Check",
        failureStatus : HealthStatus.Degraded | HealthStatus.Unhealthy,
        tags : new string[] {"redis"}
    )
    .AddMongoDb(
        mongodbConnectionString: "mongodb://localhost:27017",
        name: "MongoDB Check",
        failureStatus: HealthStatus.Degraded | HealthStatus.Unhealthy,
        tags: new string[] { "mongodb" }
    )
    .AddSqlServer(
        connectionString: "Server=bss01; Database=Test1; User Id= sa; Password=q1w2e3r4t5*X; TrustServerCertificate=True",
        name : "SqlServer",
        healthQuery :  "SELECT 1",
        failureStatus: HealthStatus.Degraded | HealthStatus.Unhealthy,
        tags: new string[] { "sqlserver", "sql", "db" }
    );
var app = builder.Build();

//app.UseHealthChecks("/health");
//AspNetCore.HealthChecks.UI.Client k�t�phanesi ile konfig�rasyon

app.UseHealthChecks("/health" , new HealthCheckOptions
{ 
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
