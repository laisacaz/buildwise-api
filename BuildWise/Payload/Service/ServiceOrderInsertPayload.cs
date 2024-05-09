namespace BuildWise.Payload.Service
{
    public class ServiceOrderInsertPayload
    {
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
