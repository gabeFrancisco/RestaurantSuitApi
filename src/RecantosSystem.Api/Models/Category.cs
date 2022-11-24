using System.ComponentModel.DataAnnotations;

namespace RecantosSystem.Api.Models
{
	public class Category : BaseEntity
	{
		[Required]
		[MaxLength(40)]
		public string Name { get; set; }
		[Required]
		[MaxLength(10)]
		public string Color { get; set; }
		public virtual User User { get; set; }
		[Required]
		public int UserId { get; set; }
	}
}