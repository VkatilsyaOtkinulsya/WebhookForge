namespace WebhookForge
{
    public class Subscriber
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Url { get; set; } = string.Empty;
        public string EncryptedSecret { get; set; } = string.Empty; // обратимое шифрование, не хеш — нужен для HMAC
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Event
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        public string Type { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum DeliveryStatus
    {
        Pending,
        Delivered,
        Failed,
        Exhausted
    }

    public class DeliveryAttempt
    {
        public int Id { get; set; } 
        public string EventId { get; set; } = string.Empty;
        public string SubscriberId { get; set; } = string.Empty;
        public DeliveryStatus Status { get; set; } = DeliveryStatus.Pending;
        public int AttemptNumber { get; set; }
        public int? ResponseCode { get; set; }
        public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;
    }
}
