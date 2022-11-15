using System.ComponentModel.DataAnnotations;

namespace RecantosSystem.Api.Models
{
	public class ProductOrder : BaseEntity
	{
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }
		public int Quantity { get; set; }
		public decimal Total
		{
			get => Quantity * Product.Price;
		}
		public virtual User User { get; set; }
		[Required]
		public int UserId { get; set; }
	}
}