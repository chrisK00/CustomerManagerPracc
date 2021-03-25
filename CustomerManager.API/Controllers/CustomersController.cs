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

            //Todo
            //move to data seed class 
            //add xml comments on methods and enable swagger xml doc file
            FakeSeedDataAsync();
        }

       private async Task FakeSeedDataAsync()
        {
            await _customerRepo.AddAsync(new Customer { Username = "Monkey" });
            await _unitOfWork.SaveAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetCustomers()
        {
            var customers = await _customerRepo.GetMembersAsync();
            return Ok(customers);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDTO>> GetCustomer(string username)
        {
            var customer = await _customerRepo.GetMemberByUsernameAsync(username);
            // return customer == null ? NotFound(name) : customer;

            if (customer == null)
            {
                _logger.LogInformation($"customer {username} not found");
                return NotFound(username);
            }

            return customer;
        }

        //Todo
        //update the method below
        [HttpPatch]
        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            _customerRepo.Update(customer);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            await _customerRepo.AddAsync(customer);
            await _unitOfWork.SaveAsync();
            return Created("Customers", customer.Username);
        }

        [HttpDelete]
        public IActionResult RemoveCustomer(Customer customer)
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
