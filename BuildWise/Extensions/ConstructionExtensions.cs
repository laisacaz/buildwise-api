using static BuildWise.Enum.ConstructionEnum;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Extensions
{
    public static class ConstructionExtensions
    {
        public static string ToDescription(this EStatusConstruction status)
        {
            return status switch
            {
                EStatusConstruction.Open => "Aberto",
                EStatusConstruction.Finalized => "Finalizada"
            };
        }
    }
}
