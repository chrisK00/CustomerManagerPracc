using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CustomerManager.API.DTOs;
using CustomerManager.API.Extensions;
using CustomerManager.API.Helpers;
using CustomerManager.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IMapper _mapper;

        public CustomersController(ILogger<CustomersController> logger, ICustomerRepository customerRepo, IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            //Todo
            //move to service
            _customerRepo = customerRepo;
            _unitOfWork = unitOfWork;
            //should have a service that does the update and mappings
            _mapper = mapper;

            //add xml comments on methods and enable swagger xml doc file
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<CustomerDTO>>> GetCustomers([FromQuery] UserParams userParams)
        {
            userParams.CurrentUserName = User.GetUsername();
            var customers = await _customerRepo.GetCustomersAsync(userParams);
            Response.AddPaginationHeader(customers.PageSize, customers.TotalCount, customers.TotalPages, customers.CurrentPage);

            return customers;
        }

        [HttpGet("{username}", Name = "GetCustomer")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(string username)
        {
            var customer = await _customerRepo.GetCustomerByUserNameAsync(username);
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
        public async Task<IActionResult> UpdateCustomer(string username, CustomerUpdateDTO customer)
        {
            var usernameFromToken = User.GetUsername();
            var user = await _customerRepo.GetUserByUserNameAsync(usernameFromToken);
            _mapper.Map(customer, user);

            if (!await _unitOfWork.SaveAsync())
            {
                return BadRequest("Failed to update user");
            }
            return NoContent();
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> RemoveUser(string username)
        {
            var user = await _customerRepo.GetUserByUserNameAsync(User.GetUsername());
            await _customerRepo.RemoveAsync(user);

            //Todo
            //Throw keynotfound exception inside of service
            //Catch and send back Notfound
            if (!await _unitOfWork.SaveAsync())
            {
                return BadRequest("Failed to update user");
            }
            return NoContent();
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDTO>> AddPhoto(IFormFile file)
        {
            //Todo
            //add and configure cloudinary or change from photo to todo instead or mb quotes?
            var photo = new PhotoDTO { };

            return CreatedAtRoute(nameof(GetCustomer), new { username = "Hull" }, photo);
        }
    }
}