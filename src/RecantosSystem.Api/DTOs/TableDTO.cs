using System.ComponentModel.DataAnnotations;

namespace RecantosSystem.Api.DTOs
{
    public class TableDTO
    {
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
		public int Chairs { get; set; }
        public bool IsBusy { get; set; }
    }
}