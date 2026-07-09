namespace WebhookForge
{
    public class Subscriber
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Url { get; set; } = string.Empty;
        public string EncryptedSecret { get; set; } = string.Empty; // обратимое шифрование, не хеш — нужен для HMAC
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
