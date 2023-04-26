using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RecantosSystem.Api.Models;

namespace RecantosSystem.Api.DTOs
{
    public class OrderSheetDTO
    {
        public int Id { get; set; }
        [Required]
        public int TableId { get; set; }
        [Required]
		public IEnumerable<ProductOrder> Orders { get; set; }
		public int CustomerId { get; set; }
    }
}