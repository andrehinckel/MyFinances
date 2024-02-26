namespace MyFinances.Common.Core.Email.Brevo;

public class BrevoOptions
{
    public string ApiKey { get; init; }
    public string Url { get; init; }
    public int EmailConfirmationCodeId { get; init; }
}