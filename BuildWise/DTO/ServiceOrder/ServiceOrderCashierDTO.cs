namespace BuildWise.DTO.ServiceOrder
{
    public class ServiceOrderCashierDTO
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public bool Status { get; set; }
        public decimal Amount { get; set; }
    }
}
