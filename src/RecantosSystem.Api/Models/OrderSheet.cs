using System.Collections.Generic;
namespace RecantosSystem.Api.Models
{
	public class OrderSheet : BaseEntity
	{
		public Table Table { get; set; }
		public IEnumerable<ProductOrder> Orders { get; set; }
	}
}