using System.Collections.Generic;
using System.Threading.Tasks;
using RecantosSystem.Api.Models;

namespace RecantosSystem.Api.Interfaces
{
	public interface IBaseService<T> where T : class
	{
		User User { get; }
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetAsync(int id);
		Task<T> AddAsync(T entity);
		Task<T> UpdateAsync(T entity, int id);
		Task<bool> DeleteAsync(int id);
	}
}