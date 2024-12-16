namespace BuildWise.DTO.Cashier
{
    public class CashierReceivementMethodsDTO
    {
        public decimal? Pix { get; set; }
        public decimal? CreditCard { get; set; }
        public decimal? DebitCard { get; set; }
        public decimal? Money { get; set; }
        public decimal? AmountAvailable { get; set; }
        public decimal? Entries { get; set; }
        public decimal? Outs { get; set; }
    }
}
