using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CustomerManager.API.DTOs;
using CustomerManager.API.Helpers;
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
            return await _userManager.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<AppUser> GetUserByUserNameAsync(string username)
        {
            return await _userManager.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<PagedList<CustomerDTO>> GetCustomersAsync(UserParams userParams)
        {
            //we just wanna return theese to the client
            var query = _userManager.Users.ProjectTo<CustomerDTO>(_mapper.ConfigurationProvider).AsNoTracking();
            return await PagedList<CustomerDTO>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<CustomerDTO> GetCustomerByUserNameAsync(string username)
        {
            return await _userManager.Users
                .Where(u => u.UserName == username)
                .ProjectTo<CustomerDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<UserDTO> GetUserDTOByUserNameAsync(string username)
        {
            return await _userManager.Users.Where(u => u.UserName == username)
                 .ProjectTo<UserDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
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