using AutoMapper;
using BCP.Application.Interfaces;
using Common.Application.Services.Helpers;
using Common.Application.Services.Logging;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BCP.Application.Commands.Inscription.GeneratePdf
{
    public class GenerateInscriptionPdfHandler : IRequestHandler<GenerateInscriptionPdfRequest, Result<FileStreamResult>>
    {
        private readonly IMapper _mapper;
        private readonly IInscriptionRepository _repo;
        private readonly ILogger<GenerateInscriptionPdfHandler> _logger;
        private readonly IPdfReportService _pdfReportService;

        public GenerateInscriptionPdfHandler(IMapper mapper, IInscriptionRepository repo, ILogger<GenerateInscriptionPdfHandler> logger, IPdfReportService pdfReportService)
        {
            _mapper = mapper;
            _repo = repo;
            _logger = logger;
            _pdfReportService = pdfReportService;
        }

        public async Task<Result<FileStreamResult>> Handle(GenerateInscriptionPdfRequest request, CancellationToken cancellationToken)
        {
            _logger.Debug($"Entering command handler {GetType().Name}");
            try
            {

                //Call domain method
                var addInscriptionResult = await _repo.GetAsync<Domain.Entities.Inscription>(x => x.Id == Guid.Parse(request.Id));
                if(addInscriptionResult.IsFailed) return addInscriptionResult.ToResult();
                var inscriptionObj = addInscriptionResult.Value.First();

                var generateResult = await _pdfReportService.GenerateInscriptionPdfReport(inscriptionObj, cancellationToken);
                if(generateResult.IsFailed) return generateResult.ToResult();

                var stream = new MemoryStream(generateResult.Value);
                return new FileStreamResult(stream, "application/pdf")
                {
                    FileDownloadName = "Inscription.pdf"
                };
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Failed to generate inscription pdf");
                return ResultHelper.MapToResult(ex);
            }
        }
    }
}
