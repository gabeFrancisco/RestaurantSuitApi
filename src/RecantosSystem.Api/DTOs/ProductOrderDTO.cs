using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RecantosSystem.Api.Models;

namespace RecantosSystem.Api.DTOs
{
    public class ProductOrderDTO
    {
        [Required]
        public int OrderSheetId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
		public int Quantity { get; set; }
    }
}