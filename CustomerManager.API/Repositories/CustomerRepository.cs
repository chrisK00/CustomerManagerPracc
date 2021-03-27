using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CustomerManager.API.Data;
using CustomerManager.API.DTOs;
using CustomerManager.API.Models;
using CustomerManager.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomerManager.API.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public CustomerRepository(IMapper mapper, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<AppUser> GetUserAsync(string id)
        {
            var user = await _userManager.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<AppUser> GetUserByUserNameAsync(string username)
        {
            var user = await _userManager.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.UserName == username);
            return user;
        }

        public async Task<ICollection<CustomerDTO>> GetCustomersAsync()
        {
            var customers = await _userManager.Users.ProjectTo<CustomerDTO>(_mapper.ConfigurationProvider).ToListAsync();
            return customers;
        }

        public async Task<CustomerDTO> GetCustomerByUserNameAsync(string username)
        {
            var customer = await _userManager.Users
                .Where(u => u.UserName == username)
                .ProjectTo<CustomerDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

            return customer;
        }

        public async Task<UserDTO> GetUserDTOByUserNameAsync(string username)
        {
            var user = await _userManager.Users.Where(u => u.UserName == username)
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

            return user;
        }

        public async Task RemoveAsync(AppUser user)
        {
            await _userManager.DeleteAsync(user);
        }

        public async Task AddAsync(AppUser user, string password)
        {
            await _userManager.CreateAsync(user, password);
        }    
    }
}
