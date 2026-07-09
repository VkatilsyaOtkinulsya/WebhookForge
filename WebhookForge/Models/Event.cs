namespace WebhookForge
{

    public static class EventActions
    {
        public const string Created = "created";
        public const string Updated = "updated";
        public const string Deleted = "deleted";
    }
    public class Event
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Object { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
