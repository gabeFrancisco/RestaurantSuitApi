using System.Collections.Generic;

namespace RecantosSystem.Api.Models
{
    public class OrderSheet
    {
        public int Id { get; set; }
        public Table Table { get; set; }
        public IEnumerable<ProductOrder> Orders { get; set; }
    }
}