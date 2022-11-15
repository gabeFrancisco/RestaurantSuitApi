using System.ComponentModel.DataAnnotations;
using RecantosSystem.Api.Models.Enums;

namespace RecantosSystem.Api.DTOs
{
	public class UserDTO
	{

		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Phone { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
	}
}