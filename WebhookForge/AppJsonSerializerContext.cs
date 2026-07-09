using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebhookForge
{
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(Subscriber))]
    [JsonSerializable(typeof(Event))]
    [JsonSerializable(typeof(DeliveryAttempt))]
    [JsonSourceGenerationOptions(UseStringEnumConverter = true)]
    [JsonSerializable(typeof(Subscriber[]))]
    internal partial class AppJsonSerializerContext : JsonSerializerContext
    {

    }
}