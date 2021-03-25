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

        public async Task<ICollection<MemberDTO>> GetMembersAsync()
        {           
            var members = await _context.Customers.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).ToListAsync();
            return members;
        }

        public async Task<CustomerDTO> GetCustomerByUsernameAsync(string username)
        {
           var customer = await _context.Customers
                .Where(c => c.Username == username)
                .ProjectTo<CustomerDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
            return customer;
        }

        public async Task<MemberDTO> GetMemberByUsernameAsync(string username)
        {
            var customer = await _context.Customers
                 .Where(c => c.Username == username)
                 .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
            return customer;
        }

        //Todo
        //update below they are old
        public void Remove(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
        }
    }
}
