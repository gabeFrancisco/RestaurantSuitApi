using System.ComponentModel.DataAnnotations;

namespace RecantosSystem.Api.Models
{
	public class Table : BaseEntity
	{
		public int Number { get; set; }
		public int Chairs { get; set; }
		public virtual User User { get; set; }
		[Required]
		public int UserId { get; set; }
	}
}