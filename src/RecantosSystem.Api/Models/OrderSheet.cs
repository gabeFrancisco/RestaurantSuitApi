using System.Collections.Generic;
namespace RecantosSystem.Api.Models
{
	public class OrderSheet : BaseEntity
	{
		public int TableId { get; set; }
		public virtual Table Table { get; set; }
		public IEnumerable<ProductOrder> Orders { get; set; }
		public int CustomerId { get; set; }
		public virtual Customer Customer { get; set; }
	}
}