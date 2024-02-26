namespace MyFinances.Common.Core.Results;

public record ResultApi(bool IsSuccess, List<Error>? Errors, object? Data = null);