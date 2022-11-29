using System.Threading.Tasks;
using RecantosSystem.Api.DTOs;

namespace RecantosSystem.Api.Interfaces
{
    public interface IWorkGroupService : IBaseService<WorkGroupDTO>
    {
         Task<WorkGroupDTO> SelectWorkGroup(int id);
    }
}