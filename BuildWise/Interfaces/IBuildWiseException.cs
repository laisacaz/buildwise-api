using System.Net;

namespace BuildWise.Interfaces
{
    public interface IBuildWiseException
    {
            string MessageError { get; }
            string CodeError { get; }
            HttpStatusCode StatusHttp { get; }
            object[] Parameters { get; set; }
    }
}
