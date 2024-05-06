using BuildWise.DTO.Report;
using BuildWise.Extensions;
using BuildWise.Interfaces.Service.Report;
using BuildWise.Provider;
using FastReport;
using FastReport.Data.JsonConnection;
using FastReport.Export.PdfSimple;
using FastReport.Utils;
using FastReport.Web;
using Newtonsoft.Json;
using NJsonSchema;
using static BuildWise.Enums.ReportEnum;

namespace BuildWise.Services.Service.Report
{
    public class OpenFastReportService : IReportService
    {
        private readonly WebReport webReport;
        private readonly IConfiguration configuration;
        public OpenFastReportService(
            IConfiguration configuration)
        {
            webReport = new WebReport();
            Config.WebMode = true;
            this.configuration = configuration;
        }

        public Task<PdfDTO> GeneratePDF<T>(
            EReportType reportType,
            T data,
            IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            //https://fastreports.github.io/FastReport.Documentation/ReferenceReportObject.html

            string reportPath = GetReportFilePath(reportType);
            string jsonConnectionString = GetJsonConnectionString(data);

            webReport.Report.Load(reportPath);
            webReport.Report.Dictionary.Connections[0].ConnectionString = jsonConnectionString;
            SetParameters(parameters);
            ConfigComponents();
            webReport.Report.Prepare();

            return GenerateFromFastReportPDFExport();
        }
        private Task<PdfDTO> GenerateFromFastReportPDFExport()
        {
            MemoryStream stream = new();
            webReport.Report.Export(new PDFSimpleExport(), stream);
            stream.Flush();
            stream.Position = 0;

            return Task.FromResult(new PdfDTO(
                content: stream,
            name: "report.pdf"));
        }
        private void SetParameters(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if (parameters is not null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    webReport.Report.SetParameterValue(parameter.Key, parameter.Value);
                }
            }
        }
        private string GetReportFilePath(EReportType reportType)
        {
            string basePath = Directory.GetCurrentDirectory();
            string componentReportPath = Path.Combine("wwwroot", "Report", "FastReport", reportType.GetReportDirectory());
            componentReportPath = Path.ChangeExtension(componentReportPath, "frx");
            string path = Path.Combine(basePath, componentReportPath);
            return path;
        }
        private static string GetJsonConnectionString<T>(T data)
        {
            //https://fabiomorcillopro.medium.com/gerando-relat%C3%B3rios-utilizando-gerador-gratuito-com-net-5-33cf8c350235

            string jsonData = JsonConvert.SerializeObject(
                data,
                typeof(T),
                JsonProvider.GetNewtonSoft());

            JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType(data.GetType());
            JsonDataSourceConnectionStringBuilder jsonBuilder = new()
            {
                JsonSchema = jsonSchema.ToJson(),
                Json = jsonData
            };
            return jsonBuilder.ConnectionString;
        }
        private void ConfigComponents()
        {
            foreach (object @object in webReport.Report.AllObjects)
            {
                if (@object is not ReportComponentBase)
                    continue;
            }
        }
    }
}
