using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecantosSystem.Api.Models
{
	public class Product : BaseEntity
	{
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }
		public int CategoryId { get; set; }
		public virtual Category Category { get; set; }
		public int Quantity { get; set; }

		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(8,2)")]
		[Required]
		public decimal Price { get; set; }
		public virtual User User { get; set; }
		[Required]
		public int UserId { get; set; }
	}
}