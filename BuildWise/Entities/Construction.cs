using static BuildWise.Enum.ConstructionEnum;

namespace BuildWise.Entities
{
    public class Construction
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinalizedAt { get; set; } = null;
        public DateTime? CanceledAt { get; set; } = null;
        public int ClientId { get; set; }
        public string Observation { get; set; }
        public EStatusConstruction Status { get; set; }
    }
}
