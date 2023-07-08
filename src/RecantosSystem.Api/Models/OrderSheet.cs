using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecantosSystem.Api.Models.Enums;

namespace RecantosSystem.Api.Models
{
	public class OrderSheet : BaseEntity
	{
		[Required]
		public OrderState OrderState { get; set; }
		public int TableId { get; set; }
		public virtual Table Table { get; set; }
		public IEnumerable<ProductOrder> ProductOrders { get; set; }
		public int CustomerId { get; set; }
		public virtual Customer Customer { get; set; }
		[Required]
		public string OpenBy { get; set; }
		public virtual WorkGroup WorkGroup { get; set; }
		[Required]
		public int WorkGroupId { get; set; }
	}
	

}
