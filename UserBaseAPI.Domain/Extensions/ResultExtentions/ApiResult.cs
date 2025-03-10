using System.Net;
using UserBaseAPI.Domain.Enums;

namespace UserBaseAPI.Domain.Extensions.ResultExtentions;

/// <summary>
/// Result model to issuccess, contain data, result type (status code), and errors
/// </summary>

public class ApiResult<T>
{
    public bool IsSuccess { get; set; }
    public EResultType ResultType { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public List<string> Errors { get; set; }
    public IEnumerable<T> Data { get; set; }
}
