using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CustomerManager.API.DTOs;
using CustomerManager.API.Models;
using CustomerManager.API.Repositories;
using CustomerManager.API.Repositories.Interfaces;
using CustomerManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerManager.API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepo;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(ITokenService tokenService, ILogger<AuthController> logger, IMapper mapper,
            ICustomerRepository customerRepo, IUnitOfWork unitOfWork)
        {
            _tokenService = tokenService;
            _logger = logger;
            _mapper = mapper;
            _customerRepo = customerRepo;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            //todo
            //check if user already exists

            await _customerRepo.AddAsync(_mapper.Map<AppUser>(userRegisterDTO));
            await _unitOfWork.SaveAsync();
            return Created("Customers", userRegisterDTO.Username);
        }

        [HttpPost("login")]
        public ActionResult<UserDTO> Login(UserLoginDTO customer)
        {
            //Todo
            //setup identity framework 
            //authenticate

            var token = _tokenService.CreateToken(customer);
            _logger.LogInformation($"New token created for {customer.Username}", token);

            return new UserDTO
            {
                Username = customer.Username,
                Token = token
            };

        }

        [Authorize]
        [HttpDelete("{username}")]
        public async Task<IActionResult> RemoveUserAsync(string username, UserDTO userDTO)
        {
            //Todo
            //update

            var usernameFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _customerRepo.GetUserByNameAsync(usernameFromToken);
            _customerRepo.Remove(user);

            //Todo
            //Throw keynotfound exception inside of service
            //Catch and send back Notfound
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
