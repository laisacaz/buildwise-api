using static BuildWise.Enums.ReportEnum;
using static FastReport.Export.Html.HTMLExport;

namespace BuildWise.Extensions
{
    public static class ReportExtensions
    {
        public static string GetReportDirectory(this EReportType reportType)
        {
            return reportType switch
            {
                EReportType.SaleByDate => Path.Combine("Sale", "SaleByDate"),
                EReportType.ProductRanking => Path.Combine("Product", "ProductRanking"),
                EReportType.Sale => Path.Combine("Sale", "Sale"),
                EReportType.Cashier => Path.Combine("Cashier", "Cashier"),

                _ => throw new System.NotImplementedException()
            };
        }
    } 
}
