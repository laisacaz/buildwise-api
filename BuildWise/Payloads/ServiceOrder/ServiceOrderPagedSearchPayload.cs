namespace BuildWise.Payload.ServiceOrder
{
    public class ServiceOrderPagedSearchPayload : BasePagedSearchPayload
    {
        public string? Search { get; set; } = string.Empty;
    }
}
