using Car.BLL.Dto.Email;
using System.Threading.Tasks;

namespace Car.BLL.Services.Interfaces
{
    public interface IEmailSenderService
    {
        //void SendEmail(Message message);
        //Task SendEmailAsync(Message message);

        Task CancelJourneyAsync(MailingMessage message);
    }
}
