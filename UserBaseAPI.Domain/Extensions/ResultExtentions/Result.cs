using System.Net;
using UserBaseAPI.Domain.Enums;

namespace UserBaseAPI.Domain.Extensions.ResultExtentions;

/// <summary>
/// Result model to issuccess, contain data, result type (status code), and errors
/// </summary>
public abstract class Result<T>
{
    public abstract bool IsSuccess { get; }
    public abstract EResultType ResultType { get; }
    public abstract HttpStatusCode StatusCode { get; }
    public abstract List<string> Errors { get; }
    public abstract IEnumerable<T> Data { get; }
}
