using TMParking_Backend.Models;

namespace TMParking_Backend.UtilityService
{
    public interface IEmailService
    {
        void SendEmail(EmailModel emailModel);
    }
}
