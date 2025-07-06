using BCP.Application.Entity.Models;
using BCP.Application.Interfaces;
using BCP.Domain.Entities;
using Common.Application.Services.Helpers;
using Common.Application.Services.Logging;
using FluentResults;
using Microsoft.Extensions.Logging;
using static Common.Domain.Definition.Inscription_DEFValues;

namespace BCP.Application.Services
{
    public class InscriptionJobService : IInscriptionJobService
    {
        private readonly IEmailService _emailService;
        private readonly IPdfReportService _pdfReportService;
        private readonly IInscriptionRepository _repo;
        private readonly ILogger<InscriptionJobService> _logger;

        public InscriptionJobService(IEmailService emailService, IPdfReportService pdfReportService, IInscriptionRepository repo, ILogger<InscriptionJobService> logger)
        {
            _emailService = emailService;
            _pdfReportService = pdfReportService;
            _repo = repo;
            _logger = logger;
        }

        public async Task<Result> GeneratePdfAndSendEmailAsync(Guid inscriptionId)
        {

            var inscriptionGetResult = await _repo.GetAsync<Inscription>(x => x.Id == inscriptionId);
            if(inscriptionGetResult.IsFailed) return inscriptionGetResult.ToResult();
            var inscription = inscriptionGetResult.Value.FirstOrDefault();
            if(inscription == null) throw new Exception("Inscription not found.");

            try
            {
                var pdfBytesResult = await _pdfReportService.GenerateInscriptionPdfReport(inscription);
                if(pdfBytesResult.IsFailed)
                {
                    inscription.Status = DEFStatus.Error;
                    var updateResultForPdf = await UpdateInscription(inscription);
                    if(updateResultForPdf.IsFailed) return updateResultForPdf.ToResult();
                    return pdfBytesResult.ToResult();
                }

                var emailCommand = CreateEmailCommand(inscription);

                var attachment = new AttachmentCommand
                {
                    Name = $"Inscription_{inscription.LastName}_{inscription.FirstName}.pdf",
                    Data = pdfBytesResult.Value,
                    MediaType = "application/pdf"
                };

                emailCommand.Attachments.Add(attachment);

                var sendResult = await _emailService.SendEmail(emailCommand);
                if(sendResult.IsFailed)
                {
                    inscription.Status = DEFStatus.Error;
                    var updateResultForEmail = await UpdateInscription(inscription);
                    if(updateResultForEmail.IsFailed) return updateResultForEmail.ToResult();
                    return sendResult;
                }
                
                inscription.Status = DEFStatus.Success;
                var updateResult = await UpdateInscription(inscription);
                if(updateResult.IsFailed) return updateResult.ToResult();

                return Result.Ok();
            }
            catch(Exception e)
            {
                _logger.Error(e.Message);
                inscription.Status = DEFStatus.Error;
                await UpdateInscription(inscription);
                return ResultHelper.MapToResult(e);
            }
        }

        private EmailCommand CreateEmailCommand(Inscription inscription)
        {
            var emailCommand = new EmailCommand();

            emailCommand.Object = "[Nouvelle Inscription] Formulaire de candidature";
            emailCommand.Body = $@"
                <!DOCTYPE html>
                <html lang='fr'>
                <head>
                  <meta charset='UTF-8'>
                  <title>Inscription BCP</title>
                </head>
                <body>
                  <p>Bonjour,</p><p>Une nouvelle demande d'inscription a été soumise.</p><p><strong>Nom du candidat :</strong> {inscription.FirstName} {inscription.LastName}</p><p>Le formulaire est disponible en pièce jointe (PDF).</p><br/><p>Bien cordialement,<br/>Système BCP</p>
                </body>
                </html>";

            return emailCommand;
        }

        private async Task<Result<Inscription>> UpdateInscription(Inscription inscription)
        {
            // update letter
            var updateResult = await _repo.UpdateAsync(inscription);
            if(updateResult.IsFailed) return updateResult.ToResult();

            var commitOperationsResult = await _repo.CommitOperationsAsync();
            if(commitOperationsResult.IsFailed) return commitOperationsResult;

            return Result.Ok(updateResult.Value);
        }
    }
}
