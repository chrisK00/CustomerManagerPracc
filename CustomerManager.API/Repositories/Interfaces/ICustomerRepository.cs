using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerManager.API.DTOs;
using CustomerManager.API.Helpers;
using CustomerManager.API.Models;

namespace CustomerManager.API.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<AppUser> GetUserAsync(string id);

        Task<AppUser> GetUserByUserNameAsync(string username);

        Task<UserDTO> GetUserDTOByUserNameAsync(string username);

        Task<CustomerDTO> GetCustomerByUserNameAsync(string username);

        Task<PagedList<CustomerDTO>> GetCustomersAsync(UserParams userParams);

        Task AddAsync(AppUser user, string password);

        Task RemoveAsync(AppUser user);
    }
}