using Refit;

namespace MyFinances.Common.Core.Email.Brevo;

[Headers("accept: application/json", "content-type: application/json")]
public interface IBrevoApi
{
    [Post("/smtp/email")]
    Task<HttpResponseMessage> SendDynamicEmail([Body] DynamicEmailDto dynamicEmail, [Header("api-key")] string apiKey);
}