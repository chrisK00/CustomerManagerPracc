using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerManager.API.DTOs;
using CustomerManager.API.Models;
using CustomerManager.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerManager.API.Controllers
{
    [Authorize]
    public class CustomersController : BaseApiController
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerRepository _customerRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CustomersController(ILogger<CustomersController> logger, ICustomerRepository customerRepo, IUnitOfWork unitOfWork)
        {
            _logger = logger;

            //Todo
            //move to service
            _customerRepo = customerRepo;
            _unitOfWork = unitOfWork;

            //add xml comments on methods and enable swagger xml doc file
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var customers = await _customerRepo.GetMembersAsync();
            return Ok(customers);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(string username)
        {
            var customer = await _customerRepo.GetMemberByUsernameAsync(username);
            // return customer == null ? NotFound(name) : customer;

            if (customer == null)
            {
                _logger.LogWarning($"customer {username} not found");
                return NotFound(username);
            }

            return customer;
        }

        //Todo
        //update the methods make use of the url username + jwt User claims
        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateCustomer(string username, AppUser customer)
        {            
            _customerRepo.Update(customer);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddCustomer(AppUser customer)
        {
            await _customerRepo.AddAsync(customer);
            await _unitOfWork.SaveAsync();
            return Created("Customers", customer.Username);
        }

        [HttpDelete("{username}")]
        public IActionResult RemoveCustomer(string username, AppUser customer)
        {
            //Todo
            //Throw keynotfound exception inside of service
            //Catch and send back Notfound
            //update route should have id in url

            _customerRepo.Remove(customer);
            _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
