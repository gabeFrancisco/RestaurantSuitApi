using System.Threading.Tasks;
using RecantosSystem.Api.DTOs;

namespace RecantosSystem.Api.Interfaces
{
	public interface IUserService
	{
		int UserId { get; }
		Task<dynamic> RegisterUser(UserDTO userDto);
		Task<dynamic> Login(LoginDTO loginDto);
		Task<dynamic> GetUser(int userId);
	}
}