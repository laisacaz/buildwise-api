using BuildWise.DTO.Report;
using static BuildWise.Enums.ReportEnum;
using static FastReport.Export.Html.HTMLExport;

namespace BuildWise.Interfaces.Service.Report
{
    public interface IReportService
    {
        /// <summary>
        /// Generate PDF of list data
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="reportType"> </param>
        /// <param name="data"> </param>
        /// <param name="parameters"> Customized report parameters </param>
        /// <param name="topMilimiters"> </param>
        /// <param name="bottomMilimiters"> </param>
        /// <param name="leftMilimiters"> </param>
        /// <param name="rightMilimiters"> </param>
        /// <returns> </returns>
        Task<PdfDTO> GeneratePDF<T>(
            EReportType reportType,
            T data,
            IEnumerable<KeyValuePair<string, object>> parameters = null);
    }
}
