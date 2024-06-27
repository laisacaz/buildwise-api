namespace BuildWise.Entities
{
    public class Cashier
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public bool Status { get; set; }
        public decimal InitialValue { get; set; }
        public decimal? ClosureValue { get; set; }
        public decimal? AmountAvailable { get; set; }
        public decimal? Outs { get; set; }
        public decimal? Entries { get; set; }

    }
}
