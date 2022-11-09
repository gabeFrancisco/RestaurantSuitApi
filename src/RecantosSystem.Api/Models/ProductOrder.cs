namespace RecantosSystem.Api.Models
{
	public class ProductOrder
	{
		public int Id { get; set; }
		public Product Product { get; set; }
		public int Quantity { get; set; }
		public decimal Total
		{
			get => Quantity * Product.Price;
		}
	}
}