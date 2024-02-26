namespace MyFinances.Common.Core.Results;

public sealed record Error(string Description)
{
    public static readonly Error None = new(string.Empty);
}