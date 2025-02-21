using SendGrid;
using SendGrid.Helpers.Mail;

namespace USAFlag.Auth.Core.Infrastructure.Persistence.Email
{
    public interface IEmailService
    {
        Task<Response> SendEmail(string apiKey, string subject,
            string message, List<string> emails, string fromEmail);
    }

    public class EmailService : IEmailService
    {
        public Task<Response> SendEmail(string apiKey, string subject,
            string message, List<string> emails, string fromEmail)
        {
            try
            {
                var sendGridClient = new SendGridClient(apiKey);
                var sendGridMessage = new SendGridMessage()
                {
                    From = new EmailAddress("info@a.com", "no-reply"),
                    Subject = subject,
                    PlainTextContent = message,
                    HtmlContent = message
                };
                foreach (var email in emails)
                {
                    sendGridMessage.AddTo(new EmailAddress(email));
                }
                Task<Response> response = sendGridClient.SendEmailAsync(sendGridMessage);
                return response;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
