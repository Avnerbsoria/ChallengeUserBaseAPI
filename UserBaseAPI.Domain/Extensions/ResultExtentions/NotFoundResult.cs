using System.Net;
using UserBaseAPI.Domain.Enums;

namespace UserBaseAPI.Domain.Extensions.ResultExtentions;

/// <summary>
/// Not found result
/// </summary>
/// <typeparam name="T"></typeparam>
public class NotFoundResult<T> : Result<T>
{
    private readonly string _error;
    public NotFoundResult(string error)
    {
        _error = error;
    }

    public override bool IsSuccess => false;
    public override EResultType ResultType => EResultType.NotFound;
    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public override List<string> Errors => new() { _error ?? "objeto não encontrado" };
    public override IEnumerable<T> Data => default;
}
