using Microsoft.Data.Sqlite;

namespace WebhookForge.Repositories
{
    public class EventRepository
    {
        private readonly ISqliteConnectionFactory _factory;

        public EventRepository(ISqliteConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task CreateAsync(Subscriber subscriber)
        {
            using var connection = _factory.CreateConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = """
                INSERT INTO Subscribers(Id, Url, EncryptedSecret, CratedAt)
                VALUES ($id, $url, $secret, $createdAt)
            """;
            command.Parameters.AddWithValue("$id", subscriber.Id);
            command.Parameters.AddWithValue("$url", subscriber.Url);
            command.Parameters.AddWithValue("$secret", subscriber.EncryptedSecret);
            command.Parameters.AddWithValue("$createdAt", subscriber.CreatedAt.ToString("o"));

            await command.ExecuteNonQueryAsync();
        }

        public async Task<Subscriber?> GetByIdAsync(string id)
        {
            using var connection = _factory.CreateConnection();
            await connection.OpenAsync();


            using var command = connection.CreateCommand();
            command.CommandText = "SELECT id, Url, EncryptedSecret, createdAt FROM Subscribers WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (!await reader.ReadAsync())
                return null;

            return MapReaderToSubscriber(reader);
        }

        public async Task<IReadOnlyList<Event>> GetAllAsync(string id)
        {
            using var connection = _factory.CreateConnection();
            await connection.OpenAsync();


            using var command = connection.CreateCommand();
            command.CommandText = "SELECT id, Url, EncryptedSecret, createdAt FROM Subscribers";

            var result = new List<Event>();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(MapReaderToEvent(reader));
            }

            return result;
        }


        public async Task<bool> UpdateUrlAsync(string id, string newUrl)
        {
            using var connection = _factory.CreateConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE Subscribers SET url = $url WHERE Id = $id";
            command.Parameters.AddWithValue("$url", newUrl);
            command.Parameters.AddWithValue("$id", id);

            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            using var connection = _factory.CreateConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Subscribers WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);

            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        private static Event MapReaderToEvent(SqliteDataReader reader)
        {
            return new Event
            {
                Id = reader.GetString(reader.GetOrdinal("Id")),
                Object = reader.GetString(reader.GetOrdinal("Object")),
                Action = reader.GetString(reader.GetOrdinal("Action")),
                Payload = reader.GetString(reader.GetOrdinal("Payload")),
                CreatedAt = DateTime.Parse(
            reader.GetString(reader.GetOrdinal("CreatedAt")),
            null,
            System.Globalization.DateTimeStyles.RoundtripKind)
            };
        }
    }
}
