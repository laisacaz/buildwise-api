namespace BuildWise.Payload.ServiceOrder
{
    public class ServiceOrderUpdatePayload
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
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
