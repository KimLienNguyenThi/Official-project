using WebAPI.Helper;

namespace WebAPI.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
