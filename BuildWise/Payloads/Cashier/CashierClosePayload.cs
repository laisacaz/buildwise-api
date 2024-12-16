namespace BuildWise.Payload.Cashier
{
    public class CashierClosePayload
    {
        private int id;
        public void SetId(int id)
        {
            this.id = id;
        }
        public int GetId()
        {
            return id;
        }
        public decimal ClosureValue { get; set; }
    }
}
