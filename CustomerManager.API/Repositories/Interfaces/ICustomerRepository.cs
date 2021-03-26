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
        Task<AppUser> GetUserAsync(int id);
        Task<AppUser> GetUserByNameAsync(string username);
        Task<UserDTO> GetUserDTOByUsernameAsync(string username);
        Task<CustomerDTO> GetCustomerByUsernameAsync(string username);
        Task<ICollection<CustomerDTO>> GetCustomersAsync();
        Task AddAsync(AppUser user);
        void Remove(AppUser user);
    }
}
