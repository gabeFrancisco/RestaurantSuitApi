using System.ComponentModel.DataAnnotations;

namespace RecantosSystem.Api.Models
{
	public class Table : BaseEntity
	{
		public int Number { get; set; }
		public int Chairs { get; set; }
		public virtual WorkGroup WorkGroup { get; set; }
		[Required]
		public int WorkGroupId { get; set; }
	}
}