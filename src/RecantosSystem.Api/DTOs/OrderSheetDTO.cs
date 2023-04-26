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
        [Required]
        public OrderState OrderState { get; set; }
        public int Id { get; set; }
        [Required]
        public int TableId { get; set; }
        public TableDTO Table { get; set; }
        [Required]
		public IEnumerable<ProductOrderDTO> ProductOrders { get; set; }
        [Required]
		public int? CustomerId { get; set; }
        public CustomerDTO Customer { get; set; }
        public string OpenBy { get; set; }
    }
}