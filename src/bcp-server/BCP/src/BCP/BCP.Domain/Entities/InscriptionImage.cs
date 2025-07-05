using Common.Domain.Common;

namespace BCP.Domain.Entities
{
	public class InscriptionImage : BaseEntity
	{
		public Guid IdInscription { get; set; }
		public byte[] ImageData { get; set; }
	}
}
