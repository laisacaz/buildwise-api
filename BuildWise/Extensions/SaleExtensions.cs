using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Extensions
{
    public static class SaleExtensions
    {
        public static string ToDescription(this ESaleStatus status)
        {
            return status switch
            {
                ESaleStatus.Open => "Aberto",
                ESaleStatus.Finalized=> "Finalizada"                
            };
        }
    }
}
