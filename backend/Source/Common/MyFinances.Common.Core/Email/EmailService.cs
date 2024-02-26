using Microsoft.Extensions.Options;
using MyFinances.Common.Core.Email.Brevo;
using MyFinances.Common.Core.Email.Dtos;
using Refit;

namespace MyFinances.Common.Core.Email;

public class EmailService : IEmailService
{
    private readonly IBrevoApi _brevoApi;
    private readonly BrevoOptions _brevoOptions;

    public EmailService(IOptions<BrevoOptions> options)
    {
        _brevoOptions = options.Value;
        _brevoApi = RestService.For<IBrevoApi>(
            _brevoOptions.Url,
            new RefitSettings(new NewtonsoftJsonContentSerializer())
        );
    }

    public async Task SendEmailConfirmationCode(SendEmailConfirmationCodeDto emailConfirmationCodeDto)
    {
        var dynamicEmailDto = new DynamicEmailDto
        {
            TemplateId = _brevoOptions.EmailConfirmationCodeId,
            To =
            [
                new ToDto
                {
                    Email = emailConfirmationCodeDto.Email
                }
            ],
            Params = new ParamsDto
            {
                VerificationCode = emailConfirmationCodeDto.Code
            }
        };

        await _brevoApi.SendDynamicEmail(dynamicEmailDto, _brevoOptions.ApiKey);
    }
}