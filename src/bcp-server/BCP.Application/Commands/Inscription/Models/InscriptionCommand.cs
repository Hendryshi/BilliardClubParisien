using BCP.Domain.Definitions;
using BCP.Domain.Entities;

namespace BCP.Application.Commands.Inscription.Models
{
	public class InscriptionCommand
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Sex { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public bool IsMemberBefore { get; set; }
		public string Formula { get; set; }
		public bool JoinCompetition { get; set; }
		public List<string> CompetitionCats { get; set; }
		public string Motivation { get; set; }
		public string Status { get; set; }
		public List<InscriptionImageCommand> InscriptionImages { get; set; }
	}

	public class InscriptionImageCommand
	{
		public int Id { get; set; }
		public int IdInscription { get; set; }
		public string ImageData { get; set; }
	}
}
