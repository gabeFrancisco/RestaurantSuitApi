using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecantosSystem.Api.DTOs
{
    public class EventDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string Hour { get; set; }
        [Required]
        public string Minutes { get; set; }
    }
}