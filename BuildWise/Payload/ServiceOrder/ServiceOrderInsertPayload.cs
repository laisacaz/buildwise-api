namespace BuildWise.Payload.ServiceOrder
{
    public class ServiceOrderInsertPayload
    {
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
