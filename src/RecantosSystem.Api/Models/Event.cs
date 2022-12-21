using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecantosSystem.Api.Models
{
    public class Event : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Image { get; set; }
        public DateTime EventDate { get; set; }
    }
}