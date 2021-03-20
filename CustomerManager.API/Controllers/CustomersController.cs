using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManager.API.Models;
using CustomerManager.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerRepository _customerRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CustomersController(ILogger<CustomersController> logger, ICustomerRepository customerRepo, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _customerRepo = customerRepo;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return Ok(await _customerRepo.GetAllAsync());
        }

        [HttpGet("name")]
        public async Task<ActionResult<Customer>> GetCustomer(string name)
        {
            var customer = await _customerRepo.GetByNameAsync(name);
            // return customer == null ? NotFound(name) : customer;

            if (customer == null)
            {
                _logger.LogInformation($"customer {name} not found");
                return NotFound(name);
            }
            return customer;
        }

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
            return Created("Customers", customer.Name);
        }

        [HttpDelete]
        public IActionResult RemoveCustomer(Customer customer)
        {
            _customerRepo.Remove(customer);
            _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
