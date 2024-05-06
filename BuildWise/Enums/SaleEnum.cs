namespace BuildWise.Enum
{
    public class SaleEnum
    {
        public enum ESaleReceivementMethod
        {
            Pix,
            Money,
            DebitCard,
            CreditCard
        }
        public enum ESaleStatus
        {
            Open,
            Canceled,
            Finalized
        }
        public enum ESaleSearchType
        {
            Id,
            ClientName
        }
    }
}
