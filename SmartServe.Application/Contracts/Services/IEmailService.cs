using System.Threading.Tasks;

namespace SmartServe.Application.Contracts.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
