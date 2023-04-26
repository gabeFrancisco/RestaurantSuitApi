using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RecantosSystem.Api.Models;
using RecantosSystem.Api.Models.Enums;

namespace RecantosSystem.Api.DTOs
{
    public class OrderSheetDTO
    {
        public OrderState OrderState { get; set; }
        public int Id { get; set; }
        [Required]
        public int TableId { get; set; }
        public Table Table { get; set; }
        [Required]
		public IEnumerable<ProductOrderDTO> ProductOrders { get; set; }
		public int? CustomerId { get; set; }
        public int UserId { get; set; }
        public Customer Customer { get; set; }
    }
}