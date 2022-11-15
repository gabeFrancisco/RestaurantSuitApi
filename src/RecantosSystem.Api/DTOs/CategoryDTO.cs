using System.ComponentModel.DataAnnotations;

namespace RecantosSystem.Api.DTOs
{
    public class CategoryDTO
    {
        [Required]
		[MaxLength(40)]
		public string Name { get; set; }
		[Required]
		[MaxLength(10)]
		public string Color { get; set; }
    }
}