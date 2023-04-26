using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecantosSystem.Api.Models
{
	public class OrderSheet : BaseEntity
	{
		public int TableId { get; set; }
		public virtual Table Table { get; set; }
		public IEnumerable<ProductOrder> ProductOrders { get; set; }
		public int CustomerId { get; set; }
		public virtual Customer Customer { get; set; }
		public virtual WorkGroup WorkGroup { get; set; }
		[Required]
		public int WorkGroupId { get; set; }
	}
}