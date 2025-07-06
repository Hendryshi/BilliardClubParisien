using BCP.Application.Entity.Models;
using BCP.Application.Interfaces;
using BCP.Domain.Configuration;
using Common.Application.Services.Helpers;
using FluentResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using Common.Application.Services.Logging;
using System.Text;

namespace BCP.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly SMTPOptions _options;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<SMTPOptions> options, ILogger<EmailService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task<Result> SendEmail(EmailCommand command)
        {
            try
            {
                var smtpClient = new SmtpClient(_options.Address)
                {
                    Port = _options.Port,
                    Credentials = new NetworkCredential(_options.Login, _options.Pwd),
                    EnableSsl = _options.EnableSsl,
                    Timeout = 10000
                };

                var senderMail = command.From;
                if(string.IsNullOrEmpty(senderMail))
                    senderMail = command.NoResponse.Value ? _options.Sender.FromNoResponseAddress : _options.Sender.FromAdress;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderMail),
                    Subject = command.Object,
                    Body = command.Body,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8,
                };

                if(command.To.Count == 0)
                    command.To.Add(_options.DefaultReceiver);

                foreach(var recipient in command.To)
                {
                    mailMessage.To.Add(new MailAddress(recipient));
                }

                foreach(var recipient in command.CC)
                {
                    mailMessage.CC.Add(new MailAddress(recipient));
                }

                List<MemoryStream> streams = new List<MemoryStream>();
                foreach(var attachment in command.Attachments)
                {
                    var stream = new MemoryStream(attachment.Data);

                    var mailAttachment = new Attachment(stream, attachment.MediaType);
                    mailAttachment.Name = attachment.Name;
                    mailAttachment.NameEncoding = Encoding.UTF8;

                    mailMessage.Attachments.Add(mailAttachment);
                    streams.Add(stream);
                }

                smtpClient.Send(mailMessage);

                smtpClient.Dispose();
                foreach(var stream in streams)
                    stream.Dispose();

                return Result.Ok();
            }
            catch(Exception e)
            {
                _logger.Error(e, "Failed to send email.");
                return ResultHelper.MapToResult(e);
            }
        }
    }
}
