using BCP.Domain.Definitions;

namespace BCP.Domain.Entities
{
	public class Inscription
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Sex { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public bool IsMemberBefore { get; set; }
		public string Formula { get; set; }
		public bool JoinCompetition { get; set;}
		public List<string> CompetitionCats { get; set; }
		public string Motivation { get; set; }
		public string Status { get; set; }
		public DateTime? DtCreate { get; set; }
		public DateTime? DtUpdate { get; set; }
		public List<InscriptionImage> InscriptionImages { get; set; }
	}
}
