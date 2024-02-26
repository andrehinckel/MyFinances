using Microsoft.AspNetCore.Http;

namespace MyFinances.Common.Core.Results.Extensions;

public static class ResultExtensions
{
    public static IResult ToResult(
        this Result result,
        IResult? onSuccess = null,
        IResult? onFailure = null)
    {
        if (result.IsSuccess)
            return onSuccess ?? Microsoft.AspNetCore.Http.Results.Ok(result);

        return onFailure ?? Microsoft.AspNetCore.Http.Results.BadRequest(result);
    }

    public static IResult ToResult<T>(
        this Result<T> result,
        IResult? onSuccess = null,
        IResult? onFailure = null)
    {
        if (result.IsSuccess)
            return onSuccess ?? Microsoft.AspNetCore.Http.Results.Ok(result);

        return onFailure ?? Microsoft.AspNetCore.Http.Results.BadRequest(result);
    }
}