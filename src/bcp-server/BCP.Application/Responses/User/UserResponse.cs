using System.ComponentModel.DataAnnotations;

namespace BCP.Application.Responses.User
{
    public class UserResponse
    {
        #region fields
        [Required]
        public string Id { get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
		public byte[] PasswordHash { get; set; }
		[Required]
		public byte[] PasswordSalt { get; set; }
		#endregion
	}
}