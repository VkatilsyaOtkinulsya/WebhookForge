using Microsoft.Data.Sqlite;

namespace WebhookForge
{
    public static class DatabaseConfiguration
    {
        public static string BuildConnectionString(IConfiguration configuration, IHostEnvironment environment)
        {
            // сначала пробуем взять путь из конфигурации (appsettings.json / переменные окружения)
            var configuredPath = configuration["Database:Path"];

            // если не задано — используем путь по умолчанию рядом с приложением
            var dbPath = string.IsNullOrWhiteSpace(configuredPath)
                ? Path.Combine(environment.ContentRootPath, "database.db")
                : configuredPath;

            return new SqliteConnectionStringBuilder
            {
                DataSource = dbPath,
                Mode = SqliteOpenMode.ReadWriteCreate,
                Cache = SqliteCacheMode.Shared
            }.ToString();
        }
    }
}
