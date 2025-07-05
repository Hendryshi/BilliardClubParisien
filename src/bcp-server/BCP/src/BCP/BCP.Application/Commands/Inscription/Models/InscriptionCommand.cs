using Common.Application.Commands;
using Common.Domain.Common;

namespace BCP.Application.Commands.Inscription.Models
{
    public class InscriptionCommand : BaseAuditableEntityCommand
    {
        public Optional<string> FirstName { get; set; }
        public Optional<string> LastName { get; set; }
        public Optional<string> Sex { get; set; }
        public Optional<string> Email { get; set; }
        public Optional<string> Phone { get; set; }
        public Optional<bool> IsMemberBefore { get; set; }
        public Optional<string> Formula { get; set; }
        public Optional<bool> JoinCompetition { get; set; }
        public Optional<List<string>> CompetitionCats { get; set; }
        public Optional<string> Motivation { get; set; }
        public Optional<string> Status { get; set; }
        public Optional<List<InscriptionImageCommand>> InscriptionImages { get; set; }
    }

    public class InscriptionImageCommand : BaseEntityCommand
    {
        public Optional<Guid?> IdInscription { get; set; }
        public Optional<string> ImageData { get; set; }
    }
}
