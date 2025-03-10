using Microsoft.AspNetCore.Mvc;
using UserBaseAPI.Domain.Enums;

namespace UserBaseAPI.Domain.Extensions.ResultExtentions;

public static class ResultExtensions
{
    /// <summary>
    /// Creates an ActionResult from a service Result
    /// </summary>
    /// <returns>The action result.</returns>
    /// <param name="controller"></param>
    /// <param name="result">Service Result.</param>
    /// <typeparam name="T">The data type of the Result.</typeparam>
    public static ActionResult FromResult<T>(this ControllerBase controller, Result<T> result)
    {
        switch (result.ResultType)
        {
            case EResultType.Ok:
                if (!result.Data.Any())
                    return controller.NoContent();
                return controller.Ok(result); //result.Data
            case EResultType.NotFound:
                return controller.NotFound(result); //result.Errors
            case EResultType.Invalid:
                return controller.BadRequest(result); //result.Errors
            case EResultType.Unexpected:
                return controller.BadRequest(result); //result.Errors
            case EResultType.Unauthorized:
                return controller.Unauthorized();
            default:
                throw new Exception("An unhandled result has occurred as a result of a service call.");
        }
    }
}
