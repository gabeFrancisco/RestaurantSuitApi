using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecantosSystem.Api.DTOs
{
	public class WorkGroupDTO
	{
		[Required]
		public string Title { get; set; }
		public string Description { get; set; }
		public IEnumerable<int> Workers { get; set; }
	}
}