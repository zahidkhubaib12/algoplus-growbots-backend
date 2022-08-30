using GrowBotsAlgoplus.Models;
using System.Net;
using System.Net.Mail;

namespace GrowBotsAlgoplus.Connector
{
    public class ServiceConnector : IServiceConnector
    {
        public IConfiguration Configuration { get; }
        public ServiceConnector(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<bool> sendEmail(Email email)
        {
            var smtpClient = new SmtpClient
            {
                Host = this.Configuration["Email:Host"], // set your SMTP server name here
                Port = Convert.ToInt32(this.Configuration["Email:Port"]), // Port 
                EnableSsl = Convert.ToBoolean(this.Configuration["Email:EnableSsl"]),
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(this.Configuration["Email:EmailID"], this.Configuration["Email:Password"])
            };

            using (var mailMessage = new MailMessage(this.Configuration["Email:EmailID"], email.To, email.Subject, email.Body))
            {
                mailMessage.IsBodyHtml = true;

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
