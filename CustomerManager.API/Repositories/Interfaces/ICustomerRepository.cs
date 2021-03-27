using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CustomerManager.API.DTOs;
using CustomerManager.API.Models;

namespace CustomerManager.API.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<AppUser> GetUserAsync(string id);
        Task<AppUser> GetUserByUserNameAsync(string username);
        Task<UserDTO> GetUserDTOByUserNameAsync(string username);
        Task<CustomerDTO> GetCustomerByUserNameAsync(string username);
        Task<ICollection<CustomerDTO>> GetCustomersAsync();
        Task AddAsync(AppUser user, string password);
        Task RemoveAsync(AppUser user);
    }
}
