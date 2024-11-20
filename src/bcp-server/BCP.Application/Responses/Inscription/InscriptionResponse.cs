using System.ComponentModel.DataAnnotations;

namespace BCP.Application.Responses.Inscription
{
    public class InscriptionResponse
    {
		[Required]
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
		public DateTime? DtCreate { get; set; }
		public DateTime? DtUpdate { get; set; }
		public List<InscriptionImageResponse> InscriptionImages { get; set; }
	}

	public class InscriptionImageResponse
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public int IdInscription { get; set; }
		public byte[] ImageData { get; set; }
	}
}