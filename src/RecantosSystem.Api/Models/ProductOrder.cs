namespace RecantosSystem.Api.Models
{
	public class ProductOrder : BaseEntity
	{
		public Product Product { get; set; }
		public int Quantity { get; set; }
		public decimal Total
		{
			get => Quantity * Product.Price;
		}
	}
}