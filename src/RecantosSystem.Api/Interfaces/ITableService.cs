using System.Threading.Tasks;
using RecantosSystem.Api.DTOs;

namespace RecantosSystem.Api.Interfaces
{
	public interface ITableService : IBaseService<TableDTO>
	{
        Task<bool> SetIsBusy(int tableId, bool state, bool confirm);
    }
}