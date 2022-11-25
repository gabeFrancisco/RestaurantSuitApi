using System.ComponentModel.DataAnnotations;
using RecantosSystem.Api.Models.Enums;

namespace RecantosSystem.Api.Models
{
	public class User : BaseEntity
	{
		[Required]
		[MaxLength(30)]
		public string Username { get; set; }
		[Required]
		[MaxLength(30)]
		public string Name { get; set; }
		[Required]
		[MaxLength(100)]
		public string Surname { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
		public Role Role { get; set; }
		public virtual WorkGroup WorkGroup { get; set; }
		public int WorkGroupId { get; set; }
	}
}