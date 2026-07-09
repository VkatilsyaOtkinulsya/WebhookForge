using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebhookForge;


var builder = WebApplication.CreateBuilder(args);

var connectionString = DatabaseConfiguration.BuildConnectionString(
    builder.Configuration,
    builder.Environment);

builder.Services.AddSingleton<ISqliteConnectionFactory>(_ => new SqliteConnectionFactory(connectionString));


builder.Services.AddAuthorization();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/health", () => Results.Ok("WebhookForge is running"));

app.Run();
