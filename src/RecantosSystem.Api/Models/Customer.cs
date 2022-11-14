using System.ComponentModel.DataAnnotations;

namespace RecantosSystem.Api.Models
{
	public class Customer : BaseEntity
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }
		[Required]
		[MaxLength(20)]
		public string Phone { get; set; }
        [Required]
		[MaxLength(40)]
		public string Email { get; set; }
	}
}