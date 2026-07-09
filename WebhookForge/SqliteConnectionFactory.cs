using Microsoft.Data.Sqlite;

namespace WebhookForge
{
    public interface ISqliteConnectionFactory
    {
        SqliteConnection CreateConnection();
    }

    public class SqliteConnectionFactory : ISqliteConnectionFactory
    {
        private readonly string _connectionString;

        public SqliteConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqliteConnection CreateConnection()
        {
            // объект создаётся, но НЕ открывается здесь —
            // открытие/закрытие происходит в каждом репозиторном методе через using
            return new SqliteConnection(_connectionString);
        }
    }
}
