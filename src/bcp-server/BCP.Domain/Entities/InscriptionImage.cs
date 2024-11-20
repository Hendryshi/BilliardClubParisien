namespace BCP.Domain.Entities
{
	public class InscriptionImage
	{
		public int Id { get; set; }
		public int IdInscription { get; set; }
		public byte[] ImageData { get; set; }
	}
}
