using System.Net.Mail;
using System.Threading.Tasks;

namespace siwar.Infrastructure.Mail
{
    public interface IMailer
    {
        Task SendAsync<T>(T model, string view, MailMessage message);
    }
}
