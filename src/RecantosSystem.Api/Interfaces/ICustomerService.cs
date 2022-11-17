using System.Collections.Generic;
using System.Threading.Tasks;
using RecantosSystem.Api.DTOs;

namespace RecantosSystem.Api.Interfaces
{
    public interface ICustomerService
    {
         int UserId { get; }
         Task<IEnumerable<CustomerDTO>> GetAllAsync();
         Task<CustomerDTO> GetAsync(int customerId);
         Task<CustomerDTO> AddAsync(CustomerDTO customerDto);
         Task<CustomerDTO> UpdateAsync(CustomerDTO customerDto);
         Task<bool> DeleteAsync(int customerId);
    }
}