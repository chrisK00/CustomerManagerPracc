using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CustomerManager.API.Data;
using CustomerManager.API.DTOs;
using CustomerManager.API.Models;
using CustomerManager.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerManager.API.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(CustomerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppUser> GetUserAsync(int id)
        {
            var user = await _context.AppUsers.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<AppUser> GetUserByNameAsync(string username)
        {
            var user = await _context.AppUsers.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }

        public async Task<ICollection<CustomerDTO>> GetCustomersAsync()
        {           
            var customers = await _context.AppUsers.ProjectTo<CustomerDTO>(_mapper.ConfigurationProvider).ToListAsync();
            return customers;
        }

        public async Task<UserDTO> GetUserDTOByUsernameAsync(string username)
        {
           var user = await _context.AppUsers
                .Where(c => c.Username == username)
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
            return user;
        }

        public async Task<CustomerDTO> GetCustomerByUsernameAsync(string username)
        {
            var customer = await _context.AppUsers
                 .Where(c => c.Username == username)
                 .ProjectTo<CustomerDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
            return customer;
        }

        public void Remove(AppUser customer)
        {
            _context.AppUsers.Remove(customer);
        }

        public async Task AddAsync(AppUser customer)
        {
            await _context.AppUsers.AddAsync(customer);
        }

    
    }
}
