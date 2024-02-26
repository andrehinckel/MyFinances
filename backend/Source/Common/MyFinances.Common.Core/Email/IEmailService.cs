using MyFinances.Common.Core.Email.Dtos;

namespace MyFinances.Common.Core.Email;

public interface IEmailService
{
    Task SendEmailConfirmationCode(SendEmailConfirmationCodeDto emailConfirmationCodeDto);
}