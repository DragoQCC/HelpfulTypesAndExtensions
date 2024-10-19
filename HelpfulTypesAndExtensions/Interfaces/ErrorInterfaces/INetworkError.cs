using System.Net;
using System.Net.Http.Headers;

namespace HelpfulTypesAndExtensions;

public interface INetworkError<T> : IError
where T : struct, INetworkError<T>
{
    public HttpStatusCode? StatusCode { get; set; }
    public IEnumerable<HttpHeaders>? Headers { get; set; }
}