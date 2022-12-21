using System.Threading.Tasks;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Models;

namespace RecantosSystem.Api.Interfaces
{
	public interface IUserService : IBaseService<UserDTO>
	{
		Task<User> GetActualUser();
		int UserId { get; }
        int SelectedWorkGroup { get; }
		Task<dynamic> RegisterUser(UserDTO userDto);
		Task<dynamic> Login(LoginDTO loginDto);
        Task<User> GetSingleUserAsync(int id);
        Task<bool> UpdateUserLastWorkGroupId(int workGroupId);
    }
}