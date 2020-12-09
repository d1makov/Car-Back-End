using Car.BLL.Dto.Email;
using Car.BLL.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;
using System.Threading.Tasks;

namespace Car.BLL.Services.Implementation
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly IWebHostEnvironment _env;
        private readonly ISmptClient _smtpClient;
        private readonly string _templatesFolderName;

        public EmailSenderService(EmailConfiguration emailConfig, IWebHostEnvironment env, ISmptClient smtpClient)//
        {
            _emailConfig = emailConfig;
            _env = env;
            _smtpClient = smtpClient;
            _templatesFolderName = "Templates";
        }

        protected virtual async Task<string> GetMessageTemplateFromFile(string templateFileName)
        {
            using var reader =
                new StreamReader(Path.Combine(_env.ContentRootPath, _templatesFolderName, templateFileName));
            return await reader.ReadToEndAsync();
        }

        [Obsolete]
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));

            emailMessage.To.AddRange(message.To);

            emailMessage.Subject = message.Subject;

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };

            return emailMessage;
        }

        [Obsolete]
        public async Task CancelJourneyAsync(MailingMessage message)
        {
            var body = await GetMessageTemplateFromFile("CancelJourney.cshtml");

            body = body.Replace("{USER.NAME}", message.Passanger.Name);
            body = body.Replace("{DRIVER.NAME}", message.Driver.Name);
            body = body.Replace("{TIME}", message.CancelDate.ToString());

            

            var messageTitle = new Message(
                new List<string>() { message.PassangerAddress.Address },
                $"Your yourney was cancelled", body);
            await _smtpClient.SendAsync(CreateEmailMessage(messageTitle), _emailConfig);

            // var emailMessage = CreateEmailMessage(messageTitle);

            // await SendAsync(emailMessage);
        }

    }
}





/*public async Task SendEmailAsync(Message message)
       {
           var emailMessage = CreateEmailMessage(message);

           await SendAsync(emailMessage);
       }*/

/*private async Task SendAsync(MimeMessage mailMessage)
{
    using (var client = new SmtpClient())
    {
        try
        {
            await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

            await client.SendAsync(mailMessage);
        }
        catch
        {
            throw;
        }
        finally
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}*/


/*public void SendEmail(Message message)
{
    var emailMessage = CreateEmailMessage(message);

    Send(emailMessage);
}*/

/*private void Send(MimeMessage mailMessage)
{
    using(var client = new SmtpClient())
    {
        try
        {
            client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

            client.Send(mailMessage);
        }
        catch
        {
            throw;
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
}*/
