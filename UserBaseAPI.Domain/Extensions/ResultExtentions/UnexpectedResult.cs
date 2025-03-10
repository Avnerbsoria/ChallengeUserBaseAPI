using System.Net;
using UserBaseAPI.Domain.Enums;

namespace UserBaseAPI.Domain.Extensions.ResultExtentions;

/// <summary>
/// Unexpected result
/// </summary>
public class UnexpectedResult<T> : Result<T>
{
    private readonly string _error;

    public UnexpectedResult(string error)
    {
        _error = error;
    }

    public override bool IsSuccess => false;
    public override EResultType ResultType => EResultType.Unexpected;
    public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
    public override List<string> Errors => new() { _error ?? "Ocorreu um erro inesperado" };
    public override IEnumerable<T>? Data => default;
}
