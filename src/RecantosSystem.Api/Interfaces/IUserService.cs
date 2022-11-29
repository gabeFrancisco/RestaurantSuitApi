using System.Threading.Tasks;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Models;

namespace RecantosSystem.Api.Interfaces
{
	public interface IUserService
	{
		User GetUser();
		int SelectedWorkGroup { get; }
		Task<dynamic> RegisterUser(UserDTO userDto);
		Task<dynamic> Login(LoginDTO loginDto);
		Task<User> ReadUser(int id);
	}
}