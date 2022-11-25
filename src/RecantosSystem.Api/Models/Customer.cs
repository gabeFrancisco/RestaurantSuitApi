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
		public virtual WorkGroup WorkGroup { get; set; }
		[Required]
		public int WorkGroupId { get; set; }
	}
}