namespace WebhookForge
{
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
