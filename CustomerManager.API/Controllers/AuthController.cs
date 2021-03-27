using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CustomerManager.API.DTOs;
using CustomerManager.API.Models;
using CustomerManager.API.Repositories;
using CustomerManager.API.Repositories.Interfaces;
using CustomerManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(ITokenService tokenService, ILogger<AuthController> logger, IMapper mapper,
            ICustomerRepository customerRepo, IUnitOfWork unitOfWork,
            SignInManager<AppUser> signInManager)
        {
            _tokenService = tokenService;
            _logger = logger;
            _mapper = mapper;
            _customerRepo = customerRepo;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            await _customerRepo.AddAsync(_mapper.Map<AppUser>(userRegisterDTO), userRegisterDTO.Password);
            await _unitOfWork.SaveAsync();
            return Created("Customers", userRegisterDTO.UserName);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(UserLoginDTO customer)
        {
            var user = await _customerRepo.GetUserByUserNameAsync(customer.UserName);
            if (user == null)
            {
                return NotFound();
            }
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, customer.Password, false);

            if (!signInResult.Succeeded)
            {
                return Unauthorized();
            }

            var token = _tokenService.CreateToken(customer);
            _logger.LogInformation($"New token created for {customer.UserName}", token);

            return new UserDTO
            {
                UserName = customer.UserName,
                Token = token
            };
        }


        [Authorize]
        [HttpDelete("{username}")]
        public async Task<IActionResult> RemoveUserAsync(string username)
        {
            //Todo
            //update

            var usernameFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _customerRepo.GetUserByUserNameAsync(usernameFromToken);
            await _customerRepo.RemoveAsync(user);

            //Todo
            //Throw keynotfound exception inside of service
            //Catch and send back Notfound
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}


