using BCP.Application.Interfaces;
using BCP.Domain.Entities;
using Common.Application.Services.Helpers;
using Common.Application.Services.Logging;
using FluentResults;
using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BCP.Application.Services
{
    public class PdfReportService : IPdfReportService
    {
        private readonly ILogger<PdfReportService> _logger;

        public PdfReportService(ILogger<PdfReportService> logger)
        {
            _logger = logger;
        }

        public async Task<Result<byte[]>> GenerateInscriptionPdfReport(Inscription inscription, CancellationToken cancellationToken = default)
        {
            try
            {
                var doc = new InscriptionDocument(inscription);
                return doc.GeneratePdf();
            }
            catch(Exception e)
            {
                _logger.Error(e, "Failed to generation inscription pdf report");
                return ResultHelper.MapToResult(e);
            }
        }
    }

    public class InscriptionDocument : IDocument
    {
        private readonly Inscription _data;

        public InscriptionDocument(Inscription data)
        {
            _data = data;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));
                page.Content()
                    .Column(column =>
                    {
                        // 标题区域
                        column.Item().Background(Colors.Blue.Lighten4).Padding(20).Column(header =>
                        {
                            header.Item().Text("BILLARD CLUB PARISIEN").FontSize(20).Bold().FontColor(Colors.Blue.Darken3).AlignCenter();
                            header.Item().Text("PRISE DE LICENCE - SAISON 2025-2026").FontSize(16).Bold().FontColor(Colors.Blue.Darken2).AlignCenter();
                            header.Item().Text("FORMULAIRE DE CANDIDATURE").FontSize(14).Bold().FontColor(Colors.Blue.Darken1).AlignCenter();
                        });

                        column.Item().Height(20);

                        // 个人信息表格
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(0.3f);
                                columns.RelativeColumn(0.7f);
                            });

                            // 表格标题
                            table.Header(header =>
                            {
                                header.Cell().ColumnSpan(2).Background(Colors.Grey.Lighten3).Padding(8).Text("INFORMATIONS PERSONNELLES").Bold().AlignCenter();
                            });

                            // 个人信息行
                            AddTableRow(table, "Nom", _data.LastName);
                            AddTableRow(table, "Prénom", _data.FirstName);
                            AddTableRow(table, "Sexe", _data.Sex);
                            AddTableRow(table, "Téléphone", _data.Phone);
                            AddTableRow(table, "Email", _data.Email);
                        });

                        column.Item().Height(15);

                        // 会员信息
                        column.Item().Table(memberTable =>
                        {
                            memberTable.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(0.7f);
                                columns.RelativeColumn(0.3f);
                            });

                            // 表格标题
                            memberTable.Header(header =>
                            {
                                header.Cell().ColumnSpan(2).Background(Colors.Grey.Lighten3).Padding(8).Text("INFORMATIONS DE LICENCE").Bold().AlignCenter();
                            });

                            // 现有会员状态
                            memberTable.Cell().Background(Colors.White).Padding(8).Text("Licencié(e) au BCP lors de la dernière saison :").FontSize(10).Bold();
                            memberTable.Cell().Background(Colors.White).Padding(8).Text(_data.IsMemberBefore ? "Oui" : "Non").FontSize(10).FontColor(_data.IsMemberBefore ? Colors.Green.Darken2 : Colors.Red.Darken2).Bold().AlignCenter();

                            // 会员套餐
                            memberTable.Cell().Background(Colors.White).Padding(8).Text("Formule d'abonnement souhaitée :").FontSize(10).Bold();
                            memberTable.Cell().Background(Colors.White).Padding(8).Text(_data.Formula).FontSize(10).FontColor(Colors.Blue.Darken2).Bold().AlignCenter();

                            // 竞赛参与
                            memberTable.Cell().Background(Colors.White).Padding(8).Text("Participation aux compétitions 2025-2026 :").FontSize(10).Bold();
                            memberTable.Cell().Background(Colors.White).Padding(8).Text(_data.JoinCompetition ? "Oui" : "Non").FontSize(10).FontColor(_data.JoinCompetition ? Colors.Green.Darken2 : Colors.Red.Darken2).Bold().AlignCenter();
                        });

                        column.Item().Height(15);

                        // 竞赛类别和级别
                        column.Item().Table(competitionTable =>
                        {
                            competitionTable.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1f);
                            });

                            competitionTable.Header(header =>
                            {
                                header.Cell().Background(Colors.Grey.Lighten3).Padding(8).Text("Catégorie & Niveau").Bold().AlignCenter();
                            });

                            // 类别和级别行 - 水平排列
                            competitionTable.Cell().Background(Colors.White).Padding(12).Row(row =>
                            {
                                // 类别部分
                                row.RelativeItem(0.5f).Column(categories =>
                                {
                                    categories.Item().Text($"{Checkbox(_data.CompetitionCats.Contains("JUNIOR"))} Junior (-23 ans)").FontSize(10);
                                    categories.Item().Height(2);
                                    categories.Item().Text($"{Checkbox(_data.CompetitionCats.Contains("MIXTE"))} Mixte").FontSize(10);
                                    categories.Item().Height(2);
                                    categories.Item().Text($"{Checkbox(_data.CompetitionCats.Contains("FEMININE"))} Féminine").FontSize(10);
                                });

                                // 级别部分
                                row.RelativeItem(0.5f).Column(levels =>
                                {
                                    levels.Item().Text($"{Checkbox(_data.CompetitionCats.Contains("REGIONAL"))} Régional").FontSize(10);
                                    levels.Item().Height(2);
                                    levels.Item().Text($"{Checkbox(_data.CompetitionCats.Contains("NATIONAL"))} National").FontSize(10);
                                });
                            });
                        });

                        column.Item().Height(15);

                        // 动机和目标
                        column.Item().Table(motivationTable =>
                        {
                            motivationTable.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1f);
                            });

                            motivationTable.Header(header =>
                            {
                                header.Cell().Background(Colors.Grey.Lighten3).Padding(8).Text("MOTIVATIONS ET OBJECTIFS SPORTIFS").Bold().AlignCenter();
                            });

                            motivationTable.Cell().Background(Colors.White).Padding(12).Text(_data.Motivation).FontSize(11);
                        });

                        column.Item().Height(15);

                        // 个人图片
                        column.Item().Table(imageTable =>
                        {
                            imageTable.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1f);
                            });

                            imageTable.Header(header =>
                            {
                                header.Cell().Background(Colors.Grey.Lighten3).Padding(8).Text("UPLOADED IMAGES").Bold().AlignCenter();
                            });

                            imageTable.Cell().Background(Colors.White).Padding(12).Column(images =>
                            {
                                if(_data.InscriptionImages != null && _data.InscriptionImages.Any())
                                {
                                    foreach(var image in _data.InscriptionImages.Take(2))
                                    {
                                        if(image.ImageData != null && image.ImageData.Length > 0)
                                        {
                                            images.Item().Height(200).AlignCenter().Image(image.ImageData).FitArea();
                                            images.Item().Height(10);
                                        }
                                    }
                                }
                                else
                                {
                                    images.Item().Text("Aucune image fournie").FontSize(10).FontColor(Colors.Grey.Medium).AlignCenter();
                                }
                            });
                        });
                    });
            });
        }

        private void AddTableRow(TableDescriptor table, string label, string value)
        {
            table.Cell().Background(Colors.Grey.Lighten4).Padding(5).Text(label).Bold().FontSize(10);
            table.Cell().Background(Colors.White).Padding(5).Text(value ?? "").FontSize(10);
        }

        private string Checkbox(bool isChecked) => isChecked ? "[X]" : "[  ]";
    }
}

