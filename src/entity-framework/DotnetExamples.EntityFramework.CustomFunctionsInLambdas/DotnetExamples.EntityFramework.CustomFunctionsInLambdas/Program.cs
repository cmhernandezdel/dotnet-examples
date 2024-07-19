using DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Configuration;
using DotnetExamples.EntityFramework.CustomFunctionsInLambdas.Database;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddEndpoints();

WebApplication app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();<
app.MapEndpoints();

using IServiceScope scope = app.Services.CreateScope();
DatabaseContext databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
IEnumerable<string> pendingMigrations = databaseContext.Database.GetPendingMigrations();
if (pendingMigrations.Any())
{
    await databaseContext.Database.MigrateAsync();
}

app.Run();